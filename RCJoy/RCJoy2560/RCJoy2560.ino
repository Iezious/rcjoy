#include <arduino.h>

#include "config.h"
#include "def.h"

#include "Usb.h"
//#include <usbhub.h>
#include "hid.h"
#include "HidUniND.h"

#include "SerialCommander.h"
#include "Joystick.h"
#include "JoystickReader.h"
#include "datamap.h"
#include "ppmGen.h"
#include "XBeeWriter.h"

USB                                             Usb;
//USBHub                                          Hub(&Usb);
HIDUniversal                                    Hid(&Usb);
//HIDNoDelay                                    Hid(&Usb);


void setup()
{
	InitData();

#ifdef DEBUG
	Serial.begin(115200);
	SCOM.Init();
	Serial.println("Started");
	Serial.flush();
#endif 

#ifdef LCD
	intLCD();
#endif
	
	if (Usb.Init() == -1)
		Serial.println("OSC did not start.");

	delay( 200 );

	if (!Hid.SetReportParser(0, &READER))
		ErrorMessage<uint8_t>(PSTR("SetReportParser"), 1  ); 


	PPMSetup();
	XBEEWRITER.init();

	sei();

}

void loop()
{
	Usb.Task();	
}
