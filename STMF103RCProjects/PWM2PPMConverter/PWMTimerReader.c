#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>
#include "PWMTimerReader.h"
#include "PPMGenerator.h"

#define TOP 20000

void prepareRCC()
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);
//	RCC_APB2PeriphClockCmd(RCC_APB2ENR_AFIOEN, ENABLE);


	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM4, ENABLE);
}

void preparePINS()
{
	GPIO_InitTypeDef GPIO_InitStructure;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2 | GPIO_Pin_3 | 
		GPIO_Pin_6 | GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_6 | GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
}

void prepareTimers()
{
	TIM_DeInit(TIM2);
	TIM_DeInit(TIM3);
	TIM_DeInit(TIM4);

	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = TOP;
	TIM_TimeBaseStructure.TIM_Prescaler = SystemCoreClock / 1000000 - 1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM2, &TIM_TimeBaseStructure);
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);
	TIM_TimeBaseInit(TIM4, &TIM_TimeBaseStructure);

	TIM_ICInitTypeDef timer_ic;

	timer_ic.TIM_ICPolarity = TIM_ICPolarity_Rising;
	timer_ic.TIM_ICSelection = TIM_ICSelection_DirectTI;
	timer_ic.TIM_ICPrescaler = TIM_ICPSC_DIV1;
	timer_ic.TIM_ICFilter = 0;

	timer_ic.TIM_Channel = TIM_Channel_1;
	TIM_ICInit(TIM2, &timer_ic);
	TIM_ICInit(TIM3, &timer_ic);
	TIM_ICInit(TIM4, &timer_ic);

	timer_ic.TIM_Channel = TIM_Channel_2;
	TIM_ICInit(TIM2, &timer_ic);
	TIM_ICInit(TIM3, &timer_ic);
	TIM_ICInit(TIM4, &timer_ic);

	timer_ic.TIM_Channel = TIM_Channel_3;
	TIM_ICInit(TIM2, &timer_ic);
	TIM_ICInit(TIM3, &timer_ic);
	TIM_ICInit(TIM4, &timer_ic);

	timer_ic.TIM_Channel = TIM_Channel_4;
	TIM_ICInit(TIM2, &timer_ic);
	TIM_ICInit(TIM3, &timer_ic);
	TIM_ICInit(TIM4, &timer_ic);

	TIM2->DIER = 0;
	TIM3->DIER = 0;
	TIM4->DIER = 0;

	AFIO->MAPR &= ~((1 << 8) | (1 << 9) | (1 << 10) | (1 << 11) | (1 << 12));

	TIM_ITConfig(TIM2, TIM_IT_CC1 | TIM_IT_CC2 | TIM_IT_CC3 | TIM_IT_CC4, ENABLE);
	TIM_ITConfig(TIM3, TIM_IT_CC1 | TIM_IT_CC2 | TIM_IT_CC3 | TIM_IT_CC4, ENABLE);
	TIM_ITConfig(TIM4, TIM_IT_CC1 | TIM_IT_CC2 | TIM_IT_CC3 | TIM_IT_CC4, ENABLE);

	
	NVIC_EnableIRQ(TIM2_IRQn);
	NVIC_EnableIRQ(TIM3_IRQn);
	NVIC_EnableIRQ(TIM4_IRQn);
}

volatile uint16_t Raising[12];
volatile uint16_t Falling[12];
volatile uint8_t Ready[12];

void InitPWMReader()
{
	uint16_t i;

	for (i = 0; i < 12; i++)
	{
		Raising[i] = 0;
		Falling[i] = 0;
		Ready[i] = 0;
	}

	prepareRCC();
	preparePINS();
	prepareTimers();
}

inline uint16_t uint16_time_diff(uint16_t now, uint16_t before)
{
	return (now >= before) ? (now - before) : (TOP - before + now);
}

void StartPWMReader()
{
	TIM_Cmd(TIM2, ENABLE);
	TIM_Cmd(TIM3, ENABLE);
	TIM_Cmd(TIM4, ENABLE);
}

void CalcChans()
{
	uint16_t i;

	for (i = 0; i < 12; i++)
	{
		if (Ready[i])
		{
			Ready[i] = 0;
			u16 pulse = uint16_time_diff(Falling[i], Raising[i]);
			setPPMGeneratorChan(i, pulse);
		}
	}
}



#define CheckChannel(TIMx, TChan, CChan)						\
	if (TIM_GetITStatus(TIMx, TIM_IT_CC##TChan) != RESET)		\
	{															\
		TIM_ClearFlag(TIMx, TIM_IT_CC##TChan);					\
																\
		if (TIMx->CCER & TIM_CCER_CC##TChan##P)					\
		{														\
			Falling[CChan] = TIMx->CCR##TChan;					\
			Ready[CChan] = 1;									\
			TIMx->CCER &= ~TIM_CCER_CC##TChan##P;					\
		}														\
		else													\
		{														\
			Raising[CChan] = TIMx->CCR##TChan;						\
			TIMx->CCER |= TIM_CCER_CC##TChan##P;						\
		}															\
																	\
		if (TIM_GetFlagStatus(TIMx, TIM_FLAG_CC##TChan##OF) != RESET)	\
			TIM_ClearFlag(TIMx, TIM_FLAG_CC##TChan##OF);				\
	}															

void TIM3_IRQHandler()
{
	CheckChannel(TIM3, 1, 4);
	CheckChannel(TIM3, 2, 5);
	CheckChannel(TIM3, 3, 6);
	CheckChannel(TIM3, 4, 7);
}

void TIM2_IRQHandler()
{
	CheckChannel(TIM2, 1, 0);
	CheckChannel(TIM2, 2, 1);
	CheckChannel(TIM2, 3, 2);
	CheckChannel(TIM2, 4, 3);
}

void TIM4_IRQHandler()
{
	CheckChannel(TIM4, 1, 8);
	CheckChannel(TIM4, 2, 9);
	CheckChannel(TIM4, 3, 10);
	CheckChannel(TIM4, 4, 11);
}


/*
if (TIM_GetITStatus(TIM1, TIM_IT_CC1) != RESET)
{
TIM_ClearFlag(TIM1, TIM_IT_CC1);

if (TIM1->CCER & TIM_CCER_CC1P)
{
Falling[4] = TIM1->CCR1;
Ready[4] = 1;
TIM1->CCER &= ~TIM_CCER_CC1P;
}
else
{
Raising[4] = TIM1->CCR1;
TIM1->CCER |= TIM_CCER_CC1P;
}

if (TIM_GetFlagStatus(TIM1, TIM_FLAG_CC1OF) != RESET)
TIM_ClearFlag(TIM1, TIM_FLAG_CC1OF);
}

if (TIM_GetITStatus(TIM1, TIM_IT_CC2) != RESET)
{
TIM_ClearFlag(TIM1, TIM_IT_CC2);

if (TIM1->CCER & TIM_CCER_CC2P)
{
Falling[4] = TIM1->CCR2;
Ready[4] = 1;
TIM1->CCER &= ~TIM_CCER_CC2P;
}
else
{
Raising[4] = TIM1->CCR2;
TIM1->CCER |= TIM_CCER_CC2P;
}

if (TIM_GetFlagStatus(TIM1, TIM_FLAG_CC2OF) != RESET)
TIM_ClearFlag(TIM1, TIM_FLAG_CC2OF);
}

if (TIM_GetITStatus(TIM1, TIM_IT_CC3) != RESET)
{
TIM_ClearFlag(TIM1, TIM_IT_CC3);

if (TIM1->CCER & TIM_CCER_CC3P)
{
Falling[7] = TIM1->CCR3;
Ready[7] = 1;
TIM1->CCER &= ~TIM_CCER_CC3P;
}
else
{
Raising[7] = TIM1->CCR3;
TIM1->CCER |= TIM_CCER_CC3P;
}

if (TIM_GetFlagStatus(TIM1, TIM_FLAG_CC3OF) != RESET)
TIM_ClearFlag(TIM1, TIM_FLAG_CC3OF);
}

if (TIM_GetITStatus(TIM1, TIM_IT_CC4) != RESET)
{
TIM_ClearFlag(TIM1, TIM_IT_CC4);

if (TIM1->CCER & TIM_CCER_CC4P)
{
Falling[8] = TIM1->CCR4;
Ready[8] = 1;
TIM1->CCER &= ~TIM_CCER_CC4P;
}
else
{
Raising[8] = TIM1->CCR4;
TIM1->CCER |= TIM_CCER_CC4P;
}

if (TIM_GetFlagStatus(TIM1, TIM_FLAG_CC4OF) != RESET)
TIM_ClearFlag(TIM1, TIM_FLAG_CC4OF);
}
*/