#include <inttypes.h>
#include <math.h>
#include <stdio.h>
#include <inttypes.h>
#include "conf.h"
#include "def.h"
#include "gui.h"
#include "guimodal.h"

/*********************************************************************************************
/****   Definitions
/*****************/

static u32 tag;
static u8* Caption1;
static u8* Caption2;



#define OK_BUTTON_COLOR GUI_COLOR_GREEN
#define CANCEL_BUTTON_COLOR GUI_COLOR_RED

#define FONT_CAPTION  (&Font20)
#define FONT_BUTTON  (&Font24)
#define FONT_SUBCAPTION (&Font16)
#define ICON_FONT (&FontPkts)

static void (OnConfirm*)(u32 tag);
static void (OnCancel*)(u32 tag);

static bool OKButtonClick(GUIElementDef*, u16, u16);
static bool CancelButtonClick(GUIElementDef*, u16, u16);

static void ButtonDraw(GUIElementDef*);
static void CaptionDraw(GUIElementDef*);


/*********************************************************************************************
/****   Elelents
/*****************/


static GUIElementDef ButtonOK =
{
	36, 80, 20, 260, &ButtonDraw, &OKButtonClick, NULL, 0
};

static GUIElementDef ButtonCancel =
{
	36, 80, 160, 260, &ButtonDraw, &OKButtonClick, NULL, 1
};

static GUIElementDef Header = 
{
 36, 200, 20, 100, &CaptionDraw, NULL, NULL, 0
}

static GUIElemnentDef SubHeader = 
{
  36, 200, 20, 160, &CaptionDraw, NULL, NULL, 0
}

static GUIElementDef Elements[4] = { ButtonOK, ButtonCancel, Header, SubHeader };

static ModalWindowDef ConfirmDialog =
{
	Elements, 4, NULL, NULL, NULL, NULL, NULL
};




/*********************************************************************************************
/****   Callbacks
/*****************/


static void ButtonDraw(GUIElementDef* e)
{
	 BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	 BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);
  	BSP_LCD_SetTextColor(BUTTON_BORDER_COLOR);
  	BSP_LCD_DrawRect(e->Left, e->Top, e->Width, e->Height);

  BSP_LCD_SetFont(FONT_BUTTON);
  BSP_LCD_SetTextColor(e->Tag == 0 ? OK_BUTTON_COLOR : CANCEL_BUTTON_COLOR);
  u8* text = (u8*)(e->Tag == 0 ? "OK" : "Cancel");   

  u16 w = __strlen(text) * FONT_BUTTON->Width;
  BSP_LCD_DisplayStringAt(e->Left + (e->Width -w) / 2, e->Top + 8, text, LEFT_MODE);
}

static void CaptionDraw(GUIElementDef* e)
{
	 BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	 BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);
  
  u8* text = e->Tag == 0 ? Caption1 : Caption2;
  if(!text) return;
  
  Font* fnt = e->Tag == 0 ? FONT_CAPTION : FONT_SUBCAPTION;
  
  BSP_LCD_SetTextColor(e->Tag == 0? CAPTION_COLOR : SUBHEADER_COLOR);
  BSP_LCD_SetFont(fnt);
     
	 u16 w = __strlen(text) * fnt->Width;
	 BSP_LCD_SetTextColor(NAME_COLOR);
	 BSP_LCD_SetFont(FONT_NAME);
	 BSP_LCD_DisplayStringAt(e->Left + (e->Width -w) / 2, e->Top, text, LEFT_MODE);
}


static bool OKButtonClick(GUIElementDef* el, u16 x, u16 y)
{
  if(OnConfirm) OnConfirm(tag);
  GUIRoot.HideModal();
  
  return true;
}
static bool CancelButtonClick(GUIElementDef* e, u16 x, u16 y)
{
  if(OnCancel) OnCancel(tag);
  GUIRoot.HideModal();
  
  return true;
}

/*********************************************************************************************
/****   Show call
/*****************/

void ShowConfirmDialog(ConfirmOvelayData *data)
{
  OnConfirm = data->Confirmed;
  OnCancel = data->Declined;
  tag = data->Tag,
  Caption1 = data->Caption;
  Caption2 = data->SubCaption;
  
  GUIRoot.ShowModal(&ConfirmDialog);
}