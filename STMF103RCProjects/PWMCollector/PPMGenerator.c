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

inline s16 abs(s16 x)
{
	return x < 0 ? -x : x;
}

void timerInit()
{
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB | RCC_APB2Periph_AFIO, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	GPIO_PinRemapConfig(GPIO_PartialRemap_TIM3, ENABLE);


	TIM_TimeBaseInitTypeDef  TIM_TimeBaseStructure;

	TIM_TimeBaseStructure.TIM_Period = 1500;
	TIM_TimeBaseStructure.TIM_Prescaler = SystemCoreClock/1000000 - 1;
	TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);

	TIM_OCInitTypeDef  TIM_OCInitStructure;

	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;
	TIM_OCInitStructure.TIM_Pulse = PPM_PULSE;
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_Low;
	TIM_OC2Init(TIM3, &TIM_OCInitStructure);
	TIM_OC2PreloadConfig(TIM3, TIM_OCPreload_Enable);

	TIM_ARRPreloadConfig(TIM3, ENABLE);

	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);
	NVIC_SetPriority(TIM3_IRQn, 2);
	NVIC_EnableIRQ(TIM3_IRQn);
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

	TIM3->CCR2 = 0;
	TIM3->ARR = PPM_SYNC;
	TIM3->CNT = 0;

	TIM_Cmd(TIM3, ENABLE);
}

void stopPPMGenerator()
{
	TIM_Cmd(TIM3, DISABLE);
}

void setPPMGeneratorChannelCount(uint8_t channels)
{
	active_channels = channels;
	FRAME_TIME = CHAN_LENGTH * channels + PPM_SYNC;
}

void TIM3_IRQHandler()
{
	if (onsync)
	{
		onsync = 0;
		current_chan = 0;
		time_spent = 0;
	}

	if (current_chan < active_channels)
	{
		TIM3->ARR = *(channels + current_chan);
		TIM3->CCR2 = PPM_PULSE;
		time_spent += *(channels + current_chan);

		current_chan++;
	}
	else
	{
		onsync = 1;
		TIM3->ARR = FRAME_TIME - time_spent;
		TIM3->CCR2 = PPM_PULSE;
	}

	TIM_ClearITPendingBit(TIM3, TIM_IT_Update);
}