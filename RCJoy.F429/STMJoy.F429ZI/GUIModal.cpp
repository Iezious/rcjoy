#include <inttypes.h>
#include "gui.h"
#include "def.h"
#include "conf.h"
#include "guimodal.h"
#include "guielement.h"


void DrawModal(ModalWindowDef* w)
{
  BSP_LCD_Clear(MAIN_BACK_COLOR);  
  if(w->Draw) w->Draw();
  
  DrawElements(w->Elements, w->ElementsCount);
}
