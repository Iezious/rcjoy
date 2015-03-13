#include "PPMGenerator.h"

#include "stm32f4xx.h"
#include "ComCom.h"

#define PPM_TOTAL_CHANS 16
#define CHAN_LENGTH 2200UL
#define PPM_SYNC 4500
#define PPM_PULSE 300

volatile static uint32_t FRAME_TIME;

static uint16_t channels[PPM_TOTAL_CHANS];
volatile static uint16_t active_channels;
volatile static uint32_t time_spent;
volatile static uint16_t current_chan;
volatile static bool onsync = false;

TIM_HandleTypeDef htim3;

void MX_TIM3_Init(void)
{
	TIM_ClockConfigTypeDef sClockSourceConfig;
	TIM_OC_InitTypeDef sConfigOC;
	TIM_MasterConfigTypeDef sMasterConfig;

	htim3.Instance = TIM3;
	htim3.Init.Prescaler = 84-1;
	htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
	htim3.Init.Period = 1500;
	htim3.Init.RepetitionCounter = 0;
	htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	HAL_TIM_Base_Init(&htim3);

	sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
	HAL_TIM_ConfigClockSource(&htim3, &sClockSourceConfig);
	HAL_TIM_PWM_Init(&htim3);

	sConfigOC.OCMode = TIM_OCMODE_PWM1;
	sConfigOC.Pulse = PPM_PULSE;
	sConfigOC.OCPolarity = TIM_OCPOLARITY_LOW;
	sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
	HAL_TIM_PWM_ConfigChannel(&htim3, &sConfigOC, TIM_CHANNEL_1);

	sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
	sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
	HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig);
}

void HAL_TIM_Base_MspInit(TIM_HandleTypeDef* htim_base)
{

	GPIO_InitTypeDef GPIO_InitStruct;
	if (htim_base->Instance == TIM3)
	{
		/* Peripheral clock enable */
		__GPIOB_CLK_ENABLE();

		/**TIM3 GPIO Configuration
		PB4     ------> TIM3_CH1
		*/
		GPIO_InitStruct.Pin = GPIO_PIN_4;
		GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
		GPIO_InitStruct.Pull = GPIO_PULLUP;
		GPIO_InitStruct.Speed = GPIO_SPEED_FAST;
		GPIO_InitStruct.Alternate = GPIO_AF2_TIM3;
		HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

		__TIM3_CLK_ENABLE();

		/* Peripheral interrupt init*/
		/* Sets the priority grouping field */
		HAL_NVIC_SetPriorityGrouping(NVIC_PRIORITYGROUP_0);
		HAL_NVIC_SetPriority(TIM3_IRQn, 0, 0);
		HAL_NVIC_EnableIRQ(TIM3_IRQn);
	}
}

void HAL_TIM_Base_MspDeInit(TIM_HandleTypeDef* htim_base)
{

	if (htim_base->Instance == TIM3)
	{
		/* Peripheral clock disable */
		__TIM3_CLK_DISABLE();
		__GPIOB_CLK_DISABLE();
		
		/**TIM3 GPIO Configuration
		PB4     ------> TIM3_CH1
		*/
		HAL_GPIO_DeInit(GPIOB, GPIO_PIN_4);

		/* Peripheral interrupt Deinit*/
		HAL_NVIC_DisableIRQ(TIM3_IRQn);
	}
}


void SendPPMState(uint8_t *b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	*al = active_channels * 2;
	*ab = (uint8_t*)channels;
}

PPMGenerator::PPMGenerator(void)
{

}

void PPMGenerator::init(uint16_t chans)
{
	for (int i = 0; i < PPM_TOTAL_CHANS; i++) *(channels + i) = 1500;
	for (int i = 0; i < PPM_TOTAL_CHANS; i+=2) *(channels + i) = 1200;

	active_channels = chans;
	FRAME_TIME = CHAN_LENGTH * chans + PPM_SYNC;

	COMCOM.RegisterCommand(0x05, &SendPPMState);

	MX_TIM3_Init();
}

void PPMGenerator::setChan(uint16_t chan, uint16_t val)
{
	if(val > 800 && val < CHAN_LENGTH)
		*(channels+chan) = val;
}

void PPMGenerator::start()
{
	current_chan = 0;
	time_spent = 0;
	onsync = true;

	TIM3->CCR1 = 0;
	TIM3->ARR = PPM_SYNC;
	TIM3->CNT = 0;

	HAL_TIM_Base_Start_IT(&htim3);
	HAL_TIM_PWM_Start_IT(&htim3, TIM_CHANNEL_1);
}

void PPMGenerator::stop()
{
	HAL_TIM_Base_Stop(&htim3);
}

uint16_t PPMGenerator::getChannel(uint8_t idx)
{
  return idx < active_channels ? *(channels+idx) : 0;
}

void PPMGenerator::setChannels(uint8_t channels)
{
	active_channels = channels;
	FRAME_TIME = CHAN_LENGTH * channels + PPM_SYNC;
}


void TIM3_IRQHandler()
{
	HAL_NVIC_ClearPendingIRQ(TIM3_IRQn);
	HAL_TIM_IRQHandler(&htim3);
}

void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
	if (onsync)
	{
		onsync = false;
		current_chan = 0;
		time_spent = 0;
	}

	if (current_chan < active_channels)
	{
		TIM3->ARR = *(channels + current_chan);
		TIM3->CCR1 = PPM_PULSE;
		time_spent += *(channels + current_chan);

		current_chan++;
	}
	else
	{
		onsync = true;
		TIM3->ARR = FRAME_TIME - time_spent;
		TIM3->CCR1 = PPM_PULSE;
	}
}