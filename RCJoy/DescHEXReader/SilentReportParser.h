// SilentReportParser.h

#ifndef _SILENTREPORTPARSER_h
#define _SILENTREPORTPARSER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "Arduino.h"
#else
	#include "WProgram.h"
#endif


#include <Arduino.h>
#include <avrpins.h>
#include <max3421e.h>
#include <usbhost.h>
#include <usb_ch9.h>
#include <Usb.h>

#include <hid.h>

class SilentReportParser : public HIDReportParser
{
 private:


 public:
	virtual void Parse(HID *hid, bool is_rpt_id, uint8_t len, uint8_t *buf);
};

#endif

