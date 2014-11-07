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
} VariableDef;

typedef struct __attribute__((__packed__))
{
	uint8_t __Align1__;
	uint8_t JoyReportLength;
	uint8_t PPM_Length;
	uint8_t PH_Bits;

	uint8_t LCD_ADDR;
	uint8_t EEPROM_ADDR;
	uint8_t EEPROM_ASTYLE;
	uint8_t __Align2__;

	uint16_t DataMapLength;
	uint16_t StringsCount;
	uint16_t ModelsCount;
	uint16_t VariablesCount;
	
	char** STRINGS;
	ModelDataDef* Models;
	VariableDef* Variables;

	uint16_t* CommonCode;
	uint16_t* StartupCode;
	uint16_t* MenuCode;

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

#define EEP_MODEL 2


extern ModelDataDef* CurrentModel;
extern uint16_t CurrentModelIdx;

extern void ExecuteStartup();
extern void ExecuteMenu();
extern void ExecuteCommon();
extern void ExecuteModel();

extern void SwitchCurrentModel(uint16_t idx);

