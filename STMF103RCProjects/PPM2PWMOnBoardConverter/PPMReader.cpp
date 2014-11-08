
#include "PPMReader.h"

uint16_t PPMChannels[MAX_CHANNELS];


void timerInit()
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM1, ENABLE);

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_AFIO, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = 35000;
	TIM_TimeBaseStructure.TIM_Prescaler = SystemCoreClock / 1000000 - 1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM1, &TIM_TimeBaseStructure);

	TIM_ICInitTypeDef timer_ic;

	timer_ic.TIM_ICPolarity = TIM_ICPolarity_Rising;
	timer_ic.TIM_ICSelection = TIM_ICSelection_DirectTI;
	timer_ic.TIM_ICPrescaler = TIM_ICPSC_DIV1;
	timer_ic.TIM_ICFilter = 1;
	timer_ic.TIM_Channel = TIM_Channel_4;

	TIM_ICInit(TIM1, &timer_ic);

	AFIO->MAPR &= ~((1 << 6) | (1 << 7));
	TIM1->DIER = 0;


	TIM_ITConfig(TIM1, TIM_IT_CC4, ENABLE);
//	TIM_ITConfig(TIM1, TIM_IT_CC4 | TIM_IT_Update, ENABLE);
	
	NVIC_SetPriority(TIM1_CC_IRQn, 2);
	NVIC_EnableIRQ(TIM1_CC_IRQn);

//	NVIC_SetPriority(TIM1_UP_IRQn, 4);
//	NVIC_EnableIRQ(TIM1_UP_IRQn);
}


void StartPPMReader()
{

	for (u8 i = 0; i < MAX_CHANNELS; i++) PPMChannels[i] = 0;
	timerInit();
	TIM_Cmd(TIM1, ENABLE);
}

static uint8_t channel = 0;
static bool sync = false;

extern "C" 
{
	/*
	void TIM1_UP_IRQHandler()
	{
		if (TIM_GetITStatus(TIM1, TIM_IT_Update) != RESET)
		{
			TIM_ClearFlag(TIM1, TIM_IT_Update);
			
			sync = false;
			channel = 0;
		}
	}
	*/
	void TIM1_CC_IRQHandler()
	{
		if (TIM_GetITStatus(TIM1, TIM_IT_CC4) != RESET)
		{
			TIM1->CNT = 0;
			TIM_ClearFlag(TIM1, TIM_IT_CC4);

			uint16_t time = TIM1->CCR4;

			if (time > 2600)
			{
				sync = true;
				channel = 0;
			}
			else if (sync)
			{
				PPMChannels[channel] = time;
				channel++;

				sync = channel < MAX_CHANNELS;
			}

			if (TIM_GetFlagStatus(TIM1, TIM_FLAG_CC4OF) != RESET)
				TIM_ClearFlag(TIM1, TIM_FLAG_CC4OF);
		}
	}
}