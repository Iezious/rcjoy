#ifndef __DEF_H__
#define __DEF_H__

#include <stdint.h>
#include <stdint-gcc.h>

#if STM32F40XX

#include <stm32f4xx.h>

#endif

#include "conf.h"
#include "I2C.h"


#ifdef LCDENABLED
#ifdef I2CLCD

#include "I2C_LCD.h"

#define LCD_TYPE I2C_LCD
#define LCDINIT &I2C_1, I2CLCDADDR, 16, 2
#endif

#ifdef GPIOLCD

#include "GPIO_LCD.h"
#define LCD_TYPE GPIO_LCD
#define LCDINIT 16, 2

#endif
#endif

#ifdef LCDENABLED

extern LCD_TYPE LCD;

#endif


#ifdef PPM_GEN

#include "PPMGenerator.h"

#endif

#endif

typedef int16_t axis_t;
typedef uint16_t idx_t;

typedef uint16_t u16;
typedef int16_t s16;

typedef uint8_t u8;
typedef int8_t s8;

typedef uint32_t u32;
typedef int32_t s32;

#ifdef STMF407VG
#define PROGRAM_BASE ((uint32_t)0x080E0000)
#endif

#ifdef STMF407VE
#define PROGRAM_BASE ((uint32_t)0x08060000)
#endif

#ifdef STM32F429ZI
#define PROGRAM_BASE ((uint32_t)0x080E0000)
#define PROGRAM_SECTOR 11
#endif


#define AXE_MIN 0
#define AXE_MAX 1000
#define AXE_MIDDLE 500