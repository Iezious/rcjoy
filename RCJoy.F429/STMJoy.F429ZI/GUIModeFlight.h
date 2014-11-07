#pragma once

#include <inttypes.h>
#include "guimode.h"
#include "guielement.h"

extern GUIModeDef ModeFlight;

extern void SetModelName(const char *name);
extern void SetModelMode(const char *name);
extern void ChangeVar(const char *name, int16_t value);
extern void ChangeVar(const char *name, const char *value);
extern void ShowMessage(const char* message);
extern void SetTrimmerValue(u16 trim, s16 val);