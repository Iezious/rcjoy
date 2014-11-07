#include "PPMGenerator.h"

#include "stm32f4xx.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_tim.h"
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

void static timerInit()
{
	GPIO_InitTypeDef GPIO_InitStructure;

	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOB, ENABLE);

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_4;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP ;

	GPIO_Init(GPIOB, &GPIO_InitStructure);
	GPIO_PinAFConfig(GPIOB, GPIO_PinSource4, GPIO_AF_TIM3);

	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = 1500;
	TIM_TimeBaseStructure.TIM_Prescaler = 84-1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);

	TIM_OCInitTypeDef  TIM_OCInitStructure;

	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
	TIM_OCInitStructure.TIM_Pulse = PPM_PULSE;
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_Low;  
	TIM_OC1Init(TIM3, &TIM_OCInitStructure);

	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);
	NVIC_EnableIRQ(TIM3_IRQn);
}

PPMGenerator::PPMGenerator(void)
{

}

void PPMGenerator::init(uint16_t chans)
{
	for(int i=0;i<PPM_TOTAL_CHANS;i++) *(channels+i)=1500;

	active_channels = chans;
	FRAME_TIME = CHAN_LENGTH * chans + PPM_SYNC;

	timerInit();
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

	TIM_Cmd(TIM3, ENABLE);

}

void PPMGenerator::stop()
{
	TIM_Cmd(TIM3, DISABLE);
}

void PPMGenerator::setChannels(uint8_t channels)
{
	active_channels = channels;
	FRAME_TIME = CHAN_LENGTH * channels + PPM_SYNC;
}

void SendPPMState()
{
	COMCOM.Send(0x05, active_channels * 2, (u8*)channels);
}

void TIM3_IRQHandler()
{
	if(onsync)
	{
		onsync = false;
		current_chan = 0;
		time_spent = 0;
	}

	if(current_chan < active_channels)
	{
		TIM3->ARR = *(channels+current_chan);
		TIM3->CCR1 = PPM_PULSE;
		time_spent += *(channels+current_chan);

		current_chan++;
	}
	else
	{
		onsync = true;
		TIM3->ARR =FRAME_TIME - time_spent;
		TIM3->CCR1 = PPM_PULSE;
	}

	TIM_ClearITPendingBit(TIM3, TIM_IT_Update);
}