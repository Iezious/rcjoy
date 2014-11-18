#include <inttypes.h>
#include <math.h>
#include <stdio.h>
#include <inttypes.h>
#include "conf.h"
#include "def.h"
#include "gui.h"
#include "guimodal.h"
#include "NumInputOverlay.h"


static bool NumButtonClick(GUIElementDef*, u16, u16);
static bool FuncButtonClick(GUIElementDef*, u16, u16);
static bool OKButtonClick(GUIElementDef*, u16, u16);
static bool CancelButtonClick(GUIElementDef*, u16, u16);

static void ButtonDraw(GUIElementDef*);
static void HeaderDraw(GUIElementDef*);
static void DefsDraw(GUIElementDef*);
static void ValueDraw(GUIElementDef*);

#define GUI_COLOR_DEFAULT 0xFFCCCCCC
#define GUI_COLOR_DARK 0xFF777777
#define GUI_COLOR_HL 0xFFFFFFFF
#define GUI_COLOR_GREEN 0xFF77FF77
#define GUI_COLOR_RED 0xFFFF7777

#define BUTTON_BORDER_COLOR GUI_COLOR_DEFAULT
#define OK_BUTTON_COLOR GUI_COLOR_GREEN
#define CANCEL_BUTTON_COLOR GUI_COLOR_RED
#define NUM_BUTTON_COLOR GUI_COLOR_DARK
#define VALID_VAL_COLOR GUI_COLOR_GREEN
#define INVALID_VAL_COLOR GUI_COLOR_RED
#define NAME_COLOR GUI_COLOR_DEFAULT
#define HEADER_VALUE_COLOR GUI_COLOR_DEFAULT
#define HEADER_COLOR GUI_COLOR_HL

#define FONT_VALUE  (&Font24)
#define FONT_BUTTON  (&Font24)
#define FONT_HEADER  (&Font16)
#define FONT_NAME (&Font20)
#define ICON_FONT (&FontPkts)

static const char* ButtonNames[14] =
{
	"0", "1", "2",
	"3", "4", "5",
	"6", "7", "8",
	"9", "-", ",",
	"OK", "X"
};

static s16 __Value = 0;
static s16 __Default;
static s16 __Min;
static s16 __Max;

NumDialogDataDef* __Data;



static const char* __Name;

static inline bool IsValueValid()
{
	return __Value >= __Min && __Value <= __Max;
}

/*******  DEFINITIONS
**/

static GUIElementDef Button0 =
{
	36, 36, 165, 230, & ButtonDraw, &NumButtonClick, NULL, 0
};

static GUIElementDef Button1 =
{
	36, 36, 39, 146, &ButtonDraw, &NumButtonClick, NULL, 1
};

static GUIElementDef Button2 =
{
	36, 36, 81, 146, &ButtonDraw, &NumButtonClick, NULL, 2
};

static GUIElementDef Button3 =
{
	36, 36, 123, 146, &ButtonDraw, &NumButtonClick, NULL, 3
};

static GUIElementDef Button4 =
{
	36, 36, 39, 188, &ButtonDraw, &NumButtonClick, NULL, 4
};

static GUIElementDef Button5 =
{
	36, 36, 81, 188, &ButtonDraw, &NumButtonClick, NULL, 5
};

static GUIElementDef Button6 =
{
	36, 36, 123, 188, &ButtonDraw, &NumButtonClick, NULL, 6
};

static GUIElementDef Button7 =
{
	36, 36, 39, 230, &ButtonDraw, &NumButtonClick, NULL, 7
};

static GUIElementDef Button8 =
{
	36, 36, 81, 230, &ButtonDraw, &NumButtonClick, NULL, 8
};

static GUIElementDef Button9 =
{
	36, 36, 123, 230, &ButtonDraw, &NumButtonClick, NULL, 9
};

static GUIElementDef ButtonMinus =
{
	36, 36, 165, 188, &ButtonDraw, &FuncButtonClick, NULL, 10
};

static GUIElementDef ButtonDel =
{
	36, 36, 165, 146, &ButtonDraw, &FuncButtonClick, NULL, 11
};

static GUIElementDef ButtonOK =
{
	78, 36, 39, 272, &ButtonDraw, &OKButtonClick, NULL, 12
};

static GUIElementDef ButtonCancel =
{
	78, 36, 123, 272, &ButtonDraw, &CancelButtonClick, NULL, 13
};

static GUIElementDef NameField =
{
	SCREEN_WIDTH - 8, 24, 4, 7, &HeaderDraw, NULL, NULL, 0
};

static GUIElementDef ValueField =
{
	SCREEN_WIDTH - 8, 30, 4, 108, &ValueDraw, NULL, NULL, 0
};

static GUIElementDef DefsField =
{
	SCREEN_WIDTH - 8, 72, 4, 33, &DefsDraw, NULL, NULL, 0
};

static GUIElementDef* Elements[17] =
{
	&Button0, &Button1, &Button2, &Button3, &Button4,
	&Button5, &Button6, &Button7, &Button8, &Button9,
	&ButtonMinus, &ButtonDel, &ButtonOK, &ButtonCancel,
	&NameField, &ValueField, &DefsField
};

static ModalWindowDef NumDialog =
{
	Elements, 17, NULL, NULL, NULL, NULL, NULL
};



static void ButtonDraw(GUIElementDef* b)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(b->Left, b->Top, b->Width, b->Height);
	BSP_LCD_SetTextColor(BUTTON_BORDER_COLOR);
	BSP_LCD_DrawRect(b->Left, b->Top, b->Width, b->Height);

	if (b->Tag == 12)
		BSP_LCD_SetTextColor(OK_BUTTON_COLOR);
	else if (b->Tag == 13)
		BSP_LCD_SetTextColor(CANCEL_BUTTON_COLOR);
	else
		BSP_LCD_SetTextColor(NUM_BUTTON_COLOR);

	sFONT *font;

	if (b->Tag == 11)
		font = ICON_FONT;
	else
		font = FONT_BUTTON;

	BSP_LCD_SetFont(font);

	const char* txt = *(ButtonNames + b->Tag);
	u16 len = __strlen((u8*)txt);
	u16 w = len * font->Width;

	BSP_LCD_DisplayStringAt(b->Left + (b->Width - w) / 2, b->Top + (b->Height - font->Height) / 2, (u8*)txt, LEFT_MODE);
}

static char fBuffer[64];

static void ValueDraw(GUIElementDef *e)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);
	BSP_LCD_SetTextColor(BUTTON_BORDER_COLOR);
	BSP_LCD_DrawRect(e->Left, e->Top, e->Width, e->Height);

	if (IsValueValid)
		BSP_LCD_SetTextColor(VALID_VAL_COLOR);
	else
		BSP_LCD_SetTextColor(INVALID_VAL_COLOR);

	BSP_LCD_SetFont(FONT_VALUE);

	sprintf(fBuffer, "%d", __Value);
	u16 w = __strlen((u8*)fBuffer) * FONT_VALUE->Width;
	BSP_LCD_DisplayStringAt(e->Left + e->Width - 6 - w, e->Top + (e->Height - FONT_VALUE->Height) / 2, (u8*)fBuffer, LEFT_MODE);
}

static void HeaderDraw(GUIElementDef *e)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);

	u16 w = __strlen((u8*)__Name) * FONT_NAME->Width;
	BSP_LCD_SetTextColor(NAME_COLOR);
	BSP_LCD_SetFont(FONT_NAME);
	BSP_LCD_DisplayStringAt(e->Left, e->Top, (u8*)__Name, LEFT_MODE);
}

static void DefsDraw(GUIElementDef *e)
{
	BSP_LCD_SetTextColor(MAIN_BACK_COLOR);
	BSP_LCD_FillRect(e->Left, e->Top, e->Width, e->Height);

	BSP_LCD_SetFont(FONT_HEADER);

	u16 w, t, l;
	const char* str;

	// default

	t = 0;
	l = 0;

	str = "Default value: ";
	w = FONT_HEADER->Width * __strlen((u8*)str);
	BSP_LCD_SetTextColor(HEADER_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);

	l += w;
	sprintf(fBuffer, "%d", __Default);
	str = fBuffer;

	BSP_LCD_SetTextColor(HEADER_VALUE_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);

	l = 0;
	t += FONT_HEADER->Height + 4;

	// minimal

	str = "Minimal value: ";
	w = FONT_HEADER->Width * __strlen((u8*)str);
	BSP_LCD_SetTextColor(HEADER_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);

	l += w;
	sprintf(fBuffer, "%d", __Min);
	str = fBuffer;

	BSP_LCD_SetTextColor(HEADER_VALUE_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);

	l = 0;
	t += FONT_HEADER->Height + 4;

	// maximal

	str = "Maximal value: ";
	w = FONT_HEADER->Width * __strlen((u8*)str);
	BSP_LCD_SetTextColor(HEADER_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);

	l += w;
	sprintf(fBuffer, "%d", __Max);
	str = fBuffer;

	BSP_LCD_SetTextColor(HEADER_VALUE_COLOR);
	BSP_LCD_DisplayStringAt(e->Left + l, e->Top + t, (u8*)str, LEFT_MODE);
}

bool NumButtonClick(GUIElementDef* btn, u16 x, u16 y)
{
	s16 v = __Value;
	v = v * 10 + (s16)btn->Tag;

	if ((v > 10000) || (v < -10000)) return true;

	__Value = v;
	ValueDraw(&ValueField);

	return true;
}

bool FuncButtonClick(GUIElementDef* btn, u16 x, u16 y)
{
	if (btn->Tag == 10)
	{
		__Value = -1 * __Value;
	}
	else if (btn->Tag == 11)
	{
		if (fabs(__Value) < 10)
			__Value = 0;
		else
			__Value = __Value / 10;
	}

	ValueDraw(&ValueField);
	return true;
}

bool OKButtonClick(GUIElementDef* btn, u16 x, u16 y)
{
	if (!IsValueValid()) return true;

	__Data->OKCallback(__Data->tag, __Value);

	GUIRoot.HideModal();
	return true;
}

bool CancelButtonClick(GUIElementDef*, u16, u16)
{
	if (__Data->Cancel) __Data->Cancel();

	GUIRoot.HideModal();
	return true;
}


void ShowNumInput(NumDialogDataDef *data)
{
	__Name = data->Name;
	__Default = data->Default;
	__Max = data->Max;
	__Min = data->Min;

	__Data = data;
	__Value = data->Value;

	GUIRoot.ShowModal(&NumDialog);
}

