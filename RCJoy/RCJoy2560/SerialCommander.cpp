#include "SerialCommander.h"


#ifdef DEBUG

CSerialCommander SCOM;

void CSerialCommander::Init()
{
	bstate = 0;
	bpos = 0;

//	DEBUG_SERIAL.begin(11500, SERIAL_8N1);
}

void CSerialCommander::Task()
{
	if(!Serial)	return;

	while(Serial.available())
	{
		int read = Serial.read();
		uint8_t bread = read & 0xFF;

		if(bread == 0xFF) 
		{
			if(bstate == 1)
			{
				bpos=0;
				bstate=2;
			}
			else 
			{
				bstate = 1;
			}
			continue;
		}

		if(bstate != 2)
		{
			continue;
		}

		buffer.buffer[bpos++] = bread;

		if(bpos == 8)
		{
			bstate = 0;
			ExecuteCommand();
			continue;
		}
	}
}

void CSerialCommander::WriteDebug()
{
	DEBUG_SERIAL.flush();

	DEBUG_SERIAL.print("MD");

	for(int i=0; i < DATA_LENGTH; i++)
	{
		DEBUG_SERIAL.print(" ");
		DEBUG_SERIAL.print(*(CalcData+i),HEX);
	}

	DEBUG_SERIAL.print("\r\n");
	DEBUG_SERIAL.flush();

	DEBUG_SERIAL.print("JD");

	for(int i=0;i<RPT_JOY_LEN;i++)
	{
		DEBUG_SERIAL.print(" ");
		DEBUG_SERIAL.print(*(READER.__CurrentData+i),HEX);
	}

	DEBUG_SERIAL.print("\r\n");
}

void CSerialCommander::ExecuteCommand()
{

}

#endif