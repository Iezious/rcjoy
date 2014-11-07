#include "EEPRom.h"
#include "stm32f4xx_hal.h"
#include "stm32f4xx_hal_sram.h"
#include "stm32f4xx_hal_flash_ex.h"

#define SECTOR_20START 0x08180000 
#define SECTOR_21START 0x081A0000 
#define SECTOR_22START 0x081C0000 
#define SECTOR_23START 0x081E0000 

//#define WRITE_TIMEOUT 120000
#define WRITE_TIMEOUT 60000

#define SECTOR_SIZE	0x20000
#define STORAGE_SIZE 1024

static uint8_t StoredCache[STORAGE_SIZE];
static uint32_t *CurrentVersion = (uint32_t*)StoredCache;

volatile uint8_t *__CurrentStart;
volatile uint32_t __LastWrite;

static bool Changed = false;
static bool Pending = false;
static bool DataFound = false;
static bool Suspended = false;
static bool WriteError = false;

uint32_t GetSectorAddr(uint32_t addr)
{
	if (addr >= SECTOR_20START && addr < SECTOR_21START)
		return SECTOR_20START;

	if (addr >= SECTOR_21START && addr < SECTOR_22START)
		return SECTOR_21START;

	if (addr >= SECTOR_22START && addr < SECTOR_23START)
		return SECTOR_22START;

	if (addr >= SECTOR_23START && addr < SECTOR_23START + SECTOR_SIZE)
		return SECTOR_23START;

	return 0;
}

uint32_t GetSectorNum(uint32_t addr)
{
	if (addr >= SECTOR_20START && addr < SECTOR_21START)
		return 20;

	if (addr >= SECTOR_21START && addr < SECTOR_22START)
		return 21;

	if (addr >= SECTOR_22START && addr < SECTOR_23START)
		return 22;

	if (addr >= SECTOR_23START && addr < SECTOR_23START + SECTOR_SIZE)
		return 23;

	return 0;
}

uint32_t scanStart()
{
	uint32_t val = SECTOR_20START;
	uint32_t cstart = 0;
	uint32_t found_v = 0;

	while (true)
	{
		if (val == SECTOR_23START + STORAGE_SIZE)
		{
			return cstart;
		}

		uint32_t *test = (uint32_t *)val;
		
		if ((*test) != 0xFFFFFFFF)
		{
			if (*test > found_v)
			{
				found_v = *test;
				cstart = val;
			}
		}

		val += STORAGE_SIZE;
	}
	
	return val;
}

bool isBufferDiff(uint32_t *b1, uint32_t *b2, uint32_t len)
{
	for (uint32_t i = 0; i < len; i++)
	{
		if (*(b1 + i) != *(b2 + i)) return true;
	}

	return false;
}

bool EEPRom::Init()
{

	HAL_NVIC_SetPriorityGrouping(NVIC_PRIORITYGROUP_1);
	HAL_NVIC_SetPriority(FLASH_IRQn, 10, 0);
	HAL_NVIC_EnableIRQ(FLASH_IRQn);

	__CurrentStart = (uint8_t*)scanStart();

	if (__CurrentStart)
	{
		for (uint32_t i = 0; i < STORAGE_SIZE; i++)
			*(StoredCache + i) = *(__CurrentStart + i);

		DataFound = true;
	}
	else
	{
		__CurrentStart = (uint8_t*)SECTOR_20START;

		for (uint32_t i = 0; i < STORAGE_SIZE; i++)
			*(StoredCache + i) = 0;

		DataFound = false;
	}
}

bool EEPRom::Write(uint16_t addr, int16_t val)
{
	int16_t* __sc = (int16_t*)(StoredCache + addr);
	Changed = Changed ||  (*__sc) != val;
	*__sc = val;

	return true;
}

bool EEPRom::Write(uint16_t addr, int32_t val)
{
	int32_t* __sc = (int32_t*)(StoredCache + addr);
	Changed = Changed || (*__sc) != val;
	*__sc = val;

	return true;
}

bool EEPRom::Read(uint16_t addr, int16_t *val)
{
	if (!DataFound) return false;

	int16_t* __sc = (int16_t*)(StoredCache + addr);
	*val = *__sc;

	return true;
}

bool EEPRom::Read(uint16_t addr, int32_t *val)
{
	if (!DataFound) return false;

	int32_t* __sc = (int32_t*)(StoredCache + addr);
	*val = *__sc;

	return true;
}

bool EEPRom::ReadDirty(uint16_t addr, int16_t *val)
{
	int16_t* __sc = (int16_t*)(StoredCache + addr);
	*val = *__sc;

	return true;
}

bool EEPRom::ReadDirty(uint16_t addr, int32_t *val)
{
	int32_t* __sc = (int32_t*)(StoredCache + addr);
	*val = *__sc;

	return true;
}

void EEPRom::Ping()
{
	if (Pending || Suspended) return;

	if (HAL_GetTick() - __LastWrite < WRITE_TIMEOUT) return;

	__LastWrite = HAL_GetTick();

	Changed = Changed && isBufferDiff((uint32_t*)StoredCache, (uint32_t*)__CurrentStart, STORAGE_SIZE / 4);
	if (!Changed) return;

	uint32_t new_addr = (uint32_t)SECTOR_20START;
	while (*((uint32_t*)new_addr) != 0xFFFFFFFF && new_addr<SECTOR_23START + SECTOR_SIZE) new_addr += STORAGE_SIZE;

	*CurrentVersion = *CurrentVersion + 1;

	HAL_FLASH_Unlock();

	uint32_t serrors = 0;

	
//	if (new_addr != SECTOR_20START)
	if (new_addr >= SECTOR_23START + SECTOR_SIZE)
	{
		Pending = true;

		FLASH_EraseInitTypeDef fes;
		fes.Sector = 20;
		fes.NbSectors = 4;
		fes.TypeErase = TYPEERASE_SECTORS;
		fes.VoltageRange = VOLTAGE_RANGE_3;

		HAL_FLASHEx_Erase_IT(&fes);
		return;
	}

	bool error = false;

	for (uint32_t i = 0; i < STORAGE_SIZE; i++)
	{
		if (HAL_FLASH_Program(TYPEPROGRAM_BYTE, new_addr + i,
			*(StoredCache + i)) != HAL_OK)
		{
			error = true;
			break;
		}
	}

	WriteError = error;

	if (!error)
	{
		Changed = false;
		DataFound = true;
		__CurrentStart = (uint8_t *)new_addr;
	}

	HAL_FLASH_Lock();
}

bool EEPRom::GetNotSaved()
{
	return Changed;
}

bool EEPRom::GetError()
{
	return WriteError;
}

void EEPRom::ForceSave()
{
	if (Changed)
		__LastWrite = 0;
}

void EEPRom::Suspend()
{
	Suspended = true;
}

void EEPRom::Resume()
{
	Suspended = false;
}

bool EEPRom::GetBusy()
{
	return Pending;
}

void HAL_FLASH_EndOfOperationCallback(uint32_t ReturnValue)
{
	if (ReturnValue != 0xFFFFFFFF) return;

	__LastWrite = 0;
	HAL_FLASH_Lock();
	Pending = false;
}

void  HAL_FLASH_OperationErrorCallback(uint32_t ReturnValue)
{
	HAL_FLASH_Lock();
	Pending = false;
}
