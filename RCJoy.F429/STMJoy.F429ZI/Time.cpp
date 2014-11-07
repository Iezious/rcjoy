#include "inttypes.h"
#include "stm32f4xx_hal_rcc.h"
#include "stm32f4xx_hal.h"

extern "C"
{
	#include "stm32f4xx.h"
}


static volatile uint32_t __micros_divider = 168;


void StartSysTimer()
{
	uint32_t l = SysTick->LOAD;
	__micros_divider = (l+1) / 1000;
}

uint32_t micros()
{
	return HAL_GetTick() * 1000UL + (SysTick->VAL / __micros_divider);
}

void spinWait(uint32_t mks)
{
	uint32_t end = mks * __micros_divider;
	while (--end);
}