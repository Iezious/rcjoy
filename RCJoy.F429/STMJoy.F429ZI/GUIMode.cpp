#include "guimode.h"
#include "GUI.h"
#include "stm32f429i_discovery_lcd.h"

void DrawHeader(GUIModeDef *mode)
{
    BSP_LCD_SetBackColor(MAIN_BACK_COLOR);
    BSP_LCD_SetFont(ICONS_FONT);
    
	if(mode->Active)
        BSP_LCD_SetTextColor(COLOR_ACTIVE);        
    else
        BSP_LCD_SetTextColor(COLOR_INACTIVE);

	BSP_LCD_DisplayChar(mode->Index * ICONS_FONT->Width, 0, mode->Icon); 

	BSP_LCD_SetTextColor(MAIN_FRONT_COLOR);
	
	if (mode->Active)
	{
		BSP_LCD_DrawHLine(mode->Index * ICONS_FONT->Width, 0, ICONS_FONT->Width);
		BSP_LCD_DrawVLine(mode->Index * ICONS_FONT->Width, 0, ICONS_FONT->Height);
		BSP_LCD_DrawVLine((mode->Index+1) * ICONS_FONT->Width - 1, 0, ICONS_FONT->Height);
	}
	else
	{
		BSP_LCD_DrawHLine(mode->Index * ICONS_FONT->Width, ICONS_FONT->Height-1, ICONS_FONT->Width);
	}
}

void ClickMode(GUIModeDef *mode, uint16_t x, uint16_t y)
{
    ClickElements(mode->Elements, mode->ElementsCount, x, y);
}

void DrawMode(GUIModeDef *mode)
{
  if (!mode->Active) return;
	 if (mode->Update && mode->Update()) return;
  
  DrawElements(mode->Elements, mode->ElementsCount); 
}

