#pragma once

#include <stdint.h>
#include "hd44780.h"

#define En 0x04			// B00000100  // Enable bit
#define Rw 0x02			// B00000010  // Read/Write bit
#define Rs 0x01			// B00000001  // Register select bit


class LiquidCrystal_I2C
{
public:
	LiquidCrystal_I2C(uint8_t lcd_Addr,uint8_t lcd_cols,uint8_t lcd_rows);

	void begin(uint8_t cols, uint8_t rows, uint8_t charsize = LCD_5x8DOTS );
	void clear();
	void home();
	void noDisplay();
	void display();
	void noBlink();
	void blink();
	void noCursor();
	void cursor();
	void scrollDisplayLeft();
	void scrollDisplayRight();
	void printLeft();
	void printRight();
	void leftToRight();
	void rightToLeft();
	void shiftIncrement();
	void shiftDecrement();
	void noBacklight();
	void backlight();
	void autoscroll();
	void noAutoscroll(); 
	void createChar(uint8_t, uint8_t[]);
	void setCursor(uint8_t, uint8_t); 
	void init();

	void printstr(const char*);

	void write(uint8_t);
	void command(uint8_t);

private:
	
	void init_priv();
	void send(uint8_t, uint8_t);
	void write4bits(uint8_t);
	void expanderWrite(uint8_t);
	void pulseEnable(uint8_t);
	
	uint8_t _Addr;
	uint8_t _displayfunction;
	uint8_t _displaycontrol;
	uint8_t _displaymode;
	uint8_t _numlines;
	uint8_t _cols;
	uint8_t _rows;
	uint8_t _backlightval;
	
};

//extern LiquidCrystal_I2C *LCD;