

#include <Arduino.h>
#include "config.h"
#include "def.h"
#include "ppmgen.h"
#include "XBeeReader.h"

class XBEEPPMEvents : XBeeEvents
{
	void virtual OnData(uint16_t *data, uint8_t len)
	{
		if(len != NUMBER_OF_CHANNELS && len <= PPM_CHANNELS)
			setupChanelNumber(len);

		for(byte i=0; i<PPM_CHANNELS && i < len; i++)
			*(PPM_DATA + i) = *(data + i);
	}

	void virtual OnError()
	{
	
	}
};

XBEEPPMEvents PPMEV;

void setupChanelNumber(uint8_t num)
{
	NUMBER_OF_CHANNELS = num;
	FRAME_TOTAL_LENGTH = (2000 * NUMBER_OF_CHANNELS + FRAME_SYNC_LENGTH) * TIMER_SCALE;

}

void setup()
{
	setupChanelNumber(PPM_CHANNELS);

	PPMSetup();
	XBEEREADER.init((XBeeEvents*)&PPMEV);
	sei();
}

void loop()
{

}
