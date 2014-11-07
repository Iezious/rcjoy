#ifndef __SYSTIMER_H__
#define __SYSTIMER_H__

#include "stdint.h"

extern void SysTick_Handler();

#ifdef __cplusplus
extern "C" {
#endif

	void StartSysTimer();
	uint32_t milis();
	uint32_t micros();
	void delay(uint32_t);
	void spinWait(uint32_t mks);

#ifdef __cplusplus
}
#endif

#endif