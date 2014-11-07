// XBeeReader.h

#ifndef _XBEEREADER_h
#define _XBEEREADER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "Arduino.h"
#else
	#include "WProgram.h"
#endif

#include "config.h"
#include "def.h"

#define START 0xFF
#define END 0xFC
#define XBEE_MODE_WAITING 0
#define XBEE_MODE_RECVLEN 1
#define XBEE_MODE_ONRECV 2
#define XBEE_MODE_RECVEND 3


class XBeeEvents
{
	public :
		void virtual OnData(uint16_t*, uint8_t) = 0;
		void virtual OnError() = 0;
};

class XBeeReader
{
 private:
	byte buffer[48];
	XBeeEvents* Events;

	uint8_t Mode;
	uint8_t Position;
	uint8_t Length;

 public:
	void init(XBeeEvents *events);
	void recv();
};

extern XBeeReader XBEEREADER;
void XBEE_SERIAL_EVENT();

#endif

