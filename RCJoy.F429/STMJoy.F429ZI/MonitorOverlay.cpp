#include <inttypes.h>
#include <math.h>
#include <stdio.h>
#include <inttypes.h>
#include "conf.h"
#include "def.h"
#include "gui.h"
#include "guimodal.h"
#include "ppmgenerator.h"

/*********************************************************************************************
/****   Definitions
/*****************/

#define COLOR_NAME 0xFFDDDDDD
#define COLOR_VAL 0xFFFFFF33
#define FONT_NAME (&Font12)
#define FONT_VAL (&Font16)


static void DrawList(GUIElementDef* elem);
static bool ClickList(GUIElementDef*, u16, u16);
static void TickOverlay(void);

static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width);
static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY);


/*********************************************************************************************
/****   Elements
/*****************/

static GUIElementDef ChannelsList =
{
	LCD_WORK_WIDTH,						//uint16_t Width;
	LCD_WORK_HEIGHT,					//uint16_t Height;
	0,									          //uint16_t Left;
	TABS_HEIGHT,						   //uint16_t Top;

	&DrawList,							//void(*Draw)(void);
	&ClickList,							//bool(*Click)(uint16_t x, uint16_t y);
	0,									//void(*Drag)(uint16_t x, uint16_t y);
	0
};



static GUIElelemntDef Elements[1] = { ChannelList };

ModalWindowDef MonitorDialog =
{
	Elements, 1, NULL, NULL, NULL, NULL, NULL
};

static ListDef ListBox =
{
	8, 										//uint8_t Length;
	24,	   							//uint8_t ItemHeight;
	0,					  					//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};


static void DrawList(GUIElementDef* elem)
{
		DrawList(&ListBox, &ChannelsList);
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
  GUI.HideModal();
  return true;
}


static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width)
{
  		u8 __buffer[8];
			
			BSP_LCD_SetTextColor(COLOR_NAME);
			BSP_LCD_SetFont(FONT_NAME);
			
			sprintf(__buffer, "%d:", elementIndex);
			BSP_LCD_DisplayStringAt(left+10, top+6, __buffer, LEFT_MODE);
			sprintf(__buffer, "%d:", elementIndex+8);
			BSP_LCD_DisplayStringAt(left+130, top+6, __buffer, LEFT_MODE);
			
			BSP_LCD_SetTextColor(COLOR_VALUE);
			BSP_LCD_SetFont(FONT_VALUE);
			
			sprintf(__buffer, "%d", PPMGen.getChannel(elementIndex));
			BSP_LCD_DisplayStringAt(left+10+NAME_FONT->Width*3, top+3, __buffer, LEFT_MODE);
			sprintf(__buffer, "%d", PPMGen.getChannel(elementIndex+8));
			BSP_LCD_DisplayStringAt(left+130+NAME_FONT->Width*3, top+3, __buffer, LEFT_MODE);
}

static void TickOverlay(void)
{
  DrawMode(&MonitorDialog);
}


static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY)
{
  return false;
}

void ShowChannelsMonitor()
{
  GUIRoot.ShowModal(&MonitorDialog);
}