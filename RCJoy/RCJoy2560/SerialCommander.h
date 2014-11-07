#ifndef _SERIALCOMMANDER_H_
#define _SERIALCOMMANDER_H_

#include <Arduino.h>
#include "config.h"
#include "Def.h"
#include "datamap.h"
#include "JoystickReader.h"

#ifdef DEBUG

struct SerialCommand
{
	union 
	{
		uint8_t buffer[8];
		struct 
		{
			uint8_t command;
			uint8_t subcommand;
			uint16_t uargument1;
			union 
			{
				int32_t bargument;
				struct
				{
					int16_t sargument1;
					int16_t sargument2;
				};
			};
		};
	};		
};

class CSerialCommander
{
	private:
		SerialCommand buffer;
		uint8_t bpos;
		uint8_t bstate;

		void ExecuteCommand();
	public:
		void Init();
		void Task();
		void WriteDebug();
};

extern CSerialCommander SCOM;

#endif
#endif