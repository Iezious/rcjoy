#pragma once

#include <inttypes.h>

class EEPRom
{
public:
	bool Init();

	bool Write(uint16_t addr, int16_t val);
	bool Write(uint16_t addr, int32_t val);

	bool Read(uint16_t addr, int16_t *val);
	bool Read(uint16_t addr, int32_t *val);
	bool ReadDirty(uint16_t addr, int16_t *val);
	bool ReadDirty(uint16_t addr, int32_t *val);

	bool GetNotSaved();
	bool GetError();
	void ForceSave();

	void Suspend();
	void Resume();
	bool GetBusy();
	void Ping();
};

#ifdef __cplusplus 
extern "C"
{
#endif

extern EEPRom EEPROM;


#ifdef __cplusplus 
}
#endif