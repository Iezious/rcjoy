#pragma once

#include <stdint.h>

class ComCom
{

public:
	void Start(uint16_t boud);
	void Send(char* str);
	void Send(uint8_t* buffer, uint32_t len);
	void Send(uint8_t command, uint16_t leng, uint8_t *buffer);

	void Ping();
};

extern ComCom COMCOM;

extern "C" void USART3_IRQHandler();
extern "C" void DMA1_Stream3_IRQHandler();

