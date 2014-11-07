
#include <stdint.h>
#include "conf.h"
#include "def.h"

#include "Calculator.h"
#include "PPMGenerator.h"
#include "EEPRom.h"
#include "StatusBox.h"
#include "ComCom.h"
#include "functions.h"

ModelDataDef* CurrentModel = 0;
uint16_t CurrentModelIdx = 0;

axis_t DATAMAP[DATAMAP_SIZE];

void ExecMenu();
void ExecCommon();
void ExecStartup();
void ExecModel();

void SendDataMap()
{
	COMCOM.Send(0x02, CalcData->DataMapLength*2, (u8*)DATAMAP);
}

void SetVariable(uint8_t idx, axis_t val)
{
	VariableDef v = CalcData->Variables[idx];
	
	DATAMAP[v.DATAMAP_ADDR] = val;

	if(v.EEPROM_ADDR)
		EEPROM.Write(v.EEPROM_ADDR, val);
}

void GetEEPVariable(u16 idx)
{
	static u8 EEPVarAnswer[4];
	s16 val;

	if (EEPROM.Read(idx, &val))
	{
		*(EEPVarAnswer) = 0;
		*(EEPVarAnswer+1) = 0;
		*(EEPVarAnswer+2) = (val >> 8) & 0xFF;
		*(EEPVarAnswer+3) = (val) & 0xFF;
	}
	else
	{
		*(EEPVarAnswer) = 0;
		*(EEPVarAnswer + 1) = 1;
		*(EEPVarAnswer + 2) = 0;
		*(EEPVarAnswer + 3) = 0;
	}

	COMCOM.Send(6, 4, EEPVarAnswer);
}

void SetEEPVariable(u16 idx, s16 val)
{
	static u8 EEPVarAnswer[2];

	if (EEPROM.Write(idx, val))
	{
		EEPVarAnswer[0] = 1;

		for (int i = 0; i < CalcData->VariablesCount; i++)
		{
			VariableDef* __var = CalcData->Variables + i;
			if (__var->EEPROM_ADDR != idx) continue;

			DATAMAP[__var->DATAMAP_ADDR] = val;
			EEPVarAnswer[0] = 3;
			break;
		}

		EEPVarAnswer[1] = 0;
	}
	else
	{
		EEPVarAnswer[0] = 0;
		EEPVarAnswer[1] = 1;
	}
	COMCOM.Send(7, 2, EEPVarAnswer);
}

static void ExecuteCode(uint32_t datapointer)
{
	uint16_t command;

	while (command = *((uint16_t *)datapointer))
	{
		if(command == 0xFF) return;

		datapointer+=2;
		if(!(*(FunctionsMap + command))(&datapointer)) return;
	}
}

void ExecuteCommon()
{
	ExecuteCode((uint32_t)CalcData->CommonCode);
}

void ExecuteMenu()
{
	ExecuteCode((uint32_t)CalcData->MenuCode);
}

void ExecuteStartup()
{
	ExecuteCode((uint32_t)CalcData->StartupCode);

	s16 modelIdx;
	
	if(!EEPROM.Read(EEP_MODEL, &modelIdx)) modelIdx=0;

	if (modelIdx < 0 || modelIdx >= CalcData->ModelsCount)
		modelIdx = 0;

	SwitchCurrentModel(modelIdx);

	if (CalcData->PH_Bits & PH_BITS_PPM)
		PPMGen.start();
}

void ExecuteModel()
{
	if(CurrentModel != 0)
		ExecuteCode((uint32_t)CurrentModel->ModelCode);
}

void SwitchCurrentModel(uint16_t model_index)
{
	if (model_index > CalcData->ModelsCount)
		model_index = 0;

	while ((&CalcData->Models[model_index])->PPMChannels == 0)
	{
		model_index++;
		if (model_index > CalcData->ModelsCount)
		{
			CurrentModel = 0;
			return;
		}
	}

	CurrentModel = &CalcData->Models[model_index];
	CurrentModelIdx = model_index;

	EEPROM.Write(EEP_MODEL, (s16)CurrentModelIdx);
	PPMGen.setChannels(CurrentModel->PPMChannels);

	if (CurrentModel->ModesCount == 0)
	{
		STATUSBOX.SetStatus((char*)CurrentModel->ModelName);
	}
	else
	{
		VariableDef* state_var = CalcData->Variables + CurrentModel->ModesStoreVariable;
		u16 current_mode = get(state_var->DATAMAP_ADDR);
		STATUSBOX.SetStatus(CurrentModel->ModelName, *(CurrentModel->Modes + current_mode));
	}

	STATUSBOX.SetData(STATE, "");
}
