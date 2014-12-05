#include <inttypes.h>
#include "GUI.h"
#include "guielement.h"
#include "def.h"

static struct
{
	uint16_t total;
	uint16_t current;
	uint16_t visible;
} CurrentSBInfo;


bool ScrollBarClick(uint16_t y);

void DrawList(ListDef *list, GUIElementDef* gelem)
{
	if (list == NULL) return;

	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(gelem->Left, gelem->Top, gelem->Width, gelem->Height);

	uint16_t cnt = gelem->Height / (u16)list->ItemHeight;

	for (uint16_t i = 0; i < cnt; i++)
	{
		uint8_t curIdx = list->TopVisible + i;
		uint16_t top = gelem->Top + list->ItemHeight * i;

		if (curIdx >= list->Length) break;
		list->DrawElement(curIdx, gelem->Left, top, gelem->Width);
	}
}

bool ListClick(ListDef *list, uint16_t x, uint16_t y)
{
	uint16_t idx = y / list->ItemHeight;
	uint16_t el_y = y - idx * list->ItemHeight;
	idx += list->TopVisible;

	if (idx < 0 || idx > list->Length) return false;
	return list->ClickElement(idx, el_y, x);
}


void DrawScrollBar(GUIElementDef *sb, uint8_t total, uint8_t visible, uint8_t current_top)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);

	BSP_LCD_FillRect(sb->Left, sb->Top, sb->Width, sb->Height);

	if (total < visible) return;

	BSP_LCD_SetTextColor(MAIN_FRONT_COLOR);

	BSP_LCD_DrawVLine(sb->Left + sb->Width / 2, sb->Top, sb->Height);

	uint32_t h = sb->Height * (u32)visible / (u32)total;
	uint32_t t = sb->Height * (u32)current_top / (u32)total;

	if (h > LCD_WORK_HEIGHT) h = LCD_WORK_HEIGHT;
	if (h < MIN_SCROLL_HEIGHT) h = MIN_SCROLL_HEIGHT;
	if (t + h > LCD_WORK_HEIGHT) t = LCD_WORK_HEIGHT - h;

	CurrentSBInfo.current = current_top;
	CurrentSBInfo.total = total;
	CurrentSBInfo.visible = visible;

	BSP_LCD_DrawRect(sb->Left + 3, sb->Top + t, sb->Width - 6, h);
}



bool ScrollBarClick(uint16_t y)
{
	if (CurrentSBInfo.total == 0) return false;

	uint16_t h = LCD_WORK_HEIGHT * CurrentSBInfo.visible / CurrentSBInfo.total;
	uint16_t t = LCD_WORK_HEIGHT * CurrentSBInfo.current / CurrentSBInfo.total;

	if (h > LCD_WORK_HEIGHT) h = LCD_WORK_HEIGHT;
	if (h < MIN_SCROLL_HEIGHT) h = MIN_SCROLL_HEIGHT;
	if (t + h > LCD_WORK_HEIGHT) t = LCD_WORK_HEIGHT - h;

	if (y < t)
	{
		GUIRoot.ScrollUp();
		return true;
	}

	if (y >= t + h / 2)
	{
		GUIRoot.ScrollDown();
		return true;
	}

	return false;
}

bool DoListScrollUp(ListDef *list, GUIElementDef* elem)
{
	uint16_t total = elem->Height / list->ItemHeight;
	if (list->TopVisible < total)
		list->TopVisible = 0;
	else
		list->TopVisible -= total;

	GUIRoot.DrawContent();
	return true;
}

bool DoListScrollDown(ListDef *list, GUIElementDef* elem)
{
	uint16_t total = elem->Height / list->ItemHeight;
	list->TopVisible += total;

	if (list->TopVisible > list->Length - total)
		list->TopVisible = list->Length - total;

	GUIRoot.DrawContent();
	return true;
}

