#include "PWMReader.h"

#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>
#include <stm32f10x_exti.h>
#include <misc.h>

#include "SysTimer.h"
#include "PPMGenerator.h"

static uint32_t ChannelTimes[CHANNELS_COUNT];

void cleanArrays()
{
	u16 i, j;
	for (i = 0; i < CHANNELS_COUNT; i++)
	{
		ChannelTimes[i] = 0;
	}
}

void prepareGPIO()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2ENR_AFIOEN, ENABLE);
	
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2 | GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
	
}

#define EXTILINES EXTI_Line0 | EXTI_Line1 | EXTI_Line2 | EXTI_Line3 | EXTI_Line4 | EXTI_Line5 | EXTI_Line6 | EXTI_Line7

void ReadPWM()
{

}

void enableEXTI()
{
//	EXTI_DeInit();

	EXTI_InitTypeDef pref;
	pref.EXTI_Line = EXTILINES;
	pref.EXTI_LineCmd = ENABLE;
	pref.EXTI_Mode = EXTI_Mode_Interrupt;
	pref.EXTI_Trigger = EXTI_Trigger_Rising;

	EXTI_Init(&pref);

	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource0);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource1);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource2);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource3);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource4);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource5);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource6);
	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA, GPIO_PinSource7);

	
	NVIC_InitTypeDef NVIC_InitStructure;

	NVIC_InitStructure.NVIC_IRQChannel = EXTI0_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	NVIC_InitStructure.NVIC_IRQChannel = EXTI1_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	NVIC_InitStructure.NVIC_IRQChannel = EXTI2_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	NVIC_InitStructure.NVIC_IRQChannel = EXTI3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
	
	NVIC_InitStructure.NVIC_IRQChannel = EXTI4_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
	
	NVIC_InitStructure.NVIC_IRQChannel = EXTI9_5_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);
}

void initCapture()
{
	prepareGPIO();
	cleanArrays();
}

void startCapture()
{
	enableEXTI();
}

static inline void ChannelEvent(u8 chan)
{
	if (EXTI->RTSR & (1 << chan))
	{
		ChannelTimes[chan] = micros();

		EXTI->RTSR &= ~(1 << chan);
		EXTI->FTSR |= (1 << chan);
	}
	else
	{
		setPPMGeneratorChan(chan, micros() - ChannelTimes[chan]);

		EXTI->FTSR &= ~(1 << chan);
		EXTI->RTSR |= (1 << chan);
	}

	EXTI_ClearITPendingBit(1 << chan);
}

void EXTI0_IRQHandler()
{
	ChannelEvent(0);
}

void EXTI1_IRQHandler()
{
	ChannelEvent(1);
}

void EXTI2_IRQHandler()
{
	ChannelEvent(2);
}

void EXTI3_IRQHandler()
{
	ChannelEvent(3);
}

void EXTI4_IRQHandler()
{
	ChannelEvent(4);
}

void EXTI9_5_IRQHandler()
{
	if (EXTI->PR & (1 << 5))
		ChannelEvent(5);

	if (EXTI->PR & (1 << 6))
		ChannelEvent(6);

	if (EXTI->PR & (1 << 7))
		ChannelEvent(7);
}