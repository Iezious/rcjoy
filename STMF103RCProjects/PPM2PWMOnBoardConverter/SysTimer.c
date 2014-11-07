#include <stm32f10x.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>


static volatile uint32_t __currentTime;
static volatile uint32_t __micros_divider;

uint32_t milis()
{
	return __currentTime / 1000;
}

uint32_t micros()
{
	return __currentTime;// *1000UL + ((SysTick->LOAD - SysTick->VAL) / __micros_divider);
}

void delay(uint32_t time_ms)
{
	uint32_t wait_to = __currentTime + time_ms * 1000;

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

void StartSysTimer()
{
	__micros_divider = SystemCoreClock / 1000000;

	/* set reload register */
	SysTick->LOAD = ((SystemCoreClock / 1000000)  & SysTick_LOAD_RELOAD_Msk) - 1;

	/* set Priority for Systick Interrupt */
	NVIC_SetPriority(SysTick_IRQn, 0); //(1 << __NVIC_PRIO_BITS) - 1);

	/* Load the SysTick Counter Value */
	SysTick->VAL = 0;

	/* Enable SysTick IRQ and SysTick Timer */
	SysTick->CTRL = SysTick_CTRL_CLKSOURCE_Msk | SysTick_CTRL_TICKINT_Msk | SysTick_CTRL_ENABLE_Msk;
}

void SysTick_Handler()
{
	__currentTime++;
}
