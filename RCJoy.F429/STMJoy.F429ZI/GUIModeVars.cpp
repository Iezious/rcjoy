#include <stdio.h>
#include <inttypes.h>
#include "GUIModeVars.h"
#include "def.h"
#include "conf.h"
#include "GUIModeFlight.h"
#include "NumInputOverlay.h"
#include "Calculator.h"
#include "functions.h"
#include "EEPRom.h"

static void DrawScroll(GUIElementDef* elem);
static bool ClickScroll(GUIElementDef*, u16, u16);
static void DrawList(GUIElementDef* elem);
static bool ClickList(GUIElementDef*, u16, u16);
static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width);
static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY);
static bool ModeActivate();
static bool ListScrollUp();
static bool ListScrollDown();

#define MLHEIGHT 20
#define COLOR_NAME_EEP 0xFF77FF77
#define COLOR_NAME 0xFFDDDDDD
#define COLOR_VAL 0xFFFFFF33
#define FONT_NAME (&Font12)
#define FONT_VAL (&Font16)

static GUIElementDef VarsScroll =
{
	LCD_SCROLL_WIDTH,					//uint16_t Width;
	LCD_WORK_HEIGHT - 6,				//uint16_t Height;
	LCD_WORK_WIDTH,						//uint16_t Left;
	TABS_HEIGHT + 3,					//uint16_t Top;

	&DrawScroll,						//void(*Draw)(void);
	&ClickScroll,						//bool(*Click)(uint16_t x, uint16_t y);
	0,									//void(*Drag)(uint16_t x, uint16_t y);
	0          //tag
};


static GUIElementDef VarsList =
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

static GUIElementDef* Elemts[2] = { &VarsList, &VarsScroll };

GUIModeDef ModeVariables =
{
	0,						// Index
	35,						// Icon
	false,					// Active


	Elemts,									//	GUIElementDef **Elements;
	2,										//	uint8_t ElementsCount;

	&ModeActivate,							//bool(*Activate)(void);
	0,										//bool(*Deactivate)(void);
	0,										//bool(*Click)(uint16_t x, uint16_t y);
	0,										//bool(*Update)(void);
	&ListScrollUp,							//bool(*ScrollUp)(void);
	&ListScrollDown,						//bool(*ScrollDown)(void);
	0,										//bool(*ScrollTo)(uint16_t y);
	0,										//void(*Tick)(void);							
};

static ListDef VarsListBox =
{
	0,										//uint8_t Length;
	MLHEIGHT,								//uint8_t ItemHeight;
	0,										//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};

bool ModeActivate()
{
	if (!PROGRAM_PRESENT)
	{
		VarsListBox.Length = 0;
		return true;
	}

	u16 c = 0;
	for (u16 i = 0; i < CalcData->VariablesCount; i++)
		if (CalcData->Variables[i].MODEL_IDX == CurrentModelIdx) c++;

	VarsListBox.Length = c;
	VarsListBox.TopVisible = 0;
	return true;
}

bool ListScrollUp()
{
	return DoListScrollUp(&VarsListBox, &VarsList);
}

bool ListScrollDown()
{
	return DoListScrollDown(&VarsListBox, &VarsList);
}

void DrawScroll(GUIElementDef* elem)
{
	DrawScrollBar(&VarsScroll, VarsListBox.Length, VarsList.Height / VarsListBox.ItemHeight,
		VarsListBox.TopVisible);
}

static bool ClickScroll(GUIElementDef* s, u16 x, u16 y)
{
	return ScrollBarClick(y);
}

static void DrawList(GUIElementDef* elem)
{
	if (VarsListBox.Length != 0)
		DrawList(&VarsListBox, &VarsList);
	else
	{
		BSP_LCD_SetTextColor(COLOR_ERROR);
		BSP_LCD_SetFont(FONT_VAL);
		BSP_LCD_DisplayStringAt(elem->Width, elem->Top, (u8*)"No variables found", LEFT_MODE);
	}
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
	if (PROGRAM_PRESENT)
		ListClick(&VarsListBox, x, y);
}

static uint8_t buffer[32];

static VariableDef* GetModelVariable(uint16_t elementIndex)
{
	u16 idx = 0;
	u16 var_index = 0;

	for (; var_index < CalcData->VariablesCount; var_index++)
	{
		if (CalcData->Variables[var_index].MODEL_IDX == CurrentModelIdx)
		{
			if (idx == elementIndex) break;
			idx++;
		}
	}

	if (var_index == CalcData->VariablesCount) return NULL;

	return CalcData->Variables + var_index;

}

static void DrawListItem(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(left, top, width, MLHEIGHT);
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);

	VariableDef* var = GetModelVariable(elementIndex);
	if (!var) return;
	
	if (var->EEPROM_ADDR != 0)
		BSP_LCD_SetTextColor(COLOR_NAME_EEP);
	else
		BSP_LCD_SetTextColor(COLOR_NAME);

	BSP_LCD_SetFont(FONT_NAME);

	BSP_LCD_DisplayStringAt(left, top + 7, (u8*)var->Name, LEFT_MODE);

	BSP_LCD_SetTextColor(COLOR_VAL);
	BSP_LCD_SetFont(FONT_VAL);

	sprintf((char*)buffer, "%d", get(var->DATAMAP_ADDR));
	uint16_t l = __strlen((u8*)buffer);

	BSP_LCD_DisplayStringAt(
		left + width - l * FONT_VAL->Width - 2,
		top + 3, buffer, LEFT_MODE);
}

static void ValueEdited(int32_t tag, int16_t value);

static NumDialogDataDef VariableEditInfo =
{
	NULL,
	0, 0, 0, 0,
	0,

	&ValueEdited,
	NULL
};

static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY)
{
	VariableDef* var = GetModelVariable(elementIndex);
	if (!var) return true;

	VariableEditInfo.Name = var->Name;
	VariableEditInfo.Min = var->MIN;
	VariableEditInfo.Max = var->MAX;
	VariableEditInfo.Default = var->DEFAULT;
	VariableEditInfo.Value = get(var->DATAMAP_ADDR);
	VariableEditInfo.tag = elementIndex;

	ShowNumInput(&VariableEditInfo);

	return true;
}

static void ValueEdited(int32_t tag, int16_t value)
{
	VariableDef* var = GetModelVariable(tag);
	if (!var) return;

	set_val(var->DATAMAP_ADDR, value);

	if (var->EEPROM_ADDR)
		EEPROM.Write(var->EEPROM_ADDR, value);
}

