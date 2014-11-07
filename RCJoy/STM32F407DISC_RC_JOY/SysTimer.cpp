#include <stdint.h>
#include "SysTimer.h"

extern "C"
{
#include "stm32f4xx.h"
#include "stm32f4xx_rcc.h"
}

static volatile uint32_t __currentTime;
static volatile uint32_t __micros_divider;

void SysTick_Handler()
{
	__currentTime++;
}

void StartSysTimer()
{
	RCC_ClocksTypeDef RCC_Clocks; 
	RCC_GetClocksFreq(&RCC_Clocks);

	__micros_divider = RCC_Clocks.HCLK_Frequency / 1000000;

	/* set reload register */
	SysTick->LOAD  = ((RCC_Clocks.HCLK_Frequency / 1000)  & SysTick_LOAD_RELOAD_Msk) - 1;      

	/* set Priority for Systick Interrupt */
	NVIC_SetPriority (SysTick_IRQn, (1<<__NVIC_PRIO_BITS) - 1);  

	/* Load the SysTick Counter Value */
	SysTick->VAL   = 0;                 

	/* Enable SysTick IRQ and SysTick Timer */
	SysTick->CTRL  = SysTick_CTRL_CLKSOURCE_Msk |	SysTick_CTRL_TICKINT_Msk   | SysTick_CTRL_ENABLE_Msk;                    
}

uint32_t milis()
{
	return __currentTime;
}

uint32_t micros()
{
	return __currentTime * 1000UL + (SysTick->VAL / __micros_divider); 
}

void delay(uint32_t time_ms)
{
	uint32_t wait_to = __currentTime + time_ms;
	
	while (__currentTime < wait_to)
	{
		asm("nop;");
	}
}

void spinWait(uint32_t mks)
{
	uint32_t end = mks * __micros_divider;
	while (--end);
}