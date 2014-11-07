#pragma once


#include "conf.h"
#include "def.h"

enum StatusGroup { SYSTEM, VARIABLE, STATE, MODEL, TRIMMER, DATA_SWITCH };



class StatusBox
{
public:
	void Init();

	void SetStatus(const char *status);
	void SetStatus(const char *status, const char* substatus);

	void SetData(StatusGroup group, const char *data);
	void SetData(StatusGroup group, const char *data, uint16_t value);
	void SetData(StatusGroup group, const char *data, const char *value);
	void SetData(StatusGroup group, const char *data, int16_t value);

	void SetMenu(const char *section);
	void SetMenu(const char *section, const char* selection);
	void SetMenu(const char *section, const char* selection, const char *val);
	void SetMenu(const char *section, const char* selection, int16_t val);
};

extern StatusBox STATUSBOX;
extern LCD_TYPE LCD;
