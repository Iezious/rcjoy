#pragma once

#include "stm32f4xx.h"
#include "inttypes.h"
#include "stm32f4xx_hal_rcc.h"
#include "stm32f429i_discovery.h"
#include "stm32f429i_discovery_lcd.h"
#include "stm32f429i_discovery_ts.h"
#include "fonts.h"
#include "fontPkts.h"
#include "GUIMode.h"
#include "GUIModal.h"


#define MAIN_BACK_COLOR 0xFF000000
#define MAIN_FRONT_COLOR 0xFFFFFFFF

#define ICONS_FONT (&FontPkts)
#define COLOR_ACTIVE 0xFF77FF77
#define COLOR_INACTIVE 0xFFAAAAAA
#define COLOR_ERROR 0xFFCC7777

#define SCREEN_WIDTH 240
#define SCREEN_HEIGHT 320
#define LCD_SCROLL_WIDTH 20
#define TABS_HEIGHT 20
#define MIN_SCROLL_HEIGHT 10
#define LCD_WORK_WIDTH (SCREEN_WIDTH - LCD_SCROLL_WIDTH)
#define LCD_WORK_HEIGHT (SCREEN_HEIGHT - TABS_HEIGHT)

#define GUI_COLOR_DEFAULT 0xFFCCCCCC
#define GUI_COLOR_DARK 0xFF777777
#define GUI_COLOR_HL 0xFFFFFFFF
#define GUI_COLOR_GREEN 0xFF77FF77
#define GUI_COLOR_RED 0xFFFF7777

class GUI
{

private:
	GUIModeDef **Modes;
	uint16_t ModesCount;
	GUIModeDef *ActiveMode;
	ModalWindowDef *CurrentModal;
	bool USBConnected;

	void DrawHeader();

public:
	void Init(GUIModeDef **modes, uint16_t modes_count);

	void Draw()
	{
		if (CurrentModal)
		{
		  DrawModal(CurrentModal);
		}
		else
		{
			DrawHeader();
			if (ActiveMode) DrawMode(ActiveMode);
		}
	}

	void DrawContent()
	{
		if (CurrentModal)
			DrawModal(CurrentModal);
		else if (ActiveMode) 
			DrawMode(ActiveMode);
	}

	void SetUSBConnected(bool connected)
	{
		if (USBConnected == connected) return;
		USBConnected = connected;
		if (!CurrentModal) DrawHeader();
	}

	void ScrollUp()
	{
	  if(CurrentModal) ScrollUpModal(CurrentModal);
		 else if (ActiveMode) ScrollUpMode(ActiveMode);
	}

	void ScrollDown()
	{
	  if(CurrentModal) ScrollDownModal(CurrentModal);
		 else if (ActiveMode) ScrollDownMode(ActiveMode);
	}

	void ActivateTab(uint16_t idx);
	void Tick();
	void SetNotSaved(bool, bool);
	
	void ShowModal(ModalWindowDef* modal)
	{
	  CurrentModal = modal;
	  BSP_LCD_Clear(MAIN_BACK_COLOR);
	  Draw();
	}
	
	 void HideModal()
	 {
    CurrentModal = 0;
    BSP_LCD_Clear(MAIN_BACK_COLOR);
    Draw();
	 }
};

extern GUI GUIRoot;

inline uint32_t __strlen(uint8_t *str)
{
	uint32_t l = 0;
	while (*(str + l)) l++;
	return l;
}