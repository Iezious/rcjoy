#ifndef USBH_HID_COMMON_H
#define USBH_HID_COMMON_H

#include "usbh_hid.h"
#include "conf.h"

#ifdef __cplusplus

extern "C" {
#endif

USBH_StatusTypeDef USBH_HID_CommonInit(USBH_HandleTypeDef *phost);
void USBH_HID_EventCallback(USBH_HandleTypeDef *phost);
void USB_HID_ReportReadCallback(USBH_HandleTypeDef *phost);
uint8_t *USB_HID_GetLastReport();
void USB_GetReportDescriptor(uint16_t *l, uint8_t **b);
uint16_t USB_HID_GetReportLength();

#ifdef DEBUG_USB

void  USBStartCollectingDebug();
void USBGetCollectedDebug(uint8_t** b, uint32_t *len);
void USBGetStatuses(uint8_t *b);

#endif

#ifdef __cplusplus
}

#endif

#endif
