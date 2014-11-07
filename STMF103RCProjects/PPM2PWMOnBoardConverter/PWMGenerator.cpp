#include "PWMGenerator.h"

#define PPM_FRAME 10000

static void prepareRCC()
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);
	//	RCC_APB2PeriphClockCmd(RCC_APB2ENR_AFIOEN, ENABLE);


	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM4, ENABLE);
}

static void preparePINS()
{
	GPIO_InitTypeDef GPIO_InitStructure;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2 | GPIO_Pin_3 |
		GPIO_Pin_6 | GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_6 | GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
}

static void prepareTimers()
{
	TIM_DeInit(TIM2);
	TIM_DeInit(TIM3);
	TIM_DeInit(TIM4);

	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = PPM_FRAME;
	TIM_TimeBaseStructure.TIM_Prescaler = SystemCoreClock / 1000000 - 1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM2, &TIM_TimeBaseStructure);
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);
	TIM_TimeBaseInit(TIM4, &TIM_TimeBaseStructure);

	TIM_OCInitTypeDef timer_oc;

	timer_oc.TIM_OCMode = TIM_OCMode_PWM1;
	timer_oc.TIM_OutputState = TIM_OutputState_Enable;
	timer_oc.TIM_Pulse = 0;
	timer_oc.TIM_OCPolarity = TIM_OCPolarity_High;

	TIM_OC1Init(TIM2, &timer_oc);
	TIM_OC2Init(TIM2, &timer_oc);
	TIM_OC3Init(TIM2, &timer_oc);
	TIM_OC4Init(TIM2, &timer_oc);

	TIM_OC1Init(TIM3, &timer_oc);
	TIM_OC2Init(TIM3, &timer_oc);
	TIM_OC3Init(TIM3, &timer_oc);
	TIM_OC4Init(TIM3, &timer_oc);

	TIM_OC1Init(TIM4, &timer_oc);
	TIM_OC2Init(TIM4, &timer_oc);
	TIM_OC3Init(TIM4, &timer_oc);
	TIM_OC4Init(TIM4, &timer_oc);

	TIM2->DIER = 0;
	TIM3->DIER = 0;
	TIM4->DIER = 0;

	AFIO->MAPR &= ~((1 << 8) | (1 << 9) | (1 << 10) | (1 << 11) | (1 << 12));

	TIM_ITConfig(TIM2, TIM_IT_Update, ENABLE);
	TIM_ITConfig(TIM2, TIM_IT_Update, ENABLE);
	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);


	NVIC_EnableIRQ(TIM2_IRQn);
	NVIC_EnableIRQ(TIM3_IRQn);
	NVIC_EnableIRQ(TIM4_IRQn);
}

void StartGenerators()
{
	prepareRCC();
	preparePINS();
	prepareTimers();

	TIM_Cmd(TIM2, ENABLE);
	TIM_Cmd(TIM3, ENABLE);
	TIM_Cmd(TIM4, ENABLE);
}

extern "C"
{
	void TIM2_IRQHandler()
	{
		if (TIM_GetITStatus(TIM2, TIM_IT_Update) != RESET)
		{
			TIM_ClearFlag(TIM2, TIM_IT_Update);

			TIM2->CCR1 = PPMChannels[0];
			TIM2->CCR2 = PPMChannels[1];
			TIM2->CCR3 = PPMChannels[2];
			TIM2->CCR4 = PPMChannels[3];
		}
	}

	void TIM3_IRQHandler()
	{
		if (TIM_GetITStatus(TIM3, TIM_IT_Update) != RESET)
		{
			TIM_ClearFlag(TIM3, TIM_IT_Update);

			TIM3->CCR1 = PPMChannels[4];
			TIM3->CCR2 = PPMChannels[5];
			TIM3->CCR3 = PPMChannels[6];
			TIM3->CCR4 = PPMChannels[7];
		}
	}

	void TIM4_IRQHandler()
	{
		if (TIM_GetITStatus(TIM2, TIM_IT_Update) != RESET)
		{
			TIM_ClearFlag(TIM2, TIM_IT_Update);

			TIM4->CCR1 = PPMChannels[8];
			TIM4->CCR2 = PPMChannels[9];
			TIM4->CCR3 = PPMChannels[10];
			TIM4->CCR4 = PPMChannels[11];
		}
	}
}
