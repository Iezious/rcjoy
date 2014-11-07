// 
// 
// 

#include "XBeeReader.h"

void XBeeReader::init(XBeeEvents *evts)
{
	XBEE_SERIAL.begin(XBEE_BAUD);

	Events = evts;
	Mode = 0;
	Position = 0;
}

void XBeeReader::recv()
{
	while (XBEE_SERIAL.available())
	{
		int data = XBEE_SERIAL.read();
		if(data == -1) return;

		uint8_t bdata = data;

		if(bdata == START && Mode != XBEE_MODE_WAITING)
		{
			Events -> OnError();
			Mode = XBEE_MODE_WAITING;
		}

		switch (Mode)
		{

			case XBEE_MODE_WAITING:
				if(bdata != START) continue;

				Mode = XBEE_MODE_RECVLEN;
				Length = 0;

				break;

			case XBEE_MODE_RECVLEN:
				if(bdata < 16)
				{
					Mode = XBEE_MODE_ONRECV;
					Length = bdata;
					Position = 0;
				}
				else
				{
					Mode = XBEE_MODE_WAITING;
					Events -> OnError();
				}

				break;

			case XBEE_MODE_ONRECV:
				*(buffer + Position) = bdata;
				Position++;

				if(Position == Length*2)
				{
					Mode = XBEE_MODE_RECVEND;
				}
				else if(Position > Length*2)
				{
					Mode = XBEE_MODE_WAITING;
					Events -> OnError();
				}

				break;

			case XBEE_MODE_RECVEND:
				if(bdata == END)
				{
					Events -> OnData((uint16_t*)buffer, Length);			
				}
				else
				{
					Events -> OnError();
				}

				Mode = XBEE_MODE_WAITING;
				break;
		}
	}
}

XBeeReader XBEEREADER;

void XBEE_SERIAL_EVENT()
{
	XBEEREADER.recv();
}

