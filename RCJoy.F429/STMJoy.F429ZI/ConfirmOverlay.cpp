#include <inttypes.h>
#include <math.h>
#include <stdio.h>
#include <inttypes.h>
#include "conf.h"
#include "def.h"
#include "gui.h"
#include "guimodal.h"
#include "ConfirmOverlay.h"

/*********************************************************************************************
/****   Definitions
/*****************/

static u32 tag;
static u8* Caption1;
static u8* Caption2;



#define OK_BUTTON_COLOR GUI_COLOR_GREEN
#define CANCEL_BUTTON_COLOR GUI_COLOR_RED
#define BUTTON_BORDER_COLOR GUI_COLOR_DEFAULT
#define NAME_COLOR GUI_COLOR_DEFAULT
#define CAPTION_COLOR GUI_COLOR_HL
#define SUBHEADER_COLOR GUI_COLOR_DEFAULT


#define FONT_CAPTION  (&Font16)
#define FONT_BUTTON  (&Font16)
#define FONT_SUBCAPTION (&Font12)
#define ICON_FONT (&FontPkts)
#define FONT_NAME (&Font12)

static void(*OnConfirm)(u32 tag);
static void(*OnCancel)(u32 tag);

static bool OKButtonClick(GUIElementDef*, u16, u16);
static bool CancelButtonClick(GUIElementDef*, u16, u16);

static void ButtonDraw(GUIElementDef*);
static void CaptionDraw(GUIElementDef*);


/*********************************************************************************************
/****   Elelents
/*****************/


static GUIElementDef ButtonOK =
{
	80, 24, 20, 180, &ButtonDraw, &OKButtonClick, NULL, 0
};

static GUIElementDef ButtonCancel =
{
	80, 24, 140, 180, &ButtonDraw, &CancelButtonClick, NULL, 1
};

static GUIElementDef Header =
{
	220, 36, 10, 100, &CaptionDraw, NULL, NULL, 0
};

static GUIElementDef SubHeader =
{
	220, 36, 10, 120, &CaptionDraw, NULL, NULL, 1
};

static GUIElementDef* Elements[4] = { &ButtonOK, &ButtonCancel, &Header, &SubHeader };

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
	BSP_LCD_DisplayStringAt(e->Left + (e->Width - w) / 2, e->Top + 8, text, LEFT_MODE);
}

static void CaptionDraw(GUIElementDef* e)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);

	u8* text = e->Tag == 0 ? Caption1 : Caption2;
	if (!text) return;

	sFONT *fnt = e->Tag == 0 ? FONT_CAPTION : FONT_SUBCAPTION;

	BSP_LCD_SetTextColor(e->Tag == 0 ? CAPTION_COLOR : SUBHEADER_COLOR);
	BSP_LCD_SetFont(fnt);
	BSP_LCD_DisplayStringAt(e->Left, e->Top, text, LEFT_MODE);
}


static bool OKButtonClick(GUIElementDef* el, u16 x, u16 y)
{
	if (OnConfirm) OnConfirm(tag);
	GUIRoot.HideModal();

	return true;
}
static bool CancelButtonClick(GUIElementDef* e, u16 x, u16 y)
{
	if (OnCancel) OnCancel(tag);
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