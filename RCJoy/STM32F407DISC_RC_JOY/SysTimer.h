#ifndef __SYSTIMER_H__
#define __SYSTIMER_H__

#include "stdint.h"

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