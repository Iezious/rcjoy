#include "guimodeblock.h"
#include "functions.h"
#include "NumInputOverlay.h"
#include "EEPRom.h"
#include "GUIElement.h"

static void DrawScroll(GUIElementDef* );
static bool ClickScroll(GUIElementDef*, u16, u16);
static void DrawList(GUIElementDef* );
static bool ClickList(GUIElementDef*, u16, u16);
static bool ListScrollUp();
static bool ListScrollDown();

static void DrawListItem(uint8_t elementIndex, uint16_t top, uint16_t left, uint16_t width);
static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY);

static void BlockModeTick();

#define COLOR_NAME 0xFFCCCCCC
#define COLOR_VARIABLE 0xFFFFFF00
#define COLOR_EEPROM_VAR 0xFF00FFFF

#define COLOR_AXIS 0xFF000077
#define COLOR_BUTTON 0xFFFF0000
#define COLOR_VALUE 0xFF008080

#define FONT_NAME (&Font12)
#define FONT_VALUE (&Font16)
#define FONT_PKS (&FontPkts)

static GUIElementDef BlockDataList =
{
	LCD_WORK_WIDTH,						//uint16_t Width;
	LCD_WORK_HEIGHT,					//uint16_t Height;
	0,									          //uint16_t Left;
	TABS_HEIGHT,						   //uint16_t Top;

	&DrawList,							//void(*Draw)(void);
	&ClickList,							//bool(*Click)(uint16_t x, uint16_t y);
	0,									//void(*Drag)(uint16_t x, uint16_t y);
	0
};


static GUIElementDef BlocksScroll =
{
	LCD_SCROLL_WIDTH,					//uint16_t Width;
	LCD_WORK_HEIGHT - 6,				//uint16_t Height;
	LCD_WORK_WIDTH,						//uint16_t Left;
	TABS_HEIGHT + 3,					//uint16_t Top;

	&DrawScroll,						//void(*Draw)(void);
	&ClickScroll,						//bool(*Click)(uint16_t x, uint16_t y);
	0									//void(*Drag)(uint16_t x, uint16_t y);
};


static GUIElementDef* Elements[2] = { &BlockDataList,  &BlocksScroll};

ModalWindowDef BlockDialog =
{
	Elements, 2, NULL, NULL, &ListScrollUp, &ListScrollDown, &BlockModeTick
};

static ListDef ListBox =
{
	8, 										//uint8_t Length;
	24,	   									//uint8_t ItemHeight;
	0,					  					//uint8_t TopVisible;
	&DrawListItem,							//void(*DrawElement)(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width);
	&ClickListItem							//bool(*ClickElement)(uint8_t elementIndex, uint16_t eX, uint16_t eY);
};

static BlockDef* Block;
static u8 InLinksCount;
static u8 OutLinksCount;
static u8 VarsCount;

void ShowBlockDebug(BlockDef* block)
{
	Block = block;
	ListBox.Length = block->VarsCount + block->LinkCount + 1;
	
	VarsCount = block->VarsCount;
	
	InLinksCount = 0;
	OutLinksCount = 0;
	
	for(u8 i=0; i < block->LinkCount; i++)
	{
		if (block->Links[i].LINK_BITS & LINK_INPUT)
			InLinksCount++;
		else
			OutLinksCount++;
	}
	
	GUIRoot.ShowModal(&BlockDialog);
}

void DrawScroll(GUIElementDef* elem)
{
	DrawScrollBar(&BlocksScroll, ListBox.Length, BlockDataList.Height / ListBox.ItemHeight,
		ListBox.TopVisible);
}

static bool ClickScroll(GUIElementDef* s, u16 x, u16 y)
{
	return ScrollBarClick(y);
}

static bool ListScrollUp()
{
	return DoListScrollUp(&ListBox, &BlockDataList);
}

static bool ListScrollDown()
{
	return DoListScrollDown(&ListBox, &BlockDataList);
}

static void DrawLinkSymbol(BlockLinkDef* lnk, uint16_t top, uint16_t left, uint16_t width)
{
	if(lnk->LINK_BITS & LINK_TYPE_AXIS)	
		BSP_LCD_SetTextColor(COLOR_AXIS);		
	else if(lnk->LINK_BITS & LINK_TYPE_VALUE)
		BSP_LCD_SetTextColor(COLOR_VARIABLE);
	else 
		BSP_LCD_SetTextColor(COLOR_BUTTON);
	
	BSP_LCD_SetFont(FONT_PKS);
		
	if(lnk->LINK_BITS & LINK_INPUT)
		BSP_LCD_DisplayChar(left, top + 2, 45);
	else
		BSP_LCD_DisplayChar(left + width - FONT_PKS->Width, top + 2, 45);
}

static void DrawItemName(u8* name, uint16_t top, uint16_t left)
{
	BSP_LCD_SetTextColor(COLOR_NAME);
	BSP_LCD_SetFont(FONT_NAME);
	BSP_LCD_DisplayStringAt(left + FONT_PKS->Width + 4, top + 4, name, LEFT_MODE);
}

static void DrawValue(u16 addr, uint16_t left, uint16_t top, uint16_t width)
{
	char __buffer[8];

	BSP_LCD_SetFont(FONT_VALUE);
	
	sprintf(__buffer, "%d", get(addr));
	
	BSP_LCD_DisplayStringAt(
		left + width - FONT_PKS->Width - 4 - FONT_VALUE->Width * __strlen((u8*)__buffer), 
		top + 2, (u8*)__buffer, LEFT_MODE);
}

static void DrawButton(u16 addr, uint16_t left, uint16_t top, uint16_t width)
{
	BSP_LCD_SetTextColor(COLOR_BUTTON);		
	BSP_LCD_SetFont(FONT_VALUE);
	
	if(CHECK_DOWN(addr))
	{
		BSP_LCD_DisplayStringAt(
			left + width - FONT_PKS->Width - 4 - FONT_VALUE->Width * 4, 
			top + 2, (u8*)"DOWN", LEFT_MODE);
	}
	else
	{
		BSP_LCD_DisplayStringAt(
			left + width - FONT_PKS->Width - 4 - FONT_VALUE->Width * 4, 
			top + 2, (u8*)"UP  ", LEFT_MODE);
	}
}

static void DrawLink(BlockLinkDef* lnk, uint16_t left, uint16_t top, uint16_t width)
{
	DrawItemName((u8*)lnk->Name, left, top);
	DrawLinkSymbol(lnk, left, top, width);
	
	if(lnk -> LINK_BITS & LINK_TYPE_BUTTON)
	{
		DrawButton(lnk->DATAMAP_ADDR, left, top, width);
	}	
	else
	{
		BSP_LCD_SetTextColor(lnk->LINK_BITS & LINK_TYPE_AXIS ? COLOR_AXIS : COLOR_VALUE);
		DrawValue(lnk->DATAMAP_ADDR, left, top, width);
	}
}

static void DrawVariable(VariableDef* var, u16 left, u16 top, u16 width)
{
	DrawItemName((u8*)var->Name, left, top);
	BSP_LCD_SetTextColor(var->EEPROM_ADDR ? COLOR_VARIABLE : COLOR_EEPROM_VAR);
	DrawValue(var->DATAMAP_ADDR, left, top, width);
}

static void GetItem(u8 idx, u8* obj_idx, u8* isVar)
{
	if(idx >= InLinksCount && idx < VarsCount)
	{
		*isVar = 1;
		*obj_idx = idx - InLinksCount;
		return;
	}
	
	*isVar = 0;
	
	if(idx < InLinksCount)
	{
		for(u8 i = 0; i < Block->LinkCount; i++)
		{
			if(Block->Links[i].LINK_BITS & LINK_INPUT)
			{
				if(idx == 0)
				{
					*obj_idx = i;
					return;
				}
				
				idx --;
			}
		}
	}
	else
	{
		idx -= InLinksCount + VarsCount;
		for (u8 i = 0; i < Block->LinkCount; i++)
		{
			if(Block->Links[i].LINK_BITS & LINK_OUTPUT)
			{
				if(idx == 0)
				{
					*obj_idx = i;
					return;
				}
				
				idx --;
			}
		}
	}
	
	*isVar = 2;
} 

void DrawListItem(uint8_t elementIndex, uint16_t left, uint16_t top, uint16_t width)
{
	u8 idx, tp;
	
	GetItem(elementIndex, &idx, &tp);

	switch(tp)
	{
		case 0:            // link
			DrawLink(Block->Links+idx, left, top, width);
			break;
		
		case 1:
			DrawVariable(*(Block->Vars+idx), left, top, width);
			break;
	}	
}

static void DrawList(GUIElementDef* elem)
{
	if (ListBox.Length != 0)
		DrawList(&ListBox, &BlockDataList);
	else
	{
		BSP_LCD_SetTextColor(COLOR_ERROR);
		BSP_LCD_SetFont(FONT_VALUE);
		BSP_LCD_DisplayStringAt(elem->Left, elem->Top, (u8*)"Bad block structure", LEFT_MODE);
	}
}

static bool ClickList(GUIElementDef* s, u16 x, u16 y)
{
	ListClick(&ListBox, x, y);
	return true;
}

static void ValueEdited(int32_t tag, int16_t value);

static NumDialogDataDef VariableEditInfo =
{
	NULL,
	0, 0, 0, 0,
	0,

	&ValueEdited,
	NULL
};

static bool ClickListItem(uint8_t elementIndex, uint16_t eX, uint16_t eY)
{
	u8 idx, tp;
	
	GetItem(elementIndex, &idx, &tp);

	if(tp != 1) return false;
		
	VariableDef* var = *(Block->Vars+idx);

	VariableEditInfo.Name = var->Name;
	VariableEditInfo.Min = var->MIN;
	VariableEditInfo.Max = var->MAX;
	VariableEditInfo.Default = var->DEFAULT;
	VariableEditInfo.Value = get(var->DATAMAP_ADDR);
	VariableEditInfo.tag = idx;

	ShowNumInput(&VariableEditInfo);

	return true;
}

static void ValueEdited(int32_t tag, int16_t value)
{
	VariableDef* var = *(Block->Vars + tag);

	set_val(var->DATAMAP_ADDR, value);

	if (var->EEPROM_ADDR)
		EEPROM.Write(var->EEPROM_ADDR, value);
		
	GUIRoot.ShowModal(&BlockDialog);
}

static void ValueCanceled()
{
	GUIRoot.ShowModal(&BlockDialog);
}

static void BlockModeTick()
{
	if (ListBox.Length != 0) 
		DrawListNoClear(&ListBox, &BlockDataList);
}

