
#include <stdint.h>
#include "conf.h"
#include "def.h"

#include "Calculator.h"
#include "PPMGenerator.h"
#include "EEPRom.h"
#include "StatusBox.h"
#include "ComCom.h"
#include "functions.h"

extern "C" void USB_GetReportDescriptor(uint16_t *l, uint8_t **b);
extern "C" uint16_t USB_HID_GetReportLength();
extern "C" uint8_t *USB_HID_GetLastReport();
extern "C" void GetJoyInfo(uint16_t *pVendor, uint16_t *pProduct);

#ifdef DEBUG_USB

extern "C" void  USBStartCollectingDebug();
extern "C" void USBGetCollectedDebug(uint8_t** b, uint32_t *len);
extern "C" void USBGetStatuses(uint8_t *b);


#endif

ModelDataDef* CurrentModel = 0;
uint16_t CurrentModelIdx = 0;

axis_t DATAMAP[DATAMAP_SIZE];

bool ProgramStopped = false;

void ExecMenu();
void ExecCommon();
void ExecStartup();
void ExecModel();

void StopCalc()
{
	ProgramStopped = true;
}

void SendDataMap(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	*ab = (u8*)DATAMAP;
	*al = CalcData->DataMapLength * 2;
}

void SetVariable(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	uint8_t idx = b[0];
	axis_t val = b[1] << 8 | b[2];

	VariableDef v = CalcData->Variables[idx];
	
	DATAMAP[v.DATAMAP_ADDR] = val;

	if(v.EEPROM_ADDR)
		EEPROM.Write(v.EEPROM_ADDR, val);

	*ab = 0;
	*al = 0;
}

void GetEEPVariable(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	u16 idx = b[0] << 8 | b[1];

	static u8 EEPVarAnswer[4];
	s16 val;

	if (EEPROM.ReadDirty(idx, &val))
	{
		*(EEPVarAnswer) = 0;
		*(EEPVarAnswer+1) = 0;
		*(EEPVarAnswer+2) = (val >> 8) & 0xFF;
		*(EEPVarAnswer+3) = (val) & 0xFF;
	}
	else
	{
		*(EEPVarAnswer) = 0;
		*(EEPVarAnswer + 1) = 0;
		*(EEPVarAnswer + 2) = 0;
		*(EEPVarAnswer + 3) = 0;
	}

	*ab = EEPVarAnswer;
	*al = 4;
}

void SetEEPVariable(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	u16 idx = b[0] << 8 | b[1];
	s16 val = b[2] << 8 | b[3];

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
	
	*ab = EEPVarAnswer;
	*al = 2;
}

void SaveEEPROM(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 EEPVarAnswer[2];
	EEPVarAnswer[0] = 1;
	EEPVarAnswer[0] = 1;

	EEPROM.ForceSave();
	*ab = EEPVarAnswer;
	*al = 2;
}

void GetJoyDescriptor(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	uint16_t aal;
	USB_GetReportDescriptor(&aal, ab);
	*al = aal;
}

void GetJoyReport(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	*al = USB_HID_GetReportLength();
	*ab = USB_HID_GetLastReport();
}

#ifdef DEBUG_USB

void StartUSBDebug(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	USBStartCollectingDebug();

	static u8 EEPVarAnswer[2];
	EEPVarAnswer[0] = 1;
	EEPVarAnswer[0] = 1;

	*ab = EEPVarAnswer;
	*al = 2;
}

void GetUSBDebugCOllected(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	USBGetCollectedDebug(ab, al);
}

void GetUSBStateBytes(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 USBStates[10];
	USBGetStatuses(USBStates);

	*ab = USBStates;
	*al = 10;
}



#endif

static uint8_t VendorData[4];


void GetJoyIDs(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	uint16_t v, p;
	GetJoyInfo(&v, &p);

	VendorData[0] = v >> 8 & 0xFF;
	VendorData[1] = v & 0xFF;

	VendorData[2] = p >> 8 & 0xFF;
	VendorData[3] = p & 0xFF;

	*al = 4;
	*ab = VendorData;
}

void InitCalc()
{
	COMCOM.RegisterCommand(0x01, &GetJoyDescriptor);
	COMCOM.RegisterCommand(0x08, &GetJoyReport);
	COMCOM.RegisterCommand(0x0A, &GetJoyIDs);

	if (PROGRAM_PRESENT)
	{
		COMCOM.RegisterCommand(0x02, &SendDataMap);
		COMCOM.RegisterCommand(0x03, &SetVariable);
		COMCOM.RegisterCommand(0x06, &GetEEPVariable);
		COMCOM.RegisterCommand(0x07, &SetEEPVariable);
		COMCOM.RegisterCommand(0x09, &SaveEEPROM);
	}

#ifdef DEBUG_USB
	COMCOM.RegisterCommand(0x0B, &StartUSBDebug);
	COMCOM.RegisterCommand(0x0C, &GetUSBDebugCOllected);
	COMCOM.RegisterCommand(0x0D, &GetUSBStateBytes);
#endif
}

static void ExecuteCode(uint32_t datapointer)
{
	if (!PROGRAM_PRESENT) return;

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
	if (!PROGRAM_PRESENT) return;

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
	if (!PROGRAM_PRESENT) return;

	if(CurrentModel != 0)
		ExecuteCode((uint32_t)CurrentModel->ModelCode);
}

void SwitchCurrentModel(uint16_t model_index)
{
	if (!PROGRAM_PRESENT) return;

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
		STATUSBOX.SetModel((char*)CurrentModel->ModelName);
		STATUSBOX.SetFlightMode((char*)NULL);
	}
	else
	{
		VariableDef* state_var = CalcData->Variables + CurrentModel->ModesStoreVariable;
		u16 current_mode = get(state_var->DATAMAP_ADDR);
		STATUSBOX.SetModel(CurrentModel->ModelName);
		STATUSBOX.SetFlightMode(*(CurrentModel->Modes + current_mode));
	}

	STATUSBOX.SetData(STATE, "");
}
