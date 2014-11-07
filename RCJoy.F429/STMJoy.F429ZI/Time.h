#ifndef __TIME_H__
#define __TIME_H__

#include "stdint.h"
#include "stm32f4xx_hal.h"

#define delay(time_ms) HAL_Delay(time_ms)
#define milis HAL_GetTick

#ifdef __cplusplus
extern "C"
{
#endif



	extern void StartSysTimer();
	extern void SysTick_Handler();
	extern uint32_t milis();
	extern uint32_t micros();
	extern void delay(uint32_t);
	extern void spinWait(uint32_t mks);

#ifdef __cplusplus
}
#endif


#endif