#include <stdio.h>
#include <stdint.h>
#include <stm32f4xx_gpio.h>
#include <stm32f4xx_rcc.h>

#ifdef __cplusplus
extern "C"
{
#endif

#include "usb_bsp.h"
#include "usbh_core.h"
#include "usbh_usr.h"
#include "usbh_hid_core.h"

#ifdef __cplusplus
}
#endif

#include "SysTimer.h"
#include "uart_debug.h"
#include "LiquidCrystal_I2C.h"
#include "GPIO_LCD.h"

#ifdef USB_OTG_HS_INTERNAL_DMA_ENABLED
#if defined ( __ICCARM__ ) /*!< IAR Compiler */
#pragma data_alignment=4
#endif
#endif /* USB_OTG_HS_INTERNAL_DMA_ENABLED */
__ALIGN_BEGIN USB_OTG_CORE_HANDLE           USB_OTG_Core_dev __ALIGN_END ;

#ifdef USB_OTG_HS_INTERNAL_DMA_ENABLED
#if defined ( __ICCARM__ ) /*!< IAR Compiler */
#pragma data_alignment=4
#endif
#endif /* USB_OTG_HS_INTERNAL_DMA_ENABLED */
__ALIGN_BEGIN USBH_HOST                     USB_Host __ALIGN_END ;


GPIO_LCD *LCD;

int main()
{
	StartSysTimer();

	GPIO_LCD* lcd = new GPIO_LCD(0,16,2);
	LCD = lcd;

	LCD->init();
	LCD->display();
	LCD->backlight();
	LCD->cursor();
	LCD->blink();
	//  delay(3000);

	LCD->setCursor(0,0);
	//LCD.write('H');
	//LCD.noBlink();
	LCD->printstr("Hello, world!");


	char buffer[40];

	for(;;)
	{
		sprintf(buffer, "%d", milis());
		LCD->setCursor(0,1);
		LCD->printstr(buffer);
		delay(1000);
	}

#if USBTEST


	uart_debug_init(115200);
	delay(100);

	DBG ("System Initializing...\n");
	delay(100);
	DBG ("Done!\n");
	DBG ("System Ready\n");

	/* Init Host Library */
	USBH_Init(&USB_OTG_Core_dev,
		USB_OTG_FS_CORE_ID,
		&USB_Host,
		&HID_cb,
		&USR_Callbacks);

	while (1)
	{
		/* Host Task handler */
		USBH_Process(&USB_OTG_Core_dev , &USB_Host);
		delay(3);
	}
#endif
}
