// 
// 
// 

#include "LCD.h"

#ifdef LCD

void intLCD()
{
	LCD_SERIAL.begin(9600);
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x01);delay(10);
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x02);delay(10); 
	LCD_SERIAL.print("Started");
}

void lcdPrintLine1(const char* txt)
{
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x01);delay(10);
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x80);delay(10);
	LCD_SERIAL.print(txt);
}
void lcdPrintLine2(const char* txt, axis_t val)
{
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x80 + 64);delay(10);
	LCD_SERIAL.print(txt);
	LCD_SERIAL.print(":");
	LCD_SERIAL.print(val);
	LCD_SERIAL.print("              ");
}

void lcdPrintLine2(const char* txt, const char* val)
{
	LCD_SERIAL.write(0xFE);LCD_SERIAL.write(0x80 + 64);delay(10);
	LCD_SERIAL.print(txt);
	LCD_SERIAL.print(":");
	LCD_SERIAL.print(val);
	LCD_SERIAL.print("              ");
}

#endif