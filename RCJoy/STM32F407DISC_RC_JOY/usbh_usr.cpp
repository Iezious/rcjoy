/**
/**
******************************************************************************
* @file    usbh_usr.c
* @author  MCD Application Team and modified by Yuuichi Akagawa
* @version V2.0.0
* @date    22-July-2011, 2012/03/05
* @brief   This file includes the user application layer
******************************************************************************
* @attention
*
* THE PRESENT FIRMWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
* WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE
* TIME. AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY
* DIRECT, INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING
* FROM THE CONTENT OF SUCH FIRMWARE AND/OR THE USE MADE BY CUSTOMERS OF THE
* CODING INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
*
* <h2><center>&copy; COPYRIGHT 2011 STMicroelectronics</center></h2>
******************************************************************************
*/ 

/* Includes ------------------------------------------------------------------*/
#include <stddef.h>
#include "usbh_usr.h"
#include "usbh_hid_mouse.h"
#include "usbh_hid_keybd.h"
#include "stm32f4xx.h"
#include "stm32f4_discovery.h"
#include "StatusBox.h"
#include "comcom.h"
#include "menu.h"

extern USB_OTG_CORE_HANDLE           USB_OTG_Core_dev;
extern USBH_HOST                     USB_Host;
/*  Points to the DEVICE_PROP structure of current device */
/*  The purpose of this register is to speed up the execution */

USBH_Usr_cb_TypeDef USR_Callbacks =
{
	USBH_USR_Init,
	USBH_USR_DeInit,
	USBH_USR_DeviceAttached,
	USBH_USR_ResetDevice,
	USBH_USR_DeviceDisconnected,
	USBH_USR_OverCurrentDetected,
	USBH_USR_DeviceSpeedDetected,
	USBH_USR_Device_DescAvailable,
	USBH_USR_DeviceAddressAssigned,
	USBH_USR_Configuration_DescAvailable,
	USBH_USR_Manufacturer_String,
	USBH_USR_Product_String,
	USBH_USR_SerialNum_String,
	USBH_USR_EnumerationDone,
	USBH_USR_UserInput,
	NULL,
	USBH_USR_DeviceNotSupported,
	USBH_USR_UnrecoveredError
};


HID_cb_TypeDef HID_COMON_cb =
{
	USB_HID_DeviceInit,
	USB_HID_DeviceParseReport
};

/*const uint8_t MSG_HOST_INIT[]          = "Host Library Initialized";
const uint8_t MSG_DEV_ATTACHED[]       = "Device Attached";
const uint8_t MSG_DEV_DISCONNECTED[]   = "Device Disconnected";
*/
const uint8_t MSG_DEV_ERROR[]          = "Device fault";
const uint8_t MSG_DEV_NOTREC[]          = "Device type not allowed";

/**
* @brief  USBH_USR_Init 
*         Displays the message on LCD for host lib initialization
* @param  None
* @retval None
*/
void USBH_USR_Init(void)
{
	
}

/**
* @brief  USBH_USR_DeviceAttached 
*         Displays the message on LCD on device attached
* @param  None
* @retval None
*/
void USBH_USR_DeviceAttached(void)
{  
}

/**
* @brief  USBH_USR_UnrecoveredError
* @param  None
* @retval None
*/
void USBH_USR_UnrecoveredError (void)
{

}

/**
* @brief  USBH_DisconnectEvent
*         Device disconnect event
* @param  None
* @retval None
*/
void USBH_USR_DeviceDisconnected (void)
{
	LED3OFF;
}

/**
* @brief  USBH_USR_ResetUSBDevice 
*         Reset USB Device
* @param  None
* @retval None
*/
void USBH_USR_ResetDevice(void)
{
	/* Users can do their application actions here for the USB-Reset */
}


/**
* @brief  USBH_USR_DeviceSpeedDetected 
*         Displays the message on LCD for device speed
* @param  Devicespeed : Device Speed
* @retval None
*/
void USBH_USR_DeviceSpeedDetected(uint8_t DeviceSpeed)
{
	if(DeviceSpeed == HPRT0_PRTSPD_HIGH_SPEED)
	{
		return;
	}  
	else if(DeviceSpeed == HPRT0_PRTSPD_FULL_SPEED)
	{
		return;
	}
	else if(DeviceSpeed == HPRT0_PRTSPD_LOW_SPEED)
	{
		return;
	}
	else
	{
		STATUSBOX.SetData(SYSTEM, (char*)MSG_DEV_ERROR);
	}
}

/**
* @brief  USBH_USR_Device_DescAvailable 
*         Displays the message on LCD for device descriptor
* @param  DeviceDesc : device descriptor
* @retval None
*/
void USBH_USR_Device_DescAvailable(void *DeviceDesc)
{

}

/**
* @brief  USBH_USR_DeviceAddressAssigned 
*         USB device is successfully assigned the Address 
* @param  None
* @retval None
*/
void USBH_USR_DeviceAddressAssigned(void)
{

}


/**
* @brief  USBH_USR_Conf_Desc 
*         Displays the message on LCD for configuration descriptor
* @param  ConfDesc : Configuration descriptor
* @retval None
*/
void USBH_USR_Configuration_DescAvailable(USBH_CfgDesc_TypeDef * cfgDesc,
										  USBH_InterfaceDesc_TypeDef *itfDesc,
										  USBH_EpDesc_TypeDef *epDesc)
{
	USBH_InterfaceDesc_TypeDef *id;
	id = itfDesc;  

}

/**
* @brief  USBH_USR_Manufacturer_String 
*         Displays the message on LCD for Manufacturer String 
* @param  ManufacturerString : Manufacturer String of Device
* @retval None
*/
void USBH_USR_Manufacturer_String(void *ManufacturerString)
{

}

/**
* @brief  USBH_USR_Product_String 
*         Displays the message on LCD for Product String
* @param  ProductString : Product String of Device
* @retval None
*/
void USBH_USR_Product_String(void *ProductString)
{

}

/**
* @brief  USBH_USR_SerialNum_String 
*         Displays the message on LCD for SerialNum_String 
* @param  SerialNumString : SerialNum_String of device
* @retval None
*/
void USBH_USR_SerialNum_String(void *SerialNumString)
{

} 

/**
* @brief  EnumerationDone 
*         User response request is displayed to ask for
*         application jump to class
* @param  None
* @retval None
*/
void USBH_USR_EnumerationDone(void)
{
} 

/**
* @brief  USBH_USR_DeviceNotSupported
*         Device is not supported
* @param  None
* @retval None
*/
void USBH_USR_DeviceNotSupported(void)
{         
	LED3OFF;

	STATUSBOX.SetData(SYSTEM, (char*)MSG_DEV_NOTREC);
}  


USBH_USR_Status USBH_USR_UserInput(void)
{
	return USBH_USR_RESP_OK;
} 

/**
* @brief  USBH_USR_OverCurrentDetected
*         Device Overcurrent detection event
* @param  None
* @retval None
*/
void USBH_USR_OverCurrentDetected (void)
{

}

/**
* @brief  USBH_USR_DeInit
*         Deint User state and associated variables
* @param  None
* @retval None
*/
void USBH_USR_DeInit(void)
{
	LED3OFF;
}

void USB_HID_DeviceInit()
{
	LED3ON;
	STATUSBOX.SetStatus("Ready");
	ExecuteStartup();
}

uint8_t usb_buffer[64];

void USB_HID_DeviceParseReport(uint8_t *data)
{
	uint16_t l = CalcData->JoyReportLength;
	if(l>64) l = 64;

	for(int i=0; i<l; i++)
		*(usb_buffer+i) = *(data+i);

	ExecuteCommon();

	if (SYSMENU.MenuActive)
		ExecuteMenu();
	else
		ExecuteModel();
}

void SendUSBReportHeader()
{
	//for(int i=0;i<MAX_DATA_LENGTH;i++) USB_OTG_Core_dev.host.Rx_Buffer[i] = 0;

	if(	USBH_Get_HID_ReportDescriptor(&USB_OTG_Core_dev, &USB_Host, 512) == USBH_OK)
	{
		int l;

		for(l=MAX_DATA_LENGTH; USB_OTG_Core_dev.host.Rx_Buffer[l-1] == 0 && l; l--) ;
		COMCOM.Send(0x01, l, USB_OTG_Core_dev.host.Rx_Buffer);
	}
	else
		COMCOM.Send(0x01, 0, 0);


}