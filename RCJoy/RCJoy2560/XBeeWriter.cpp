// 
// 
// 

#include "XBeeWriter.h"

#ifdef XBEE

void XBeeWriter::init()
{
	XBEE_SERIAL.begin(XBEE_BAUD);
}

void XBeeWriter::send()
{
	static uint8_t buffer[40];

	XBEE_SERIAL.write((uint8_t)0xFF);
	XBEE_SERIAL.write((uint8_t)NUMBER_OF_CHANNELS);

	for(int i=0;i<NUMBER_OF_CHANNELS*2;i++)
	{
		uint8_t val = *((uint8_t*) PPM_DATA + i);
		if(val == 0xFF || val == 0xFC) val ^= 1;
		*(buffer+i) = val;
	}

	XBEE_SERIAL.write(buffer, NUMBER_OF_CHANNELS*2);
	XBEE_SERIAL.write((uint8_t)0xFC);
}

XBeeWriter XBEEWRITER;

#endif