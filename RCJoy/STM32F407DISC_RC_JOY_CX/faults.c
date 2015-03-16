#include <stdint.h>

#include "usb_bsp.h"
#include "usb_hcd_int.h"
#include "usbh_core.h"


extern void USB_OTG_BSP_TimerIRQ (void);
extern USB_OTG_CORE_HANDLE          USB_OTG_Core_dev;
//extern USBH_HOST                    USB_Host;


void  TIM2_IRQHandler() { USB_OTG_BSP_TimerIRQ(); }


#ifdef USE_USB_OTG_FS
void OTG_FS_IRQHandler(void) {
#else
void OTG_HS_IRQHandler(void) {
#endif
	
  USBH_OTG_ISR_Handler(&USB_OTG_Core_dev);
}


void NMI_Handler()					 {}
void __attribute__((naked, noreturn)) HardFault_Handler()	{ for(;;) ; }
void __attribute__((naked, noreturn)) MemManage_Handler()	{ for(;;) ; }
void __attribute__((naked, noreturn)) BusFault_Handler()	{ for(;;) ; }
void __attribute__((naked, noreturn)) UsageFault_Handler()  { for(;;) ; }
void SVC_Handler()                   {}
void DebugMon_Handler()				 {}
void  PendSV_Handler()               {}
void  WWDG_IRQHandler()              {}
void  PVD_IRQHandler()               {}
void  TAMP_STAMP_IRQHandler()        {}
void  RTC_WKUP_IRQHandler()          {}
void  FLASH_IRQHandler()             {}
void  RCC_IRQHandler()               {}
void  EXTI0_IRQHandler()             {}
void  EXTI1_IRQHandler()             {}
void  EXTI2_IRQHandler()             {}
void  EXTI3_IRQHandler()             {}
void  EXTI4_IRQHandler()             {}
void  DMA1_Stream0_IRQHandler()      {}
void  DMA1_Stream1_IRQHandler()      {}
void  DMA1_Stream2_IRQHandler()      {}
void  DMA1_Stream3_IRQHandler()      {}
void  DMA1_Stream4_IRQHandler()      {}
void  DMA1_Stream5_IRQHandler()      {}
void  DMA1_Stream6_IRQHandler()      {}
void  ADC_IRQHandler()               {}
void  CAN1_TX_IRQHandler()           {}
void  CAN1_RX0_IRQHandler()          {}
void  CAN1_RX1_IRQHandler()          {}
void  CAN1_SCE_IRQHandler()          {}
void  EXTI9_5_IRQHandler()           {}
void  TIM1_BRK_TIM9_IRQHandler()     {}
void  TIM1_UP_TIM10_IRQHandler()     {}
void  TIM1_TRG_COM_TIM11_IRQHandler(){}
void  TIM1_CC_IRQHandler()           {}
void  TIM3_IRQHandler()              {}
void  TIM4_IRQHandler()              {}
void  I2C1_EV_IRQHandler()           {}
void  I2C1_ER_IRQHandler()           {}
void  I2C2_EV_IRQHandler()           {}
void  I2C2_ER_IRQHandler()           {}
void  SPI1_IRQHandler()              {}
void  SPI2_IRQHandler()              {}
void  USART1_IRQHandler()            {}
void  USART2_IRQHandler()            {}
void  USART3_IRQHandler()            {}
void  EXTI15_10_IRQHandler()         {}
void  RTC_Alarm_IRQHandler()         {}
void  OTG_FS_WKUP_IRQHandler()       {}
void  TIM8_BRK_TIM12_IRQHandler()    {}
void  TIM8_UP_TIM13_IRQHandler()     {}
void  TIM8_TRG_COM_TIM14_IRQHandler(){}
void  TIM8_CC_IRQHandler()           {}
void  DMA1_Stream7_IRQHandler()      {}
void  FSMC_IRQHandler()              {}
void  SDIO_IRQHandler()              {}
void  TIM5_IRQHandler()              {}
void  SPI3_IRQHandler()              {}
void  UART4_IRQHandler()             {}
void  UART5_IRQHandler()             {}
void  TIM6_DAC_IRQHandler()          {}
void  TIM7_IRQHandler()              {}
void  DMA2_Stream0_IRQHandler()      {}
void  DMA2_Stream1_IRQHandler()      {}
void  DMA2_Stream2_IRQHandler()      {}
void  DMA2_Stream3_IRQHandler()      {}
void  DMA2_Stream4_IRQHandler()      {}
void  ETH_IRQHandler()               {}
void  ETH_WKUP_IRQHandler()          {}
void  CAN2_TX_IRQHandler()           {}
void  CAN2_RX0_IRQHandler()          {}
void  CAN2_RX1_IRQHandler()          {}
void  CAN2_SCE_IRQHandler()          {}


void  DMA2_Stream5_IRQHandler()      {}
void  DMA2_Stream6_IRQHandler()      {}
void  DMA2_Stream7_IRQHandler()      {}
void  USART6_IRQHandler()            {}
void  I2C3_EV_IRQHandler()           {}
void  I2C3_ER_IRQHandler()           {}
void  OTG_HS_EP1_OUT_IRQHandler()    {}
void  OTG_HS_EP1_IN_IRQHandler()     {}
void  OTG_HS_WKUP_IRQHandler()       {}

void  DCMI_IRQHandler()              {}
void  CRYP_IRQHandler()              {}
void  HASH_RNG_IRQHandler()          {}
void  FPU_IRQHandler()               {}

