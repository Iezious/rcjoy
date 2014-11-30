#include "usbh_hid_common.h"
#include "inttypes.h"
#include "usbh_def.h"



uint8_t usb_input_buffer[1024];
uint8_t usb_buffer[256];
uint8_t usb_repd_buffer[2048];
uint16_t usb_rep_desc_len;
uint16_t usb_rep_len;
volatile uint8_t usb_data_valid = FALSE;

uint16_t USB_Poll_Time = 4;

uint8_t Report_Total_Length;
uint8_t IsMultiReport;
volatile uint8_t CurrentReportShift = 0;
uint8_t HasReports = 0;

uint16_t VendorID;
uint16_t ProductID;

extern void ParseReportDescriptor(uint8_t *report, uint16_t len, uint16_t* bitlen, uint8_t *mreport);

#ifdef DEBUG_USB

#define DEBUG_BUFFER_LEN 8192

uint8_t debug_buffer[DEBUG_BUFFER_LEN];
volatile uint32_t debug_buffer_pos = 0;
volatile uint32_t debug_buffer_active = 0;

extern USBH_HandleTypeDef hUsbHostHS;

#endif

USBH_StatusTypeDef USBH_HID_CommonInit(USBH_HandleTypeDef *phost)
{
	HID_HandleTypeDef *HID_Handle = phost->pActiveClass->pData;

	HID_Handle->pData = usb_buffer;
	fifo_init(&HID_Handle->fifo, phost->device.Data, HID_QUEUE_SIZE * HID_Handle->length);

	VendorID = phost->device.DevDesc.idVendor;
	ProductID = phost->device.DevDesc.idProduct;
	USB_Poll_Time = HID_Handle->poll;
	HasReports = 0;
	IsMultiReport = 0;
	CurrentReportShift = 0;
}


void USBH_HID_EventCallback(USBH_HandleTypeDef *phost)
{
	HID_HandleTypeDef *HID_Handle = phost->pActiveClass->pData;
	uint16_t i;

	HasReports = 1;
	
	usb_rep_len = HID_Handle->length;

	if (HID_Handle->length == 0)
	{
		usb_data_valid = FALSE;
		CurrentReportShift = 0;
		return;
	}

	usb_data_valid = (fifo_read(&HID_Handle->fifo, usb_buffer, HID_Handle->length) == HID_Handle->length);

	if (!usb_data_valid)
	{
		CurrentReportShift = 0;
		return;
	}

	for (i = 0; i < HID_Handle->length; i++)
		usb_input_buffer[i + CurrentReportShift] = usb_buffer[i];

	CurrentReportShift += HID_Handle->length;

	if (CurrentReportShift >= Report_Total_Length)
		CurrentReportShift = 0;

#ifdef DEBUG_USB
	if (!debug_buffer_active) return;

	

	debug_buffer[0] = HID_Handle->length;

	for (i = 0; i < HID_Handle->length && debug_buffer_pos < DEBUG_BUFFER_LEN; i++, debug_buffer_pos++)
	{
		debug_buffer[debug_buffer_pos] = usb_buffer[i];
	}

	debug_buffer_active = (debug_buffer_pos < DEBUG_BUFFER_LEN);
#endif
}

void USB_HID_DataTimeoutCallBack(USBH_HandleTypeDef *phost)
{
	//CurrentReportShift = 0;
}

void USB_HID_ReportReadCallback(USBH_HandleTypeDef *phost)
{
	HID_HandleTypeDef *HID_Handle = phost->pActiveClass->pData;
	
	usb_rep_desc_len = HID_Handle->HID_Desc.wItemLength;
	uint16_t i;

	for (i = 0; i < usb_rep_desc_len; i++)
		*(usb_repd_buffer +i) = *(phost->device.Data + i);

	uint16_t replen_bits;

	ParseReportDescriptor(usb_repd_buffer, usb_rep_desc_len, &replen_bits, &IsMultiReport);
	Report_Total_Length = replen_bits >> 3;
}

uint8_t *USB_HID_GetLastReport()
{
	if (usb_data_valid)
		return usb_input_buffer;
	else
		return NULL;
}

uint16_t USB_HID_GetReportLength()
{
	return Report_Total_Length ? Report_Total_Length : usb_rep_len;
}

void USB_GetReportDescriptor(uint16_t *l, uint8_t **b)
{
	if (usb_rep_desc_len)
	{
		*l = usb_rep_desc_len;
		*b = usb_repd_buffer;
	}
	else
		*l = 0;
}

uint8_t CheckJoystick(uint16_t vendor, uint16_t product)
{
	return (VendorID == vendor) && (ProductID == product);
}

void GetJoyInfo(uint16_t *pVendor, uint16_t *pProduct)
{
	*pVendor = VendorID;
	*pProduct = ProductID;
}

#ifdef DEBUG_USB

void  USBStartCollectingDebug()
{
	uint16_t i;
	
	for (i = 0; i < DEBUG_BUFFER_LEN; i++)
		debug_buffer[i] = 0;

	debug_buffer_pos = 1;
	debug_buffer_active = 1;
}

void USBGetCollectedDebug(uint8_t** b, uint32_t *len)
{
	*len = DEBUG_BUFFER_LEN;
	*b = debug_buffer;
}

void USBGetStatuses(uint8_t *b)
{
	b[0] = hUsbHostHS.gState;
	b[1] = hUsbHostHS.EnumState;
	b[2] = hUsbHostHS.RequestState;
	b[3] = hUsbHostHS.Control.state;
	b[4] = hUsbHostHS.device.speed;

	if (hUsbHostHS.pActiveClass)
	{
		HID_HandleTypeDef *HID_Handle = hUsbHostHS.pActiveClass->pData;
		b[5] = HID_Handle->state;
		b[6] = HID_Handle->ctl_state;
		b[7] = Report_Total_Length;
		b[8] = IsMultiReport;
		b[9] = HasReports;
	}
	else
	{
		b[5] = 0;
		b[6] = 0;
		b[7] = 0;
		b[8] = 0;
		b[9] = 0;
	}
}

#endif
