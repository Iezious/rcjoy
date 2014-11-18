#include <stdio.h>
#include <inttypes.h>
#include "GUIModeOptions.h"
#include "def.h"
#include "conf.h"
#include "GUIModeFlight.h"
#include "ConfirmOverlay.h"
#include "MonitorOverlay.h"
#include "eeprom.h"


static void DrawList(GUIElementDef* elem);
static bool ClickList(GUIElementDef*, u16, u16);

static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width);
static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY);

#define MENU_COLOR #FFDDDDDD
#define MENU_FONT (&Font24)

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

	0,	    						//bool(*Activate)(void);
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
	4, 										//uint8_t Length;
	30,	   							//uint8_t ItemHeight;
	0,					  					//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};

static void DrawList(GUIElementDef* elem)
{
		DrawList(&ListBox, &List);
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
  ListClick(&VarsListBox, x, y);
  return true;
}

static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width)
{
  u8* __name = 0;
  
  switch(elementIndex)
  {
    case 0:
      __name = "Save data";
      break;
     
    case 1:
      __name = "Reset all variables";
      break;
      
    case 2:
      __name = "Reset model variables";
      break;
      
    case 3:
      __name = "Channels monitor";
      break;      
  }
  
  BSP_LCD_SetTextColor(MENU_COLOR);
  BSP_LCD_SetFont(MENU_FONT);
  BSP_LCD_DisplayStringAt(left+10, top+3, __name, LEFT_MODE); 
}

static void SaveData();
static void ResetVars();
static void ResetModelVars();

static bool ClickListItem(uint8_t cmd, uint16_t eX, uint16_t eY)
{
  switch(cmd)
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
  } 
}

static void SaveData()
{
  EEPRom.ForceSave();
  GUIRoot.ActivateTab(0); 
}

void DoResetVars(uint32_t tag)
{
  for(u16 i=0; i < CalcData->VariablesCount; i++)
  {
    if(tag && CalcData->Variables[i].ModelIndex != ActiveMOdel)
    {
      
      }
  }
}

static void ResetVars()
{
  ConfirmOvelayData confdata =
  {
     "Variables reset", "Reset all variables to default?", 0, &DoResetVars, 0
  };
  
  ShowConfirmDialog(&cnfdata);
}

static void ResetModelVars()
{
  ConfirmOvelayData confdata =
  {
     "Variables reset", "Reset model variables?", 1, &DoResetVars, 0
  };
  
  ShowConfirmDialog(&cnfdata);
}

