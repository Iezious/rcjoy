#include <stdio.h>
#include "def.h"
#include "GUIModelListMode.h"
#include "GUIModeFlight.h"
#include "Calculator.h"

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
#define MLINACTIVE 0xFFCCCCCC
#define MLACTIVE 0xFF77CC77
#define MLERROR COLOR_ERROR
#define MLFONT &Font16


static GUIElementDef ModelList =
{
	LCD_WORK_WIDTH,						//uint16_t Width;
	LCD_WORK_HEIGHT,					//uint16_t Height;
	0,									//uint16_t Left;
	TABS_HEIGHT,						//uint16_t Top;

	&DrawList,							//void(*Draw)(void);
	&ClickList,							//bool(*Click)(uint16_t x, uint16_t y);
	0									//void(*Drag)(uint16_t x, uint16_t y);
};

static GUIElementDef ModelScroll =
{
	LCD_SCROLL_WIDTH,					//uint16_t Width;
	LCD_WORK_HEIGHT - 6,				//uint16_t Height;
	LCD_WORK_WIDTH,						//uint16_t Left;
	TABS_HEIGHT + 3,					//uint16_t Top;

	&DrawScroll,						//void(*Draw)(void);
	&ClickScroll,						//bool(*Click)(uint16_t x, uint16_t y);
	0									//void(*Drag)(uint16_t x, uint16_t y);
};


static GUIElementDef* ModelSelectModeElements[2] = { &ModelList, &ModelScroll };

GUIModeDef ModelSelectMode =
{
	0,						// Index
	36,						// Icon
	false,					// Active


	ModelSelectModeElements,				//	GUIElementDef **Elements;
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

static ListDef ModelsListBox =
{
	0,										//uint8_t Length;
	MLHEIGHT,								//uint8_t ItemHeight;
	0,										//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};



bool ModeActivate()
{
	if (PROGRAM_PRESENT)
		ModelsListBox.Length = CalcData->ModelsCount;
	else
		ModelsListBox.Length = 0;

	return true;
}

bool ListScrollUp()
{
	uint16_t total = ModelList.Height / ModelsListBox.ItemHeight;
	if (ModelsListBox.TopVisible < total)
		ModelsListBox.TopVisible = 0;
	else
		ModelsListBox.TopVisible-=total;

	GUIRoot.DrawContent();
	return true;
}

bool ListScrollDown()
{
	uint16_t total = ModelList.Height / ModelsListBox.ItemHeight;
	ModelsListBox.TopVisible += total;

	if (ModelsListBox.TopVisible > ModelsListBox.Length - total)
		ModelsListBox.TopVisible = ModelsListBox.Length - total;

	GUIRoot.DrawContent();
	return true;
}

void DrawScroll(GUIElementDef* elem)
{
	DrawScrollBar(&ModelScroll, ModelsListBox.Length, ModelList.Height / ModelsListBox.ItemHeight, 
		ModelsListBox.TopVisible);
}

static bool ClickScroll(GUIElementDef* sender, u16 x, u16 y)
{
	return ScrollBarClick(y);
}

static void DrawList(GUIElementDef* elem)
{
	if (ModelsListBox.Length)
	{
		DrawList(&ModelsListBox, &ModelList);
	}
	else 
	{
		BSP_LCD_SetTextColor(MLERROR);
		BSP_LCD_SetFont(MLFONT);
		BSP_LCD_DisplayStringAt(elem->Width, elem->Top, (u8*)"No models found", LEFT_MODE);
	}
}

static bool ClickList(GUIElementDef* sender, u16 x, u16 y)
{
	if (ModelsListBox.Length)
		ListClick(&ModelsListBox, x, y);
}

//static uint8_t buffer[32];

static void DrawListItem(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(left, top, width, MLHEIGHT);
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetTextColor(elementIndex == CurrentModelIdx ? MLACTIVE : MLINACTIVE);
	BSP_LCD_SetFont(MLFONT);
	
	BSP_LCD_DisplayStringAt(left, top+3,(u8*) (CalcData->Models[elementIndex].ModelName), LEFT_MODE);
}

static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY)
{
	SwitchCurrentModel(elementIndex);
	GUIRoot.ActivateTab(ModeFlight.Index);

	return true;
}