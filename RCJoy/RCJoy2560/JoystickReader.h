// JoystickReader.h

#ifndef _JOYSTICKREADER_H_
#define _JOYSTICKREADER_H_

#include <Arduino.h>
#include <avrpins.h>

#include "hid.h"

#include "config.h"
#include "Def.h"
#include "Joystick.h"

class JoystickReader : public HIDReportParser
{
  public:
	uint8_t __CurrentData[RPT_JOY_LEN];
	virtual void Parse(HID *hid, bool is_rpt_id, uint8_t len, uint8_t *buf);
};

extern JoystickReader READER;

#endif

