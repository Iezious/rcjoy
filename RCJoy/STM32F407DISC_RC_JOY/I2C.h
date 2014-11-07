#pragma once

#include <stdint.h>
#include <stm32f4xx.h>
#include <stm32f4xx_gpio.h>
#include <stm32f4xx_fsmc.h>
#include <stm32f4xx_rcc.h>
#include <stm32f4xx_dac.h>
#include <stm32f4xx_i2c.h>

#define I2C_TIMEOUT 48000

class I2C
{
private:
	I2C_TypeDef* __I2Cx;

public:
	void Init(I2C_TypeDef* I2Cx, uint32_t speed = 400000);

	bool Write( uint8_t data);
	bool Write(uint8_t* data, int length);
	bool Receive(uint8_t* buffer, uint16_t length);

	bool StartRead(uint8_t addr);
	bool StartWrite(uint8_t addr);
	bool ReadSelect(uint8_t addr);

	bool Stop();

	bool Transmit(uint8_t addr, uint8_t data);
	bool Transmit(uint8_t addr, uint8_t* data, int length);
};

