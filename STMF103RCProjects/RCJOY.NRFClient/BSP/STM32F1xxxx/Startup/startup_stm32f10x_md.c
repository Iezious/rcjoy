/*
	This file contains the entry point (Reset_Handler) of your firmware project.
	The reset handled initializes the RAM and calls system library initializers as well as
	the platform-specific initializer and the main() function.
*/

extern void *_estack;

#define NULL ((void *)0)

void Reset_Handler();
void Default_Handler();

#define BootRAM ((void *)0xF108F85F)

void NMI_Handler()                __attribute__ ((weak, alias ("Default_Handler")));
void HardFault_Handler()          __attribute__ ((weak, alias ("Default_Handler")));
void MemManage_Handler()          __attribute__ ((weak, alias ("Default_Handler")));
void BusFault_Handler()           __attribute__ ((weak, alias ("Default_Handler")));
void UsageFault_Handler()         __attribute__ ((weak, alias ("Default_Handler")));
void SVC_Handler()                __attribute__ ((weak, alias ("Default_Handler")));
void DebugMon_Handler()           __attribute__ ((weak, alias ("Default_Handler")));
void PendSV_Handler()             __attribute__ ((weak, alias ("Default_Handler")));
void SysTick_Handler()            __attribute__ ((weak, alias ("Default_Handler")));
void WWDG_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void PVD_IRQHandler()             __attribute__ ((weak, alias ("Default_Handler")));
void TAMPER_IRQHandler()          __attribute__ ((weak, alias ("Default_Handler")));
void RTC_IRQHandler()             __attribute__ ((weak, alias ("Default_Handler")));
void FLASH_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void RCC_IRQHandler()             __attribute__ ((weak, alias ("Default_Handler")));
void EXTI0_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void EXTI1_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void EXTI2_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void EXTI3_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void EXTI4_IRQHandler()           __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel1_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel2_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel3_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel4_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel5_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel6_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void DMA1_Channel7_IRQHandler()   __attribute__ ((weak, alias ("Default_Handler")));
void ADC1_2_IRQHandler()          __attribute__ ((weak, alias ("Default_Handler")));
void USB_HP_CAN1_TX_IRQHandler()  __attribute__ ((weak, alias ("Default_Handler")));
void USB_LP_CAN1_RX0_IRQHandler() __attribute__ ((weak, alias ("Default_Handler")));
void CAN1_RX1_IRQHandler()        __attribute__ ((weak, alias ("Default_Handler")));
void CAN1_SCE_IRQHandler()        __attribute__ ((weak, alias ("Default_Handler")));
void EXTI9_5_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void TIM1_BRK_IRQHandler()        __attribute__ ((weak, alias ("Default_Handler")));
void TIM1_UP_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void TIM1_TRG_COM_IRQHandler()    __attribute__ ((weak, alias ("Default_Handler")));
void TIM1_CC_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void TIM2_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void TIM3_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void TIM4_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void I2C1_EV_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void I2C1_ER_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void I2C2_EV_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void I2C2_ER_IRQHandler()         __attribute__ ((weak, alias ("Default_Handler")));
void SPI1_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void SPI2_IRQHandler()            __attribute__ ((weak, alias ("Default_Handler")));
void USART1_IRQHandler()          __attribute__ ((weak, alias ("Default_Handler")));
void USART2_IRQHandler()          __attribute__ ((weak, alias ("Default_Handler")));
void USART3_IRQHandler()          __attribute__ ((weak, alias ("Default_Handler")));
void EXTI15_10_IRQHandler()       __attribute__ ((weak, alias ("Default_Handler")));
void RTCAlarm_IRQHandler()        __attribute__ ((weak, alias ("Default_Handler")));
void USBWakeUp_IRQHandler()       __attribute__ ((weak, alias ("Default_Handler")));

void * g_pfnVectors[0x43] __attribute__ ((section (".isr_vector"))) = 
{
	&_estack,
	&Reset_Handler,
	&NMI_Handler,
	&HardFault_Handler,
	&MemManage_Handler,
	&BusFault_Handler,
	&UsageFault_Handler,
	NULL,
	NULL,
	NULL,
	NULL,
	&SVC_Handler,
	&DebugMon_Handler,
	NULL,
	&PendSV_Handler,
	&SysTick_Handler,
	&WWDG_IRQHandler,
	&PVD_IRQHandler,
	&TAMPER_IRQHandler,
	&RTC_IRQHandler,
	&FLASH_IRQHandler,
	&RCC_IRQHandler,
	&EXTI0_IRQHandler,
	&EXTI1_IRQHandler,
	&EXTI2_IRQHandler,
	&EXTI3_IRQHandler,
	&EXTI4_IRQHandler,
	&DMA1_Channel1_IRQHandler,
	&DMA1_Channel2_IRQHandler,
	&DMA1_Channel3_IRQHandler,
	&DMA1_Channel4_IRQHandler,
	&DMA1_Channel5_IRQHandler,
	&DMA1_Channel6_IRQHandler,
	&DMA1_Channel7_IRQHandler,
	&ADC1_2_IRQHandler,
	&USB_HP_CAN1_TX_IRQHandler,
	&USB_LP_CAN1_RX0_IRQHandler,
	&CAN1_RX1_IRQHandler,
	&CAN1_SCE_IRQHandler,
	&EXTI9_5_IRQHandler,
	&TIM1_BRK_IRQHandler,
	&TIM1_UP_IRQHandler,
	&TIM1_TRG_COM_IRQHandler,
	&TIM1_CC_IRQHandler,
	&TIM2_IRQHandler,
	&TIM3_IRQHandler,
	&TIM4_IRQHandler,
	&I2C1_EV_IRQHandler,
	&I2C1_ER_IRQHandler,
	&I2C2_EV_IRQHandler,
	&I2C2_ER_IRQHandler,
	&SPI1_IRQHandler,
	&SPI2_IRQHandler,
	&USART1_IRQHandler,
	&USART2_IRQHandler,
	&USART3_IRQHandler,
	&EXTI15_10_IRQHandler,
	&RTCAlarm_IRQHandler,
	&USBWakeUp_IRQHandler,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	BootRAM
};

void SystemInit();
void __libc_init_array();
void main();

extern void *_sidata, *_sdata, *_edata;
extern void *_sbss, *_ebss;

#include "stm32f10x.h"

void __attribute__((naked, noreturn)) Reset_Handler()
{
	SCB->VTOR = (uint32_t)&g_pfnVectors;
	//Normally the CPU should will setup the based on the value from the first entry in the vector table.
	//If you encounter problems with accessing stack variables during initialization, ensure 
	//asm ("ldr sp, =_estack");

	void **pSource, **pDest;
	for (pSource = &_sidata, pDest = &_sdata; pDest != &_edata; pSource++, pDest++)
		*pDest = *pSource;

	for (pDest = &_sbss; pDest != &_ebss; pDest++)
		*pDest = 0;

	SystemInit();
	__libc_init_array();
	main();
	for (;;) ;
}

void __attribute__((naked, noreturn)) Default_Handler()
{
	//If we ended up here, an unexpected interrupt has happened (we did not defined the XXX_Handler function for it).
	//See the definition of g_pfnVectors above for interrupt handler names.
	for (;;) ;
}
