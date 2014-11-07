#pragma once

#include "I2C.h"

enum EEPRomAddressing
{
	A16Bit = 0,
	A8Bit = 1
};

class EEPRom
{
private :
	I2C*	__I2CPort;
	EEPRomAddressing __AddrMode;
	uint8_t __Addr;

public:
	bool Init(I2C* port, uint8_t addr, EEPRomAddressing addmode);
	
	bool Write(uint16_t addr, int16_t val);
	bool Write(uint16_t addr, int32_t val);

	bool Read(uint16_t addr, int16_t *val);
	bool Read(uint16_t addr, int32_t *val);
};


extern EEPRom EEPROM;