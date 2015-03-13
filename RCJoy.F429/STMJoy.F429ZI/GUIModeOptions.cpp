#include <stdio.h>
#include <inttypes.h>
#include "GUIModeOptions.h"
#include "def.h"
#include "conf.h"
#include "GUIModeFlight.h"
#include "ConfirmOverlay.h"
#include "MonitorOverlay.h"
#include "eeprom.h"
#include "Calculator.h"
#include "CalcMap.h"
#include "functions.h"


static bool ActivateOptions();
static void DrawList(GUIElementDef* elem);
static bool ClickList(GUIElementDef*, u16, u16);

static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width);
static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY);

extern bool code_enabled;


#define MENU_COLOR 0xFFDDDDDD
#define MENU_FONT (&Font16)

static GUIElementDef List =
{
	LCD_WORK_WIDTH,						//uint16_t Width;
	LCD_WORK_HEIGHT,					//uint16_t Height;
	0,									//uint16_t Left;
	TABS_HEIGHT,						//uint16_t Top;

	&DrawList,							//void(*Draw)(void);
	&ClickList,							//bool(*Click)(uint16_t x, uint16_t y);
	0,									//void(*Drag)(uint16_t x, uint16_t y);
	0
};

static GUIElementDef* Elemts[2] = { &List };


GUIModeDef ModeOptions =
{
	0,						// Index
	46,						// Icon
	false,					// Active


	Elemts,									//	GUIElementDef **Elements;
	1,										//	uint8_t ElementsCount;

	&ActivateOptions,	    						//bool(*Activate)(void);
	0,										//bool(*Deactivate)(void);
	0,										//bool(*Click)(uint16_t x, uint16_t y);
	0,										//bool(*Update)(void);
	0,						    	//bool(*ScrollUp)(void);
	0,	     					//bool(*ScrollDown)(void);
	0,										//bool(*ScrollTo)(uint16_t y);
	0,										//void(*Tick)(void);							
};

static ListDef ListBox =
{
	code_enabled ? 4 : 6,					//uint8_t Length;
	30,	   									//uint8_t ItemHeight;
	0,					  					//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};

static bool ActivateOptions()
{
	ListBox.Length = code_enabled ? 4 : 6;
	ListBox.TopVisible = 0;
	return true;
}

static void DrawList(GUIElementDef* elem)
{
	DrawList(&ListBox, &List);
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
	ListClick(&ListBox, x, y);
	return true;
}

static void DrawListItem(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width)
{
	u8* __name = 0;

	switch (elementIndex)
	{
	case 0:
		__name = (u8*)"Save data";
		break;

	case 1:
		__name = (u8*)"Reset all variables";
		break;

	case 2:
		__name = (u8*)"Reset model variables";
		break;

	case 3:
		__name = (u8*)"Channels monitor";
		break;

	case 4:
		__name = (u8*)"Reset program area";
		break;

	case 5:
		__name = (u8*)"Full chip reset";
		break;
	}

	BSP_LCD_SetTextColor(MENU_COLOR);
	BSP_LCD_SetFont(MENU_FONT);
	BSP_LCD_DisplayStringAt(left + 10, top + 3, __name, LEFT_MODE);
}

static void SaveData();
static void ResetVars();
static void ResetModelVars();
static void ProgramReset();
static void ChipReset();


static bool ClickListItem(uint8_t cmd, uint16_t eX, uint16_t eY)
{
	switch (cmd)
	{
	case 0:
		SaveData();
		break;

	case 1:
		ResetVars();
		break;

	case 2:
		ResetModelVars();
		break;

	case 3:
		ShowChannelsMonitor();
		break;

	case 4:
		ProgramReset();
		break;

	case 5:
		ChipReset();
		break;
	}
}

static void SaveData()
{
	EEPROM.ForceSave();
	GUIRoot.ActivateTab(0);
}

void DoResetVars(uint32_t tag)
{
	for (u16 i = 0; i < CalcData->VariablesCount; i++)
	{
		VariableDef* __var = CalcData->Variables + i;
		if (tag && __var->MODEL_IDX != CurrentModelIdx) continue;

		u16 ea = __var->EEPROM_ADDR;
		u16 da = __var->DATAMAP_ADDR;

		set_val(da, __var->DEFAULT);
		if (ea)	EEPROM.Write(ea, __var->DEFAULT);
	}
}

static void ResetVars()
{
	ConfirmOvelayData confdata =
	{
		(u8*)"Variables reset", (u8*)"Reset all variables to default?", 0, &DoResetVars, 0
	};

	ShowConfirmDialog(&confdata);
}

static void ResetModelVars()
{
	ConfirmOvelayData confdata =
	{
		(u8*)"Variables reset", (u8*)"Reset model variables?", 1, &DoResetVars, 0
	};

	ShowConfirmDialog(&confdata);
}

static void DoResetProgram(uint32_t tag)
{
	if (HAL_FLASH_Unlock() != HAL_OK)
		return;

	FLASH_EraseInitTypeDef fes;
	fes.Sector = PROGRAM_SECTOR;
	fes.NbSectors = 1;
	fes.TypeErase = TYPEERASE_SECTORS;
	fes.VoltageRange = VOLTAGE_RANGE_3;

	u32 errors;

	if ((HAL_FLASHEx_Erase(&fes, &errors) != HAL_OK) || errors != 0xFFFFFFFF)
	{
		HAL_FLASH_Lock();
		return;
	}
}

static void ProgramReset()
{
	ConfirmOvelayData confdata =
	{
		(u8*)"Clear program?", (u8*)"Reset board when finished", 1, &DoResetProgram, 0
	};

	ShowConfirmDialog(&confdata);
}


static void DoFullReset(uint32_t tag)
{
	if (HAL_FLASH_Unlock() != HAL_OK)
		return;

	FLASH_EraseInitTypeDef fes;
	fes.Banks = FLASH_BANK_BOTH;
	fes.TypeErase = TYPEERASE_MASSERASE;
	fes.VoltageRange = VOLTAGE_RANGE_3;

	u32 errors;

	if ((HAL_FLASHEx_Erase(&fes, &errors) != HAL_OK) || errors != 0xFFFFFFFF)
	{
		HAL_FLASH_Lock();
		return;
	}
}

static void ChipReset()
{
	ConfirmOvelayData confdata =
	{
		(u8*)"Clear whole flash?", (u8*)"Reset after 1 minute.", 1, &DoFullReset, 0
	};

	ShowConfirmDialog(&confdata);
}