#include <inttypes.h>

#include "stm32f4xx.h"
#include "stm32f4xx_hal_rcc.h"
#include "stm32f429i_discovery.h"
#include "stm32f429i_discovery_lcd.h"
#include "stm32f429i_discovery_ts.h"


#include "def.h"
#include "conf.h"
#include "GUI.h"

GUI GUIRoot;


#define LCD_FRAME_BUFFER_LAYER0                  (LCD_FRAME_BUFFER+0x130000)
#define LCD_FRAME_BUFFER_LAYER1                  LCD_FRAME_BUFFER
#define CONVERTED_FRAME_BUFFER                   (LCD_FRAME_BUFFER+0x260000)

static bool IsTouched = false;
static uint32_t lastTick = 0;
static uint32_t lastTouch = 0;
static uint32_t lastPB = 0;
static u8 IsNotSavedShown = 0;
static bool PassiveMode = true;
static bool PBPressed = false;

static TS_StateTypeDef  TS_State;


void GUI::Init(GUIModeDef **modes, uint16_t modes_count)
{
	Modes = modes;
	ModesCount = modes_count;
	for (u8 i = 0; i < ModesCount; i++) Modes[i]->Index = i;
	USBConnected = false;

	BSP_LCD_Init();
	BSP_LCD_SetColorKeying(1, LCD_COLOR_WHITE);
	BSP_LCD_LayerDefaultInit(0, LCD_FRAME_BUFFER_LAYER0);
	BSP_LCD_LayerDefaultInit(1, LCD_FRAME_BUFFER_LAYER1);
	BSP_LCD_SelectLayer(0);
	BSP_LCD_SetLayerVisible(1, DISABLE);
	BSP_LCD_SetLayerVisible(0, ENABLE);

	BSP_LCD_DisplayOn();

	BSP_LCD_Clear(MAIN_BACK_COLOR);

	BSP_TS_Init(BSP_LCD_GetXSize(), BSP_LCD_GetYSize());

	ActiveMode = *modes;
	ActivateMode(ActiveMode);
	BSP_PB_Init(BUTTON_KEY, BUTTON_MODE_GPIO);
}


void GUI::DrawHeader()
{
	BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(0, 0, SCREEN_WIDTH, TABS_HEIGHT);

	if (!PassiveMode)
	{
		BSP_LCD_SetTextColor(MAIN_FRONT_COLOR);
		BSP_LCD_DrawHLine(0, TABS_HEIGHT - 1, SCREEN_WIDTH);

		for (uint8_t i = 0; i < ModesCount; i++)
			::DrawHeader(*(Modes + i));
	}

	BSP_LCD_SetFont(ICONS_FONT);

	if (USBConnected)
	{
		BSP_LCD_SetTextColor(COLOR_ACTIVE);
		BSP_LCD_DisplayChar(SCREEN_WIDTH - ICONS_FONT->Width, 0, 38);
	}
	else
	{
		BSP_LCD_SetTextColor(MAIN_FRONT_COLOR);
		BSP_LCD_DisplayChar(SCREEN_WIDTH - ICONS_FONT->Width, 0, 32);
	}


	if (IsNotSavedShown & 2)
		BSP_LCD_SetTextColor(LCD_COLOR_RED);
	else if (IsNotSavedShown & 1)
		BSP_LCD_SetTextColor(LCD_COLOR_YELLOW);
	else
		BSP_LCD_SetTextColor(COLOR_INACTIVE);

	BSP_LCD_DisplayChar(SCREEN_WIDTH - ICONS_FONT->Width * 2, 0, 40);

	if (!PassiveMode)
	{
		BSP_LCD_SetTextColor(COLOR_ACTIVE);
		BSP_LCD_DrawHLine(SCREEN_WIDTH - ICONS_FONT->Width*2, TABS_HEIGHT - 1, ICONS_FONT->Width*2);
	}
}


static bool ProcessTouch()
{
	BSP_TS_GetState(&TS_State);
	if (TS_State.TouchDetected)
	{
		if (IsTouched) return false;
		IsTouched = true;

		return true;
	}

	IsTouched = false;
	return false;
}

static bool ProcessPB()
{
	if (BSP_PB_GetState(BUTTON_KEY) == RESET)
	{
		PBPressed = false;
		return false;
	}

	if (PBPressed) return false;

	PBPressed = true;
	return true;
}

static void TrySwitchTab(uint16_t x)
{
	u16 idx = x / ICONS_FONT->Width;
	GUIRoot.ActivateTab(idx);
}

void GUI::Tick()
{
	uint32_t now = HAL_GetTick();
	if (now - lastTick > 100)
	{
		for (u8 i = 0; i < ModesCount; i++)
			TickMode(*(Modes + i));

		lastTick = now;

		/* Wait for Tamper Button press before starting the Communication */
	}

	if (now - lastPB > 100)
	{
		lastPB = now;

		if (ProcessPB())
		{
			PassiveMode = !PassiveMode;
			
			ActivateTab(0);
			DrawHeader();
		}
	}

	if (now - lastTouch > 50)
	{
		if (!PassiveMode && ProcessTouch())
		{

			if (CurrentModal)
				ClickModal(CurrentModal, TS_State.X, TS_State.Y);
			else if (TS_State.Y < TABS_HEIGHT)
				TrySwitchTab(TS_State.X);
			else if (ActiveMode)
				ClickMode(ActiveMode, TS_State.X, TS_State.Y);

			lastTouch = now;
		}
	}
}

void GUI::ActivateTab(uint16_t idx)
{
	if (idx >= ModesCount) return;
	if (ActiveMode && ActiveMode->Index == idx) return;

	DeactivateMode(ActiveMode);
	ActiveMode = *(Modes + idx);
	ActivateMode(ActiveMode);
	BSP_LCD_Clear(MAIN_BACK_COLOR);

	CurrentModal = 0;
	Draw();
}

void GUI::SetNotSaved(bool hasChanges, bool error)
{
	u8 ns = 0;
	if (hasChanges) ns |= 1;
	if (error) ns |= 2;

	if (ns == IsNotSavedShown) return;

	IsNotSavedShown = ns;
	if (!CurrentModal) DrawHeader();
}

