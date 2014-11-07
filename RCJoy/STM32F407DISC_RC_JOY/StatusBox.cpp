#include <stdio.h>
#include "StatusBox.h"


#ifdef TEXTLCD

char linebuffer[40];
bool LCDPresent;

static const char* cleaner = "                ";
static const char* clean_d = " %d         ";
static const char* clean_s = " %s         ";

void StatusBox::Init()
{
	LCDPresent = true;
}

void StatusBox::SetStatus(const char *status)
{
	if(!LCDPresent) return;

	LCD.setCursor(0,0);
	LCD.printstr(status);
	LCD.printstr(cleaner);
}

void StatusBox::SetStatus(const char *status, const char *substatus)
{
	if (!LCDPresent) return;

	LCD.setCursor(0, 0);
	LCD.printstr(status);
	LCD.write(':');
	LCD.printstr(substatus);
	LCD.printstr(cleaner);
}

void StatusBox::SetData(StatusGroup group, const char *data)
{
	if(!LCDPresent) return;

	LCD.setCursor(0,1);
	LCD.printstr(data);
	LCD.printstr(cleaner);
}

void StatusBox::SetData(StatusGroup group, const char *data, uint16_t value)
{
	if(!LCDPresent) return;

	LCD.setCursor(0,1);
	LCD.printstr(data);
	
	sprintf(linebuffer, clean_d, value);
	LCD.printstr(linebuffer);
}

void StatusBox::SetData(StatusGroup group, const char *data, int16_t value)
{
	if(!LCDPresent) return;

	LCD.setCursor(0,1);
	LCD.printstr(data);
	
	sprintf(linebuffer, clean_d, value);
	LCD.printstr(linebuffer);
}

void StatusBox::SetData(StatusGroup group, const char *data, const char *value)
{
	if(!LCDPresent) return;

	LCD.setCursor(0,1);
	LCD.printstr(data);
	
	sprintf(linebuffer, clean_s, value);
	LCD.printstr(linebuffer);
}

void StatusBox::SetMenu(const char *section)
{
	if (!LCDPresent) return;

	LCD.clear();

	if (section)
	{
		LCD.setCursor(0, 0);
		LCD.printstr(section);
	}
}

void StatusBox::SetMenu(const char *section, const char* selection)
{
	if (!LCDPresent) return;

	LCD.clear();

	LCD.setCursor(0, 0);
	LCD.printstr(section);

	LCD.setCursor(0, 1);
	LCD.printstr(selection);
}

void StatusBox::SetMenu(const char *section, const char* selection, const char *value)
{
	if (!LCDPresent) return;

	LCD.clear();

	LCD.setCursor(0, 0);
	LCD.printstr(section);

	LCD.setCursor(0, 1);
	LCD.printstr(selection);

	sprintf(linebuffer, clean_s, value);
	LCD.printstr(linebuffer);
}

void StatusBox::SetMenu(const char *section, const char* selection, int16_t val)
{
	LCD.clear();

	LCD.setCursor(0, 0);
	LCD.printstr(section);

	LCD.setCursor(0, 1);
	LCD.printstr(selection);

	sprintf(linebuffer, clean_d, val);
	LCD.printstr(linebuffer);
}


#endif

#ifndef LCDENABLED

void StatusBox::Init()
{
}

void StatusBox::SetStatus(const char *status)
{
}

void StatusBox::SetData(StatusGroup group, const char *data)
{
}

void StatusBox::SetData(StatusGroup group, const char *data, uint16_t value)
{
}

void StatusBox::SetData(StatusGroup group, const char *data, const char *value)
{}

#endif