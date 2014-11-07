
#include "SilentReportParser.h"
#include <max3421e.h>
#include <usbhost.h>
#include <usb_ch9.h>
#include <Usb.h>
#include <address.h>
#include <hid.h>
#include <hiduniversal.h>

#include <printhex.h>
#include <message.h>
#include <hexdump.h>
#include <parsetools.h>

#include "HexReportParser.h"
#include "SilentReportParser.h"
/*
class HIDUniversal2 : public HIDUniversal
{
public:
	HIDUniversal2(USB *usb) : HIDUniversal(usb) {};
	
protected:
	virtual uint8_t OnInitSuccessful();
};

uint8_t HIDUniversal2::OnInitSuccessful()
{
	uint8_t    rcode;
	HexReportParser                                Rpt;

	if (rcode = GetReportDescr(0, &Rpt))
	goto FailGetReportDescr2;

	return 0;


FailGetReportDescr2:
	USBTRACE("GetReportDescr2:");
	goto Fail;

Fail:
	Serial.println(rcode, HEX);
	Release();
	return rcode;
}
*/

USB                                             Usb;
//USBHub                                          Hub(&Usb);
HIDUniversal                                   Hid(&Usb);
SilentReportParser                           Parser;

String inputString = "";  
HexReportParser                                Rpt;

void setup()
{
  Serial.begin( 115200 );
  Serial.println("Start");

  if (Usb.Init() == -1)
	  Serial.println("OSC did not start.");
	  
  delay( 200 );

  if (!Hid.SetReportParser(0, &Parser))
	  ErrorMessage<uint8_t>(PSTR("SetReportParser"), 1  ); 
}

void loop()
{
	Usb.Task();
}

void parseCommand()
{
	if(inputString == "desc")
	{
		Hid.GetReportDescr(0, &Rpt);
		return;
	}

	if(inputString == "desc0")
	{
		Hid.GetReportDescr(0, &Rpt);
		return;
	}

	if(inputString == "desc1")
	{
		Hid.GetReportDescr(1, &Rpt);
		return;
	}

	if(inputString == "desc2")
	{
		Hid.GetReportDescr(2, &Rpt);
		return;
	}

	Serial.println(inputString);
}

void serialEvent()
{
	while (Serial.available()) 
	{
		char inChar = (char)Serial.read(); 
		

		if (inChar == '\n') 
		{
			parseCommand();
			inputString = "";
		} 
		else
			inputString += inChar;
	}
}
