#include "ComCom.h"
#include "ComProg.h"
#include "EEPRom.h"


static void UnlockMemory(uint8_t *buf, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 b[2] = { 0, 0 };
	*al = 1;
	*ab = b;

	if (EEPROM.GetBusy()) return;

	b[1] = b[0] = 1;
	EEPROM.Suspend();
	StopCalc();
}

static void EraseMemory(uint8_t *buf, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 b[2] = { 0, 0 };
	*al = 1;
	*ab = b;

	if (HAL_FLASH_Unlock() != HAL_OK) return;

	FLASH_EraseInitTypeDef fes;
	fes.Sector = PROGRAM_SECTOR;
	fes.NbSectors = 1;
	fes.TypeErase = TYPEERASE_SECTORS;
	fes.VoltageRange = VOLTAGE_RANGE_3;

	u32 errors;

	if ((HAL_FLASHEx_Erase(&fes, &errors) != HAL_OK) || errors != 0xFFFFFFFF)
	{
		HAL_FLASH_Lock();
		return;
	}

	b[0] = b[1] = 1; 
}

static void FinishProg(uint8_t *buf, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 b[2] = { 0, 0 };
	*al = 1;
	*ab = b;

	HAL_FLASH_Lock();

	b[0] = b[1] = 1;
}

static void WriteChunk(uint8_t *buf, uint32_t l, uint8_t **ab, uint32_t *al)
{
	static u8 b[2] = { 0, 0 };
	*al = 1;
	*ab = b;

	u8 cs = 0;

	for (u32 i = 0; i < l; i++) cs = cs ^ buf[i];
	
	if (cs != 0)   // checksum failed
	{
		b[1] = b[0] = 5;
		HAL_FLASH_Lock();
		return;
	}

	u32 addr = PROGRAM_BASE + *((u32*)buf);

	for (u32 i = 0; i < l - 5; i++)
	{
		if (HAL_FLASH_Program(TYPEPROGRAM_BYTE, addr + i, *(buf + i + 4)) == HAL_OK) continue;

		HAL_FLASH_Lock();
		return;
	}

	b[0] = b[1] = 1;
}


void InitComProgrammer()
{
	COMCOM.RegisterCommand(0x60, &UnlockMemory);
	COMCOM.RegisterCommand(0x61, &EraseMemory);
	COMCOM.RegisterCommand(0x67, &WriteChunk);
	COMCOM.RegisterCommand(0x69, &FinishProg);
}