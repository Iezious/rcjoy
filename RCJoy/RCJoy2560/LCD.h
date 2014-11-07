// LCD.h

#ifndef _LCD_h
#define _LCD_h

#include "Arduino.h"
#include "config.h"
#include "Def.h"

#ifdef LCD

void intLCD();

void lcdPrintLine1(const char* txt);
void lcdPrintLine2(const char* txt, axis_t val);
void lcdPrintLine2(const char* txt, const char* val);

#endif

#endif

