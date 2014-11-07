#pragma once

#include <inttypes.h>

struct GUIElementDef
{
	uint16_t Width;
	uint16_t Height;
	uint16_t Left;
	uint16_t Top;

	void(*Draw)(GUIElementDef* elem);
	bool(*Click)(GUIElementDef* elem, uint16_t x, uint16_t y);
	void(*Drag)(uint16_t x, uint16_t y);
	
	uint32_t Tag;
};

struct ListDef
{
	uint8_t Length;
	uint8_t ItemHeight;
	uint8_t TopVisible;
	void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};

extern void DrawList(ListDef *list, GUIElementDef* gelem);
extern bool ListClick(ListDef *list, uint16_t x, uint16_t y);

extern void DrawScrollBar(GUIElementDef *sb, uint8_t total, uint8_t visible, uint8_t current_top);
extern bool ScrollBarClick(uint16_t y);

inline void ClickElements(GUIElementDef** elements, uint16_t count, uint16_t x, uint16_t y)
{
    for(uint8_t i=0; i < count; i++)
    {
        GUIElementDef *el = *((elements + i)); 
        
        if(
            (x < el->Left) || (x > el->Left + el->Width) ||
            (y < el->Top) || (y > el->Top + el->Height)
          ) continue;
          
		      if (el->Click && el->Click(el, x - el->Left, y - el->Top)) break;
    }

}

inline void DrawElements(GUIElementDef** elements, uint16_t count)
{
  for (uint16_t i = 0; i < count; i++)
	 {
	    GUIElementDef *el = elements[i];
		  if (el->Draw) el->Draw(el);
  }
}