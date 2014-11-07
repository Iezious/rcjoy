#pragma once

#include <inttypes.h>
#include "guielement.h"

struct GUIModeDef
{
    uint8_t Index;
    uint8_t Icon;
    
    bool Active;
    
    GUIElementDef **Elements;
    uint8_t ElementsCount;
    
    bool (* Activate)(void);
	bool(*Deactivate)(void);
	bool(*Click)(uint16_t x, uint16_t y);
	bool(*Update)(void);
	bool(*ScrollUp)(void);
	bool(*ScrollDown)(void);
	bool(*ScrollTo)(uint16_t y);
    void (* Tick)(void);
};

extern void DrawHeader(GUIModeDef *mode);
extern void ClickMode(GUIModeDef *mode, uint16_t x, uint16_t y);
extern void DrawMode(GUIModeDef *mode);

inline void ActivateMode(GUIModeDef *mode)
{
	mode->Active = true;
	if (mode->Activate) mode->Activate();
}

inline void DeactivateMode(GUIModeDef *mode)
{
	mode->Active = false;
	if (mode->Deactivate) mode->Deactivate();
}

inline void TickMode(GUIModeDef *mode)
{
	if (!mode->Active) return;
	if (mode->Tick) mode->Tick();
}

inline void ScrollUpMode(GUIModeDef *mode)
{
	if (!mode->Active) return;
	if (mode->ScrollUp) mode->ScrollUp();
}

inline void ScrollDownMode(GUIModeDef *mode)
{
	if (!mode->Active) return;
	if (mode->ScrollDown) mode->ScrollDown();
}