
#include <stdint.h>
#include <stm32f4xx.h>
#include <stm32f4xx_gpio.h>
//#include <stm32f4xx_fsmc.h>
#include <stm32f4xx_rcc.h>
//#include <stm32f4xx_dac.h>
//#include <stm32f4xx_i2c.h>

#include "GPIO_LCD.h"
#include "SysTimer.h"

static void gpio_init()
{
	GPIO_InitTypeDef GPIO_InitStructure;

	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOB | RCC_AHB1Periph_GPIOE, ENABLE);

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10 | GPIO_Pin_11 | GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_DOWN;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOE, &GPIO_InitStructure);

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10 | GPIO_Pin_11 | GPIO_Pin_12;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_DOWN;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);
	
	GPIOB->ODR = GPIOB->ODR & ~(GPIO_Pin_10 | GPIO_Pin_12) | GPIO_Pin_10;

}
/*
static void dac_init()
{
	//GPIO
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_DAC, ENABLE);

	GPIO_InitTypeDef GPIO_A4;

	GPIO_A4.GPIO_Mode = GPIO_Mode_AN;
	GPIO_A4.GPIO_Pin = GPIO_Pin_4;
	GPIO_A4.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_A4.GPIO_Speed = GPIO_Speed_50MHz;

	GPIO_Init(GPIOA, &GPIO_A4);

	//DAC

	DAC_InitTypeDef DAC_CH1;

	DAC_CH1.DAC_Trigger = DAC_Trigger_None;
	DAC_CH1.DAC_WaveGeneration = DAC_WaveGeneration_None;
	DAC_CH1.DAC_OutputBuffer = DAC_OutputBuffer_Enable;
	DAC_CH1.DAC_LFSRUnmask_TriangleAmplitude = 0;
	DAC_Init(DAC_Channel_1, &DAC_CH1);

	DAC_Cmd(DAC_Channel_1, ENABLE);
}
*/

static void gpio_send(uint8_t data, uint8_t isdata)
{
//	GPIOB->ODR &= ~GPIO_ODR_ODR_10;
	GPIOE->ODR = (isdata << 7) | (data << 8);
	spinWait(1);
	GPIOB->ODR &= ~GPIO_ODR_ODR_10;
	spinWait(5);
	GPIOB->ODR |= GPIO_ODR_ODR_10;
}

// When the display powers up, it is configured as follows:
//
// 1. Display clear
// 2. Function set: 
//    DL = 1; 8-bit interface data 
//    N = 0; 1-line display 
//    F = 0; 5x8 dot character font 
// 3. Display on/off control: 
//    D = 0; Display off 
//    C = 0; Cursor off 
//    B = 0; Blinking off 
// 4. Entry mode set: 
//    I/D = 1; Increment by 1
//    S = 0; No shift 
//
// Note, however, that resetting the Arduino doesn't reset the LCD, so we
// can't assume that its in that state when a sketch starts (and the
// LiquidCrystal constructor is called).

GPIO_LCD::GPIO_LCD(uint8_t lcd_Addr, uint8_t lcd_cols,uint8_t lcd_rows)
{
	_Addr = lcd_Addr;
	_cols = lcd_cols;
	_rows = lcd_rows;
	_backlightval = LCD_NOBACKLIGHT;
}

void GPIO_LCD::init()
{
	init_priv();
}

void GPIO_LCD::init_priv()
{
	gpio_init();
	_displayfunction = LCD_4BITMODE | LCD_1LINE | LCD_5x8DOTS;
	begin(_cols, _rows);  
}

void GPIO_LCD::begin(uint8_t cols, uint8_t lines, uint8_t dotsize) 
{
	if (lines > 1) {
		_displayfunction |= LCD_2LINE;
	}
	_numlines = lines;

	// for some 1 line displays you can select a 10 pixel high font
	if ((dotsize != 0) && (lines == 1)) {
		_displayfunction |= LCD_5x10DOTS;
	}

	// SEE PAGE 45/46 FOR INITIALIZATION SPECIFICATION!
	// according to datasheet, we need at least 40ms after power rises above 2.7V
	// before sending commands. Arduino can turn on way befer 4.5V so we'll wait 50
	delay(50); 
	_displayfunction |= LCD_8BITMODE;

	// Now we pull both RS and R/W low to begin commands
	//expanderWrite(_backlightval);	// reset expanderand turn backlight off (Bit 8 =1)
	//delay(1000);

	//put the LCD into 4 bit mode
	// this is according to the hitachi HD44780 datasheet
	// figure 24, pg 46
	/*
	// we start in 8bit mode, try to set 4 bit mode
	write4bits(0x03 << 4);
	spinWait(4500); // wait min 4.1ms

	// second try
	write4bits(0x03 << 4);
	spinWait(4500); // wait min 4.1ms

	// third go!
	write4bits(0x03 << 4); 
	spinWait(150);

	// finally, set to 4-bit interface
	write4bits(0x02 << 4); 
	*/

	// set # lines, font size, etc.
	command(LCD_FUNCTIONSET | _displayfunction);  

	// turn the display on with no cursor or blinking default
	_displaycontrol = LCD_DISPLAYON | LCD_CURSOROFF | LCD_BLINKOFF;
	display();

	// clear it off
	clear();

	// Initialize to default text direction (for roman languages)
	_displaymode = LCD_ENTRYLEFT | LCD_ENTRYSHIFTDECREMENT;

	// set the entry mode
	command(LCD_ENTRYMODESET | _displaymode);

	home();
}



/********** high level commands, for the user! */

void GPIO_LCD::clear()
{
	command(LCD_CLEARDISPLAY);// clear display, set cursor position to zero
	spinWait(2000);  // this command takes a long time!
}

void GPIO_LCD::home()
{
	command(LCD_RETURNHOME);  // set cursor position to zero
	spinWait(2000);  // this command takes a long time!
}

void GPIO_LCD::setCursor(uint8_t col, uint8_t row)
{
	const int row_offsets[] = { 0x00, 0x40, 0x14, 0x54 };
	command(LCD_SETDDRAMADDR | (col + row_offsets[row]));
	spinWait(50);
}

// Turn the display on/off (quickly)
void GPIO_LCD::noDisplay() 
{
	_displaycontrol &= ~LCD_DISPLAYON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

void GPIO_LCD::display() 
{
	_displaycontrol |= LCD_DISPLAYON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

// Turns the underline cursor on/off
void GPIO_LCD::noCursor() 
{
	_displaycontrol &= ~LCD_CURSORON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

void GPIO_LCD::cursor() 
{
	_displaycontrol |= LCD_CURSORON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

// Turn on and off the blinking cursor
void GPIO_LCD::noBlink() 
{
	_displaycontrol &= ~LCD_BLINKON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

void GPIO_LCD::blink() 
{
	_displaycontrol |= LCD_BLINKON;
	command(LCD_DISPLAYCONTROL | _displaycontrol);
	spinWait(50);
}

// These commands scroll the display without changing the RAM
void GPIO_LCD::scrollDisplayLeft(void) 
{
	command(LCD_CURSORSHIFT | LCD_DISPLAYMOVE | LCD_MOVELEFT);
	spinWait(50);
}

void GPIO_LCD::scrollDisplayRight(void) 
{
	command(LCD_CURSORSHIFT | LCD_DISPLAYMOVE | LCD_MOVERIGHT);
	spinWait(50);
}

// This is for text that flows Left to Right
void GPIO_LCD::leftToRight(void) 
{
	_displaymode |= LCD_ENTRYLEFT;
	command(LCD_ENTRYMODESET | _displaymode);
	spinWait(50);
}

// This is for text that flows Right to Left
void GPIO_LCD::rightToLeft(void) 
{
	_displaymode &= ~LCD_ENTRYLEFT;
	command(LCD_ENTRYMODESET | _displaymode);
	spinWait(50);
}

// This will 'right justify' text from the cursor
void GPIO_LCD::autoscroll(void) 
{
	_displaymode |= LCD_ENTRYSHIFTINCREMENT;
	command(LCD_ENTRYMODESET | _displaymode);
	spinWait(50);
}

// This will 'left justify' text from the cursor
void GPIO_LCD::noAutoscroll(void) 
{
	_displaymode &= ~LCD_ENTRYSHIFTINCREMENT;
	command(LCD_ENTRYMODESET | _displaymode);
	spinWait(50);
}

// Allows us to fill the first 8 CGRAM locations
// with custom characters
void GPIO_LCD::createChar(uint8_t location, uint8_t charmap[]) 
{
	location &= 0x7; // we only have 8 locations 0-7
	command(LCD_SETCGRAMADDR | (location << 3));
	spinWait(50);
	for (int i=0; i<8; i++) {
		write(charmap[i]);
	}
	spinWait(50);
}

// Turn the (optional) backlight off/on
void GPIO_LCD::noBacklight(void) 
{
	_backlightval=LCD_NOBACKLIGHT;
//	expanderWrite(0);
}

void GPIO_LCD::backlight(void) 
{
	_backlightval=LCD_BACKLIGHT;
//	expanderWrite(0);
}



/*********** mid level commands, for sending data/cmds */

void GPIO_LCD::command(uint8_t value) 
{
	gpio_send(value, 0);
}

void GPIO_LCD::write(uint8_t value) 
{
	gpio_send(value, 1);
}

/************ low level data pushing commands **********/

// write either command or data
void GPIO_LCD::send(uint8_t value, uint8_t mode) 
{
	gpio_send(value, mode);
}



void GPIO_LCD::printstr(const char *c)
{
	//This function is not identical to the function used for "real" I2C displays
	//it's here so the user sketch doesn't have to be changed 
	for(;*c;c++) write(*c);
	spinWait(50);
}

/*

#ifdef FSMC
#define LCD_REG8 (*((volatile uint8_t *) 0x60000000)) // RS = 0 
#define LCD_DAT8 (*((volatile uint8_t *) 0x60030000)) // RS = 1 



static void fsmc_init()
{
	FSMC_NORSRAMInitTypeDef FSMC_NORSRAMInitStructure;
	FSMC_NORSRAMTimingInitTypeDef p;
	GPIO_InitTypeDef GPIO_InitStructure;

	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD | RCC_AHB1Periph_GPIOE |RCC_AHB1Periph_GPIOC, ENABLE);
	// Шина данных , сигналы чтения записи NOE--> RD_TFT NWE--> WR_TFT 
	// PD.00(D2), PD.01(D3), PD.04(RD), PD.5(WR), PD.7 NE1(CS), PD.8(D13), PD.9(D14), PD.10(D15), 
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1| GPIO_Pin_4 |GPIO_Pin_5 | GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10|
		//	A16
		// PD.11(RS) PD.14(D0)	 PD.15(D1) 
		GPIO_Pin_11 | GPIO_Pin_12 | GPIO_Pin_14| GPIO_Pin_15;
	// настраиваем режим работы выводов 
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF; 
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_2MHz;	
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;	
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;	
	GPIO_Init(GPIOD, &GPIO_InitStructure);

	GPIO_PinAFConfig(GPIOD, GPIO_PinSource0, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource1, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource4, GPIO_AF_FSMC);	 // TFT_RD
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource5, GPIO_AF_FSMC);	 // TFT_WR
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource7, GPIO_AF_FSMC);	 // TFT_CS
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource8, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource9, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource10, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource11, GPIO_AF_FSMC);	 // TFT_RS
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource12, GPIO_AF_FSMC);	 // TFT_RS
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource14, GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOD, GPIO_PinSource15, GPIO_AF_FSMC);
	// шина данных PE.07(D4), PE.08(D5), PE.09(D6), PE.10(D7)
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10;
	GPIO_Init(GPIOE, &GPIO_InitStructure);	

	GPIO_PinAFConfig(GPIOE, GPIO_PinSource7 , GPIO_AF_FSMC);	
	GPIO_PinAFConfig(GPIOE, GPIO_PinSource8 , GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOE, GPIO_PinSource9 , GPIO_AF_FSMC);
	GPIO_PinAFConfig(GPIOE, GPIO_PinSource10 , GPIO_AF_FSMC);
	// Конфигурируем вывод управления подсветкой 
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	// разрешаем тактирование FSMC 
	RCC_AHB3PeriphClockCmd(RCC_AHB3Periph_FSMC, ENABLE);
	// настройка временных параметров контроллера 
	p.FSMC_AddressSetupTime = 5;
	p.FSMC_AddressHoldTime = 0;
	p.FSMC_DataSetupTime = 9;
	p.FSMC_BusTurnAroundDuration = 0;
	p.FSMC_CLKDivision = 0;
	p.FSMC_DataLatency = 0;
	p.FSMC_AccessMode = FSMC_AccessMode_A;
	// Color LCD configuration - LCD configured as follow:
	// - Data/Address MUX = Disable
	// - Memory Type = SRAM
	// - Data Width = 8bit
	// - Write Operation = Enable
	// - Extended Mode = Enable
	// - Asynchronous Wait = Disable 
	// настройка контроллера памяти 
	FSMC_NORSRAMInitStructure.FSMC_Bank = FSMC_Bank1_NORSRAM1;
	FSMC_NORSRAMInitStructure.FSMC_DataAddressMux = FSMC_DataAddressMux_Disable;
	FSMC_NORSRAMInitStructure.FSMC_MemoryType = FSMC_MemoryType_SRAM;
	FSMC_NORSRAMInitStructure.FSMC_MemoryDataWidth = FSMC_MemoryDataWidth_8b;
	FSMC_NORSRAMInitStructure.FSMC_BurstAccessMode = FSMC_BurstAccessMode_Disable;
	FSMC_NORSRAMInitStructure.FSMC_AsynchronousWait = FSMC_AsynchronousWait_Disable;
	FSMC_NORSRAMInitStructure.FSMC_WaitSignalPolarity = FSMC_WaitSignalPolarity_Low;
	FSMC_NORSRAMInitStructure.FSMC_WrapMode = FSMC_WrapMode_Disable;
	FSMC_NORSRAMInitStructure.FSMC_WaitSignalActive = FSMC_WaitSignalActive_BeforeWaitState;
	FSMC_NORSRAMInitStructure.FSMC_WriteOperation = FSMC_WriteOperation_Enable;
	FSMC_NORSRAMInitStructure.FSMC_WaitSignal = FSMC_WaitSignal_Disable;
	FSMC_NORSRAMInitStructure.FSMC_ExtendedMode = FSMC_ExtendedMode_Disable;
	FSMC_NORSRAMInitStructure.FSMC_WriteBurst = FSMC_WriteBurst_Disable;
	FSMC_NORSRAMInitStructure.FSMC_ReadWriteTimingStruct = &p;
	FSMC_NORSRAMInitStructure.FSMC_WriteTimingStruct = &p;
	FSMC_NORSRAMInit(&FSMC_NORSRAMInitStructure); 
	// Enable FSMC NOR/SRAM Bank1 
	FSMC_NORSRAMCmd(FSMC_Bank1_NORSRAM1, ENABLE);
}

#endif
*/