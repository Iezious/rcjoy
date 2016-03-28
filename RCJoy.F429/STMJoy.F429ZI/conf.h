#ifndef __CONF_H__
#define __CONF_H__
/*
#define LCDENABLED
#define TEXTLCD
//#define I2CLCD
#define GPIOLCD
#define I2CLCDADDR 0x27
#define LCD_BACKLIGHT_ON
*/

#define PPM_GEN
#define PPM_CHANNELS 8

#define DATAMAP_SIZE 2048
#define STRING_CONSTANTS_SIZE 64
#define VARIABLES_SIZE 64
#define MODELS_BLOCKS_SIZE 16

#ifdef DEBUG

#define DEBUG_USB 1

#endif 

#endif