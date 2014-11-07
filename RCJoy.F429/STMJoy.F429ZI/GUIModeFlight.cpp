#include <stdio.h>
#include "fonts.h"
#include "font12.h"
#include "fontPkts.h"
#include "font16.h"
#include "font20.h"
#include "stm32f429i_discovery_lcd.h"
#include "stm32f4xx_hal.h"
#include "def.h"
#include "GUI.h"
#include "guimodeflight.h"

#define SCREEN_CENTER 120

#define MODEL_FONT Font20
#define MODEL_COLOR LCD_COLOR_WHITE

#define FMODE_FONT Font16
#define FMODE_COLOR 0xFFCCCCCC

#define TRIMMER_BAR_COLOR 0xFFCCCCCC
#define TRIMMER_VAL_COLOR 0xFF77FF77

//#define MESSAGE_TOP 140
#define MESSAGE_FONT Font12
#define MESSAGE_COLOR 0xFFCCCCCC
#define VALUE_COLOR LCD_COLOR_YELLOW
#define MESSAGE_VISIBLE	5000

#define THCUT_FONT Font16
#define THCUT_COLOR 0xFFFF5555


static void DrawFlightScreen(void);
static void DrawModelScreen(GUIElementDef* elem);
static void DrawMessagElScreen(GUIElementDef* elem);
static void FlightModeTick();

static void DrawHTrimmer(GUIElementDef* elem);
static void DrawVTrimmer(GUIElementDef* elem);
static void DrawThrottleCut(GUIElementDef* elem);

GUIElementDef ModelInfoElem =
{
	SCREEN_WIDTH,								//Width;
	46,											//Height;
	0,											//Left;
	80,											//Top;

	&DrawModelScreen,							// Draw
	0,											// Click
	0,											// Drag
	0              // Tag
};

GUIElementDef MessageBoxElem = 
{
	 SCREEN_WIDTH,								//Width;
	 12,										//Height;
	 0,											//Left;
	 140,										//Top;

	 &DrawMessagElScreen,						// Draw
	 0,											// Click
  0,											// Drag
  0             // tag
};

GUIElementDef AileronTrimmerView =
{
	100,									//Width;
	15,										//Height;
	130,									//Left;
	300,									//Top;

	&DrawHTrimmer,  							// Draw
	0,											// Click
	0,											// Drag
	1              // Tag
};

GUIElementDef ElevatorTrimmerView =
{
	15,									//Width;
	100,								//Height;
	220,								//Left;
	190,								//Top;

	&DrawVTrimmer,  							// Draw
	0,											// Click
	0,											// Drag
	2              // Tag
};

GUIElementDef RudderTrimmerView =
{
	100,									//Width;
	15,										//Height;
	10,									//Left;
	300,	 								//Top;

	&DrawHTrimmer,  							// Draw
	0,											// Click
	0,											// Drag
	3              // Tag
};

GUIElementDef ThrottleTrimmerView =
{
	15,									//Width;
	100,								//Height;
	5, 									//Left;
	190,								//Top;

	&DrawVTrimmer,  							// Draw
	0,											// Click
	0,											// Drag
	4              // Tag
};

GUIElementDef ThrottleCutView =
{
	200,								//Width;
	24,								//Height;
	20, 									//Left;
	190,								//Top;

	&DrawThrottleCut,  							// Draw
	0,											// Click
	0,											// Drag
	4              // Tag
};

GUIElementDef* ModeFlightElements[7] = { &ModelInfoElem, &MessageBoxElem, &AileronTrimmerView, 
		&ElevatorTrimmerView, &RudderTrimmerView, &ThrottleTrimmerView, &ThrottleCutView };

GUIModeDef ModeFlight =
{
	0,						// Index
	34,						// Icon
	false,					// Active


	ModeFlightElements,						//	GUIElementDef **Elements;
	7,										//	uint8_t ElementsCount;

	0,										//bool(*Activate)(void);
	0,										//bool(*Deactivate)(void);
	0,										//bool(*Click)(uint16_t x, uint16_t y);
	0,										//bool(*Update)(void);
	0,										//bool(*ScrollUp)(void);
	0,										//bool(*ScrollDown)(void);
	0,										//bool(*ScrollTo)(uint16_t y);
	&FlightModeTick,						//void(*Tick)(void);
};
static volatile uint8_t* ModelName;
static volatile uint8_t* ModelMode;
static uint32_t VariableShown;

static void DrawModelName()
{
    BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(0, ModelInfoElem.Top, SCREEN_WIDTH, MODEL_FONT.Height);
    
    if(ModelName == NULL) return;

	u16 len = __strlen((u8*)ModelName);

    BSP_LCD_SetTextColor(MODEL_COLOR);
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetFont(&MODEL_FONT);
	BSP_LCD_DisplayStringAt(SCREEN_CENTER - len * MODEL_FONT.Width / 2, ModelInfoElem.Top, (u8 *)ModelName, LEFT_MODE);
}

static void DrawModelMode() 
{
    BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(0, ModelInfoElem.Top + 30, SCREEN_WIDTH, FMODE_FONT.Height);
    
    if(ModelMode == NULL) return;

	u16 len = __strlen((u8*)ModelMode);
    
    BSP_LCD_SetTextColor(FMODE_COLOR);
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetFont(&FMODE_FONT);
	BSP_LCD_DisplayStringAt(SCREEN_CENTER - len * FMODE_FONT.Width / 2, ModelInfoElem.Top + 30, (u8 *)ModelMode, LEFT_MODE);
}

static void DrawModelScreen(GUIElementDef* elem)
{
	DrawModelName();
	DrawModelMode();
}

static void ClearMessageBox()
{
    BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(0, MessageBoxElem.Top, SCREEN_WIDTH, MESSAGE_FONT.Height);
}

void SetModelName(const char* name)
{
    ModelName = (u8*)name;
    if(ModeFlight.Active)
        DrawModelName();
    
}
void SetModelMode(const char* name)
{
    ModelMode = (u8*)name;
    if(ModeFlight.Active)
        DrawModelMode();
}

void ChangeVar(const char* name, int16_t value)
{
    u8 val_buffer[32];
    if(!ModeFlight.Active) return;
    
    ClearMessageBox();
    
	sprintf((char*)val_buffer, " %d", value);
	uint16_t name_len = __strlen((u8*)name);
	uint16_t val_len = __strlen((u8*)val_buffer);
    
	uint16_t w = MESSAGE_FONT.Width * (name_len + val_len);
	uint16_t nl = SCREEN_CENTER - w / 2;
	uint16_t vl = nl + MESSAGE_FONT.Width * name_len;

	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetFont(&MESSAGE_FONT);
	BSP_LCD_SetTextColor(MESSAGE_COLOR);
	BSP_LCD_DisplayStringAt(nl, MessageBoxElem.Top, (u8*)name, LEFT_MODE);
	BSP_LCD_SetTextColor(VALUE_COLOR);
	BSP_LCD_DisplayStringAt(vl, MessageBoxElem.Top, val_buffer, LEFT_MODE);

	VariableShown = HAL_GetTick();
}

#define TRIMMERS_CONT 6
static int16_t trimmers[TRIMMERS_CONT] = { 0, 0, 0, 0, 0, 0 };
static uint8_t trimmers_changed[TRIMMERS_CONT] = { 0, 0, 0, 0, 0, 0 };

void SetTrimmerValue(u16 trim, s16 val)
{
	if (trim >= TRIMMERS_CONT) return;

	if (trimmers[trim] == val) return;
	trimmers_changed[trim] = 1;
	trimmers[trim] = val;
}

void ChangeVar(const char *name, const char *value)
{
	if (!ModeFlight.Active) return;

	ClearMessageBox();

	uint16_t name_len = __strlen((u8*)name);
	uint16_t val_len = __strlen((u8*)value);

	uint16_t w = MESSAGE_FONT.Width * (name_len + val_len);
	uint16_t nl = SCREEN_CENTER - w / 2;
	uint16_t vl = nl + MESSAGE_FONT.Width * name_len;

	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetFont(&MESSAGE_FONT);
	BSP_LCD_SetTextColor(MESSAGE_COLOR);
	BSP_LCD_DisplayStringAt(nl, MessageBoxElem.Top, (u8 *)name, LEFT_MODE);
	BSP_LCD_SetTextColor(VALUE_COLOR);
	BSP_LCD_DisplayStringAt(vl, MessageBoxElem.Top, (u8 *)value, LEFT_MODE);

	VariableShown = HAL_GetTick();
}

void ShowMessage(const char* message)
{
	if (!ModeFlight.Active) return;

	ClearMessageBox();
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetTextColor(MESSAGE_COLOR);
	BSP_LCD_SetFont(&MESSAGE_FONT);

	uint16_t val_w = __strlen((u8 *)message) * MESSAGE_FONT.Width;
	BSP_LCD_DisplayStringAt(SCREEN_CENTER - val_w / 2, MessageBoxElem.Top, (u8 *)message, LEFT_MODE);

	VariableShown = HAL_GetTick();
}


static void DrawMessagElScreen(GUIElementDef* elem)
{
	ClearMessageBox();
	VariableShown = 0;
}

static void DrawTrimmer(u8 i)
{
	switch (i)
	{
		case 1:		// aileron
			AileronTrimmerView.Draw(&AileronTrimmerView);
			break;

		case 2:
			ElevatorTrimmerView.Draw(&ElevatorTrimmerView);
			break;

		case 3:
			RudderTrimmerView.Draw(&RudderTrimmerView);
			break;

		case 4:
			ThrottleTrimmerView.Draw(&ThrottleTrimmerView);
			break;

		case 5:
			ThrottleCutView.Draw(&ThrottleCutView);
	}
}

static void DrawHTrimmer(GUIElementDef* elem)
{
	
	BSP_LCD_SetTextColor(TRIMMER_BAR_COLOR);
	BSP_LCD_FillRect(elem->Left, elem->Top, elem->Width, elem->Height);
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(elem->Left+2, elem->Top+2, elem->Width-4, elem->Height-4);

	BSP_LCD_SetTextColor(TRIMMER_VAL_COLOR);
	
	int32_t val = trimmers[elem->Tag];
	val = elem->Width/2 + (elem->Width * val / 1000); // sic! must be 2000 here!!! just to cut the big values
	if (val < 0) val = 0;
	if (val > elem->Width) val = elem->Width;

	BSP_LCD_DrawVLine(elem->Left + val, elem->Top, elem->Height);
}

static void DrawVTrimmer(GUIElementDef* elem)
{
	
	BSP_LCD_SetTextColor(TRIMMER_BAR_COLOR);
	BSP_LCD_FillRect(elem->Left, elem->Top, elem->Width, elem->Height);
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(elem->Left+2, elem->Top+2, elem->Width-4, elem->Height-4);

	BSP_LCD_SetTextColor(TRIMMER_VAL_COLOR);
	
	int32_t val = trimmers[elem->Tag];
	val = elem->Height / 2 + (elem->Height * val / 1000); // sic! must be 2000 here!!! just to cut the big values
	if (val < 0) val = 0;
	if (val > elem->Height) val = elem->Height;

	BSP_LCD_DrawHLine(elem->Left, elem->Top + val, elem->Width);
}

static void DrawThrottleCut(GUIElementDef* elem)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(elem->Left, elem->Top, elem->Width, elem->Height);

	if (!trimmers[5]) return;

	BSP_LCD_SetFont(&THCUT_FONT);
	BSP_LCD_SetTextColor(THCUT_COLOR);

	BSP_LCD_DisplayStringAt(
		elem->Left + (elem->Width - THCUT_FONT.Width * 13) / 2,
		elem->Top,
		(u8*)("THROTTLE CUT!"),
		LEFT_MODE);
}

static void FlightModeTick()
{
	for (u8 i = 1; i < TRIMMERS_CONT; i++)
	{
		if (trimmers_changed[i])
			DrawTrimmer(i);

		trimmers_changed[i] = 0;
	}

	if (!VariableShown) return; 
	if (HAL_GetTick() - VariableShown < MESSAGE_VISIBLE) return;
	ClearMessageBox();
	VariableShown = 0;
}
