#pragma once

#include <inttypes.h>
#include "def.h"
#include "conf.h"
#include "gui.h"
#include "guielement.h"
#include "guimodal.h"

struct NumDialogDataDef
{
	const char* Name;
	int16_t Value;
	int16_t Min;
	int16_t Default;
	int16_t Max;
  
	int32_t tag;
  
  void (*OKCallback)(int32_t tag, int16_t value);
  void (*Cancel)(void);
};

extern void ShowNumInput(NumDialogDataDef *data);