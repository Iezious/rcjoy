#include "EEPRom.h"

static inline int16_t changeEndianness(int16_t val)
{
	return (val << 8) |  ((val >> 8) & 0x00ff); 
}

static inline uint16_t changeEndianness(uint16_t val)
{
	return (val << 8) |  ((val >> 8) & 0x00ff); 
}

static inline int32_t changeEndianness(int32_t val)
{
	return (val << 24) |
		((val <<  8) & 0x00ff0000) |
		((val >>  8) & 0x0000ff00) |
		((val >> 24) & 0x000000ff);
}

static inline uint32_t changeEndianness(uint32_t val)
{
	return (val << 24) |
		((val <<  8) & 0x00ff0000) |
		((val >>  8) & 0x0000ff00) |
		((val >> 24) & 0x000000ff);
}

union SendBuffer16Bit
{
	uint8_t arr[4];
	struct 
	{
		uint16_t addr;
		int16_t val;
	};
};

union SendBuffer32Bit
{
	uint8_t arr[6];
	struct 
	{
		uint16_t addr;
		int32_t val;
	};
};

union DataBuffer16BitU
{
	uint8_t arr[2];
	uint16_t val;
};

union DataBuffer16Bit
{
	uint8_t arr[2];
	int16_t val;
};

union DataBuffer32Bit
{
	uint8_t arr[4];
	int32_t val;
};

static bool EEPRomReady = false;

bool EEPRom::Init(I2C* port, uint8_t addr, EEPRomAddressing addmode)
{
	__I2CPort = port;
	__Addr = addr;
	__AddrMode = addmode;

	if(EEPRomReady = __I2CPort->StartWrite(__Addr))
		__I2CPort->Stop();

	return EEPRomReady;
}


bool EEPRom::Write(uint16_t addr, int16_t val)
{
	if(!EEPRomReady) return false;

	if(__AddrMode == A16Bit)
	{
		SendBuffer16Bit buffer;
		buffer.addr = changeEndianness(addr);
		buffer.val = val;

		return __I2CPort->Transmit(__Addr, buffer.arr, 4);
	}
	else
	{
		SendBuffer16Bit buffer;
		buffer.addr = changeEndianness((uint16_t)addr);
		buffer.val = val;

		return __I2CPort->Transmit(__Addr | (addr >> 8), buffer.arr+1, 3);
	}
}

bool EEPRom::Write(uint16_t addr, int32_t val)
{
	if(!EEPRomReady) return false;

	if(__AddrMode == A16Bit)
	{
		SendBuffer32Bit buffer;
		buffer.addr = changeEndianness(addr);
		buffer.val = val;

		return __I2CPort->Transmit(__Addr, buffer.arr, 6);
	}
	else
	{
		SendBuffer32Bit buffer;
		buffer.addr = changeEndianness((uint16_t)addr);
		buffer.val = val;

		return __I2CPort->Transmit(__Addr | (addr >> 8), buffer.arr+1, 5);
	}
}


bool EEPRom::Read(uint16_t addr, int16_t *val)
{
	if(!EEPRomReady) return false;

	if(__AddrMode == A16Bit)
	{
		DataBuffer16BitU baddr;
		baddr.val = changeEndianness(addr);

		if(!(__I2CPort->StartWrite(__Addr) && __I2CPort->Write(baddr.arr,2))) 
		{
			__I2CPort->Stop();
			return false;
		}

		DataBuffer16Bit bval;
		if(!(__I2CPort->StartRead(__Addr) && __I2CPort->Receive(bval.arr, 2)))
		{
			__I2CPort->Stop();
			return false;
		}

		__I2CPort->Stop();
		*(val) = bval.val;
		return true;
	}
	else
	{
		if(!(__I2CPort->StartWrite(__Addr | (addr >> 8)) && __I2CPort->Write((uint8_t)addr))) 
		{
			__I2CPort->Stop();
			return false;
		}

		DataBuffer16Bit bval;
		if(!(__I2CPort->StartRead(__Addr | (addr >> 8)) && __I2CPort->Receive(bval.arr, 2)))
		{
			__I2CPort->Stop();
			return false;
		}

		__I2CPort->Stop();
		*(val) = bval.val;
		return true;
	}
}

bool EEPRom::Read(uint16_t addr, int32_t *val)
{
	if(!EEPRomReady) return false;

	if(__AddrMode == A16Bit)
	{
		DataBuffer16BitU baddr;
		baddr.val = changeEndianness(addr);

		if(!(__I2CPort->StartWrite(__Addr) && __I2CPort->Write(baddr.arr,2))) 
		{
			__I2CPort->Stop();
			return false;
		}

		DataBuffer32Bit bval;
		if(!(__I2CPort->StartRead(__Addr) && __I2CPort->Receive(bval.arr, 4)))
		{
			__I2CPort->Stop();
			return false;
		}

		*(val) = bval.val;
		return true;
	}
	else
	{
		DataBuffer16BitU baddr;
		baddr.val = changeEndianness(addr);

		if(!(__I2CPort->StartWrite(__Addr | (addr >> 8)) && __I2CPort->Write((uint8_t)addr))) 
		{
			__I2CPort->Stop();
			return false;
		}

		DataBuffer32Bit bval;
		if(!(__I2CPort->StartRead(__Addr | (addr >> 8)) && __I2CPort->Receive(bval.arr, 4)))
		{
			__I2CPort->Stop();
			return false;
		}

		*(val) = bval.val;
		return true;
	}
}