#include "PPMGenerator.h"

#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>
#include <stm32f10x_exti.h>
#include <misc.h>

#define PPM_TOTAL_CHANS 16
#define CHAN_LENGTH 2200UL
#define PPM_SYNC 4500
#define PPM_PULSE 300
#define ANTIGLITCH 10


volatile static uint32_t FRAME_TIME;

static uint16_t channels[PPM_TOTAL_CHANS];
volatile static uint16_t active_channels;
volatile static uint32_t time_spent;
volatile static uint16_t current_chan;
volatile static u8 onsync = 0;
/*
inline s16 abs(s16 x)
{
	return x < 0 ? -x : x;
}
*/
void timerInit()
{
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_TIM1, ENABLE);

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_AFIO, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = 1500;
	TIM_TimeBaseStructure.TIM_Prescaler = SystemCoreClock/1000000 - 1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseStructure.TIM_RepetitionCounter = 0;
	TIM_TimeBaseInit(TIM1, &TIM_TimeBaseStructure);

	TIM_OCInitTypeDef  TIM_OCInitStructure;

	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
	TIM_OCInitStructure.TIM_Pulse = PPM_PULSE;
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_Low;
	TIM_OC4Init(TIM1, &TIM_OCInitStructure);
	
	TIM_OC4PreloadConfig(TIM1, TIM_OCPreload_Enable);


	AFIO->MAPR &= ~((1 << 6) | (1 << 7));

	TIM_ARRPreloadConfig(TIM1, DISABLE);

	TIM_CtrlPWMOutputs(TIM1, ENABLE);

	TIM_ITConfig(TIM1, TIM_IT_Update, ENABLE);
	NVIC_SetPriority(TIM1_UP_IRQn, 2);
	NVIC_EnableIRQ(TIM1_UP_IRQn);
}

void initPPMGenerator(uint16_t chans)
{
	u8 i;
	for (i = 0; i<PPM_TOTAL_CHANS; i++) *(channels + i) = 1500;

	active_channels = chans;
	FRAME_TIME = CHAN_LENGTH * chans + PPM_SYNC;

	timerInit();
}

void setPPMGeneratorChan(uint16_t chan, uint32_t val)
{
	if (val > 800 && val < CHAN_LENGTH && abs((s16)*(channels + chan) - (s16)val) > ANTIGLITCH)
		*(channels + chan) = val;
	//if (val > 800 && val < CHAN_LENGTH)
}

void startPPMGenerator()
{
	current_chan = 0;
	time_spent = 0;
	onsync = 1;

	TIM1->CCR4 = 0;
	TIM1->ARR = PPM_SYNC;
	TIM1->CNT = 0;

	TIM_Cmd(TIM1, ENABLE);
}

void stopPPMGenerator()
{
	TIM_Cmd(TIM1, DISABLE);
}

void setPPMGeneratorChannelCount(uint8_t channels)
{
	active_channels = channels;
	FRAME_TIME = CHAN_LENGTH * channels + PPM_SYNC;
}

void TIM1_UP_IRQHandler()
{
	if (onsync)
	{
		onsync = 0;
		current_chan = 0;
		time_spent = 0;
	}

	if (current_chan < active_channels)
	{
		TIM1->ARR = *(channels + current_chan);
		TIM1->CCR4 = PPM_PULSE;
		time_spent += *(channels + current_chan);

		current_chan++;
	}
	else
	{
		onsync = 1;
		TIM1->ARR = FRAME_TIME - time_spent;
		TIM1->CCR4 = PPM_PULSE;
	}

	TIM_ClearITPendingBit(TIM1, TIM_IT_Update);
}