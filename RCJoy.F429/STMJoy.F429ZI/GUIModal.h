#pragma once

#include <inttypes.h>
#include "GUIElement.h"

struct ModalWindowDef
{
	GUIElementDef **Elements;
	uint8_t ElementsCount;

	void (*Draw)(void);

	bool(*Click)(uint16_t x, uint16_t y);
	bool(*ScrollUp)(void);
	bool(*ScrollDown)(void);
};


extern void DrawModal(ModalWindowDef* w);

inline void ClickModal(ModalWindowDef* w, uint16_t x, uint16_t y)
{
	if (w->Click)
		w->Click(x, y);
	else
		ClickElements(w->Elements, w->ElementsCount, x, y);
}

inline void ScrollDownModal(ModalWindowDef* w)
{
  if(w->ScrollDown) w->ScrollDown();
}

inline void ScrollUpModal(ModalWindowDef* w)
{
  if(w->ScrollUp) w->ScrollUp();
}