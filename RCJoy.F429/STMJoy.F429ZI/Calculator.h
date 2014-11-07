#pragma once

#include <stdint.h>
#include "conf.h"
#include "def.h"
#include "CalcMap.h"

typedef struct  __attribute__((__packed__))
{
	char* ModelName;
	uint16_t* ModelCode;

	uint16_t PPMChannels;
	uint16_t PPMMin;
	uint16_t PPMCenter;
	uint16_t PPMMax;

	uint16_t ModesCount;
	uint16_t ModesStoreVariable;
	char** Modes;

	uint16_t MODEL_IDX;
	uint16_t __Align1__;

}  ModelDataDef;

typedef struct __attribute__((__packed__))
{
	char* Name;
	uint16_t EEPROM_ADDR;
	uint16_t DATAMAP_ADDR;
	uint16_t MODEL_IDX;
	int16_t INCREMENT;
	int16_t MIN;
	int16_t MAX;
	int16_t DEFAULT;
	uint16_t __Align1__;
} VariableDef;

#define LINK_INPUT			0x0001
#define LINK_OUTPUT			0x0002
#define LINK_TYPE_BUTTON	0x0010
#define LINK_TYPE_VALUE		0x0020
#define LINK_TYPE_AXIS		0x0040

typedef struct __attribute__((__packed__))
{
	char* Name;
	uint16_t DATAMAP_ADDR;
	uint16_t LINK_BITS;
} BlockLinkDef;

typedef struct __attribute__((__packed__))
{
	char* Name;
	BlockLinkDef* Links;
	VariableDef** Vars;

	uint16_t MODEL_IDX;
	uint16_t LinkCount;
	uint16_t VarsCount;
	uint16_t __Align1__;
} BlockDef;

typedef struct __attribute__((__packed__))
{
	uint8_t __Align1__;							// 00
	uint8_t JoyReportLength;					// 01
	uint8_t PPM_Length;							// 02
	uint8_t PH_Bits;							// 03

	uint8_t LCD_ADDR;							// 04
	uint8_t EEPROM_ADDR;						// 05
	uint8_t EEPROM_ASTYLE;						// 06
	uint8_t __Align2__;							// 07

	uint16_t DataMapLength;						// 08
	uint16_t StringsCount;						// 0A
	uint16_t ModelsCount;						// 0C
	uint16_t VariablesCount;					// 0E	
	uint16_t BlocksCount;						// 10
	uint16_t __Aling3__;						// 12
	
	char** STRINGS;								// 14
	ModelDataDef* Models;						// 18
	VariableDef* Variables;						// 1C
	BlockDef* Blocks;							// 20

	uint16_t* CommonCode;						// 24
	uint16_t* StartupCode;						// 28
	uint16_t* MenuCode;							// 2C

} CalcDataDef;


extern axis_t DATAMAP[DATAMAP_SIZE];

#define CalcData ((CalcDataDef *) PROGRAM_BASE)

#define PH_BITS_LCD		0x01
#define PH_BITS_PPM		0x02
#define PH_BITS_UART	0x04
#define PH_BITS_XBEE	0x08
#define PH_BITS_EEPROM	0x10
#define PH_BITS_LCDBL	0x20

#define BUTTON_PRESSED	0x03
#define BUTTON_DOWN		0x01

#define CHECK_PRESSED(idx) ((get(idx) & BUTTON_PRESSED) == BUTTON_PRESSED)
#define CHECK_DOWN(idx) ((get(idx) & BUTTON_DOWN) == BUTTON_DOWN)
#define STRING(idx) CalcData->STRINGS[idx]

#define EEP_MODEL 4


extern ModelDataDef* CurrentModel;
extern uint16_t CurrentModelIdx;
extern bool ProgramStopped;

extern void InitCalc();
extern void StopCalc();

extern void ExecuteStartup();
extern void ExecuteMenu();
extern void ExecuteCommon();
extern void ExecuteModel();

extern void SwitchCurrentModel(uint16_t idx);

#define PROGRAM_PRESENT (!ProgramStopped && (CalcData->__Align1__ != 0xFF))
