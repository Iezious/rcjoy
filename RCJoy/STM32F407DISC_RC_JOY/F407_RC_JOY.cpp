#include <stdio.h>
#include <stm32f4xx_gpio.h>
#include <stm32f4xx_rcc.h>

#ifdef __cplusplus 
extern "C" 
{
#endif

#include "usb_bsp.h"
#include "usbh_core.h"
#include "usbh_hid_core.h"

#ifdef __cplusplus 
}
#endif

#include "usbh_usr.h"


#include "SysTimer.h"
#include "stm32f4_discovery.h"

#include "conf.h"
#include "def.h"
#include "StatusBox.h"
#include "ComCom.h"
#include "EEPRom.h"

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

I2C I2C_1;
I2C I2C_2;
ComCom COMCOM;
EEPRom EEPROM;
PPMGenerator PPMGen;

#ifdef LCDENABLED

LCD_TYPE LCD;

#endif

StatusBox STATUSBOX;

void test_sequence();
int main_sequence();

int main()
{
	StartSysTimer();
//	test_sequence();


	if(CalcData->__Align1__ == 0xFF)
	{
		test_sequence();
	}
	else
	{
		main_sequence();
	}
}


void test_sequence()
{
	init_leds();

	LED1OFF;
	LED2OFF;
	LED3OFF;
	LED4OFF;


#ifdef I2CLCD
	I2C_1.Init(I2C1, 100000UL);
	LCD.init(&I2C_1, I2CLCDADDR, 16, 2);
#endif

#ifdef GPIOLCD
	LCD.init(16, 2);
#endif

	LCD.display();
	LCD.backlight();
		
	STATUSBOX.Init();
	STATUSBOX.SetStatus("Initializing");

	I2C_2.Init(I2C2, 400000UL);
	if (EEPROM.Init(&I2C_2, 0x50, A8Bit))
		LED1ON;

	COMCOM.Start(57600);
	LED2ON;

	PPMGen.init(PPM_CHANNELS);

	PPMGen.start();
	delay(200);

//	PPMGen.setChan(0,1200);
//	PPMGen.setChan(3,1900);

//	EEPROM.Write(0x24,(s16)0xABC);
	
/*	int16_t val;
	EEPROM.Read(0x24, &val);
/*
	/* Init Host Library */
	USBH_Init(&USB_OTG_Core_dev,
		USB_OTG_FS_CORE_ID,				
		&USB_Host,
		&HID_cb,
		&USR_Callbacks);

	for(;;)
	{
		USBH_Process(&USB_OTG_Core_dev , &USB_Host);		
		COMCOM.Ping();
		delay(3);
	}
}

int main_sequence()
{
	init_leds();

	LED1OFF;
	LED2OFF;
	LED3OFF;
	LED4ON;

	if(CalcData->PH_Bits & PH_BITS_LCD)
	{
#ifdef I2CLCD
		I2C_1.Init(I2C1, 100000UL);
		LCD.init(&I2C_1, CalcData->LCD_ADDR ? CalcData->LCD_ADDR : I2CLCDADDR, 16, 2);
#else
		LCD.init(16, 2);
#endif
		LCD.display();

		if(CalcData->PH_Bits & PH_BITS_LCDBL)
			LCD.backlight();

		STATUSBOX.Init();
	}

	STATUSBOX.SetStatus("Initializing");

	if(CalcData->PH_Bits & PH_BITS_UART)
	{
		COMCOM.Start(57600);
		LED2ON;
	}

	if(CalcData->PH_Bits & PH_BITS_EEPROM)
	{
		I2C_2.Init(I2C2, 400000UL);
		if (EEPROM.Init(&I2C_2, CalcData->EEPROM_ADDR ? CalcData->EEPROM_ADDR : 0x50, A8Bit))
			LED1ON;
	}

	PPMGen.init(PPM_CHANNELS);

	delay(200);

//	ExecuteStartup();

	/* Init Host Library */
	USBH_Init(&USB_OTG_Core_dev,
		USB_OTG_FS_CORE_ID,
		&USB_Host,
		&HID_cb,
		&USR_Callbacks);

	for(;;)
	{
		USBH_Process(&USB_OTG_Core_dev , &USB_Host);		
		COMCOM.Ping();
		delay(3);
	}
}


