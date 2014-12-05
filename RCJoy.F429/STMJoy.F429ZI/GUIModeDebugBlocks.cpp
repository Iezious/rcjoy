#include <stdio.h>
#include <inttypes.h>
#include "GUIDebugBlocks.h"
#include "def.h"
#include "conf.h"
#include "GUIModeBlock.h"
#include "Calculator.h"
#include "functions.h"

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

static GUIElementDef BlocksScroll =
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


static GUIElementDef BlocksList =
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

static GUIElementDef* Elemts[2] = { &BlocksScroll, &BlocksList };

GUIModeDef ModeBlocks =
{
	0,						// Index
	37,						// Icon
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

static ListDef BlocksListBox =
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
		BlockListBox.Length = 0;
		return true;
	}

	u16 c = 0;
	for (u16 i = 0; i < CalcData->BlocksCount; i++)
		if (CalcData->Blocks[i].MODEL_IDX == CurrentModelIdx) c++;

	VarsListBox.Length = c;
	return true;
}

bool ListScrollUp()
{
	return DoListScrollUp(&BlocksListBox, &BlocksList);
}

bool ListScrollDown()
{
	return DoListScrollDown(&BlocksListBox, &BlocksList);
}

void DrawScroll(GUIElementDef* elem)
{
	DrawScrollBar(&BlocksScroll, BlocksListBox.Length, BlocksList.Height / BlocksListBox.ItemHeight,
		BlocksListBox.TopVisible);
}

static bool ClickScroll(GUIElementDef* s, u16 x, u16 y)
{
	return ScrollBarClick(y);
}

static void DrawList(GUIElementDef* elem)
{
	if (BlocksListBox.Length != 0)
		DrawList(&BlocksListBox, &BlocksList);
	else
	{
		BSP_LCD_SetTextColor(COLOR_ERROR);
		BSP_LCD_SetFont(FONT_VAL);
		BSP_LCD_DisplayStringAt(elem->Left, elem->Top, (u8*)"No blocks information", LEFT_MODE);
	}
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
	if (PROGRAM_PRESENT)
		ListClick(&BlocksListBox, x, y);
}

static uint8_t buffer[32];

static BlockDef* GetModelBlock(uint16_t elementIndex)
{
	u16 idx = 0;
	u16 var_index = 0;

	for (; var_index < CalcData->BlocksCount; var_index++)
	{
		if (CalcData->Blocks[var_index].MODEL_IDX == CurrentModelIdx)
		{
			if (idx == elementIndex) break;
			idx++;
		}
	}

	if (var_index == CalcData->BlocksCount) return NULL;

	return CalcData->Blocks + var_index;
}

static void DrawListItem(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(left, top, width, MLHEIGHT);
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);

	BlockDef* var = GetModelBlock(elementIndex);
	if (!var) return;
	
	BSP_LCD_SetTextColor(COLOR_NAME);
	BSP_LCD_SetFont(FONT_NAME);

	BSP_LCD_DisplayStringAt(left, top + 7, (u8*)var->Name, LEFT_MODE);
}


static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY)
{
	BlockDef* var = GetModelBlock(elementIndex);
	if (!var) return;
	
	ShowBlockDebug(var);
	return true;
}

