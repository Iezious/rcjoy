#include "menu.h"
#include "EEPRom.h"

SysMenu SYSMENU;

#define MENU_MODELSELECT 0
#define MENU_SETVARIABLE 1
#define MENU_EXIT 100

static u8 __CurrentMenu = MENU_MODELSELECT;
static u8 __CurrentModelIdx = 0;
static u16 __CurrentVariableIdx = 0;
static s16 __CurrentVarValue = 0;

#define MENU_CURRENT_MODEL (CalcData->Models + __CurrentModelIdx)
#define MENU_CURRENT_VAR (CalcData->Variables + __CurrentVariableIdx)


static bool selectVariable()
{
	u16 idx = 0;

	do
	{
		idx++;

		if (idx >= CalcData->VariablesCount) 
		{
			__CurrentMenu = MENU_MODELSELECT; // no variables for this model!!!
			return false;
		}
	} while (CalcData->Variables[idx].MODEL_IDX != __CurrentModelIdx);

	__CurrentVariableIdx = idx;
	__CurrentVarValue = DATAMAP[MENU_CURRENT_VAR->DATAMAP_ADDR];
	return true;
}

void static selectNextVariable()
{
	do
	{
		__CurrentVariableIdx++;
		if (__CurrentVariableIdx >= CalcData->VariablesCount)
			__CurrentVariableIdx = 0;
	} while (MENU_CURRENT_VAR->MODEL_IDX != __CurrentModelIdx);
	__CurrentVarValue = DATAMAP[MENU_CURRENT_VAR->DATAMAP_ADDR];
}

void static selectPrevVariable()
{
	do
	{
		__CurrentVariableIdx == __CurrentVariableIdx == 0 ? CalcData->VariablesCount : __CurrentVariableIdx;
		__CurrentVariableIdx--;
	} while (MENU_CURRENT_VAR->MODEL_IDX != __CurrentModelIdx);
	__CurrentVarValue = DATAMAP[MENU_CURRENT_VAR->DATAMAP_ADDR];
}


static void showMenu()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			STATUSBOX.SetMenu("Select model", MENU_CURRENT_MODEL->ModelName);
			break;

		case MENU_SETVARIABLE:
			STATUSBOX.SetMenu("Set variable", MENU_CURRENT_VAR->Name, __CurrentVarValue);
			break;

		case MENU_EXIT:
			STATUSBOX.SetMenu("Exit");
			break;
	}
}

static inline void FindNextModel(u8 delta)
{
	u8 c = __CurrentModelIdx;
	do
	{
		if (delta > 0)
			__CurrentModelIdx = (__CurrentModelIdx + 1) < CalcData->ModelsCount ? __CurrentModelIdx + 1 : 0;
		else
			__CurrentModelIdx = __CurrentModelIdx > 0 ? __CurrentModelIdx - 1 : CalcData->ModelsCount - 1;

		if (__CurrentModelIdx == c) break;

	} while (MENU_CURRENT_MODEL->PPMChannels == 0);
}


void SysMenu::Start()
{
	__CurrentModelIdx = CurrentModelIdx;
	__CurrentMenu = 0;

	MenuActive = true;

	showMenu();
}

void SysMenu::Execute()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			MenuActive = false;
			SwitchCurrentModel(__CurrentModelIdx);
			break;

		case MENU_SETVARIABLE:
			DATAMAP[MENU_CURRENT_VAR->DATAMAP_ADDR] = __CurrentVarValue;

			if (MENU_CURRENT_VAR->EEPROM_ADDR)
				EEPROM.Write(MENU_CURRENT_VAR->EEPROM_ADDR, __CurrentVarValue);

			break;

		case MENU_EXIT:
			MenuActive = false;
			SwitchCurrentModel(__CurrentModelIdx);
			break;
	}
}

void SysMenu::NextSection()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			__CurrentMenu = selectVariable() ? MENU_SETVARIABLE : MENU_EXIT;
			break;

		case MENU_SETVARIABLE:
			__CurrentMenu = MENU_EXIT;
			break;

		case MENU_EXIT:
			__CurrentMenu = MENU_MODELSELECT;
			break;
	}

	showMenu();
}

void SysMenu::PrevSection()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			__CurrentMenu = MENU_EXIT;
			break;

		case MENU_SETVARIABLE:
			__CurrentMenu = MENU_MODELSELECT;
			break;

		case MENU_EXIT:
			__CurrentMenu = selectVariable() ? MENU_SETVARIABLE : MENU_EXIT;
			break;
	}

	showMenu();
}

void SysMenu::NextSelect()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			FindNextModel(1);
			break;

		case MENU_SETVARIABLE:
			selectNextVariable();
			break;
	}

	showMenu();
}

void SysMenu::PrevSelect()
{
	switch (__CurrentMenu)
	{
		case MENU_MODELSELECT:
			FindNextModel(-1);
			break;

		case MENU_SETVARIABLE:
			selectPrevVariable();
			break;
	}
	showMenu();
}

void SysMenu::IncVariable()
{
	switch (__CurrentMenu)
	{
		case MENU_SETVARIABLE:
			s16 v = __CurrentVarValue + MENU_CURRENT_VAR->INCREMENT;
			if (v <= MENU_CURRENT_VAR->MAX) __CurrentVarValue = v;
			break;

	}
	showMenu();

}

void SysMenu::DecVariable()
{
	switch (__CurrentMenu)
	{
		case MENU_SETVARIABLE:
			s16 v = __CurrentVarValue - MENU_CURRENT_VAR->INCREMENT;
			if (v >= MENU_CURRENT_VAR->MIN) __CurrentVarValue = v;
			break;
	}
	showMenu();
}