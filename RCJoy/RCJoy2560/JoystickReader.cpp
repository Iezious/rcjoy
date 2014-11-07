// 
// 
// 

#include "JoystickReader.h"
#include "HidUniND.h"

JoystickReader READER;


void JoystickReader::Parse(HID *hid, bool is_rpt_id, uint8_t len, uint8_t *buf)
{
	bool match = true;

	// Checking if there are changes in report since the method was last called
	for (uint8_t i=0; i<RPT_JOY_LEN; i++) 
	{
		if( buf[i] != __CurrentData[i] ) 
		{
			match = false;
			break;
		}
	}

	// Calling Game Pad event handler
	if (match) return;

	for (uint8_t i=0; i<RPT_JOY_LEN; i++) 
	{
		__CurrentData[i] = buf[i];
	}

	ParseJoyData((const JoyUSBData*)__CurrentData);
	Calculate();

	((HIDNoDelay*)hid)->ForcePoll();
}


