#pragma once

#include <stdint.h>
#include "stm32f4xx_hal.h"

#define COMCOM_USART huart1

typedef void(*ComCallBack)(uint8_t * buffer, uint32_t blen, uint8_t **answer, uint32_t *answer_len);


class ComCom
{

public:
	void Start(uint32_t boud);
	void Send(char* str);
	/*
	void Send(uint8_t* buffer, uint32_t len);
	void Send(uint8_t command, uint16_t leng, uint8_t *buffer);
	*/
	void RegisterCommand(uint32_t cmd, ComCallBack cb);
	void Ping();
};


#ifdef __cplusplus 
extern "C"
{
#endif

extern ComCom COMCOM;

extern UART_HandleTypeDef huart1;
extern DMA_HandleTypeDef hdma_tx;

extern void USART1_IRQHandler();
extern void DMA2_Stream7_IRQHandler();
extern void DMA2_Stream5_IRQHandler();

extern void HAL_UART_MspInit(UART_HandleTypeDef* huart);
extern void HAL_UART_MspDeInit(UART_HandleTypeDef* huart);

extern void HAL_UART_RxCpltCallback(UART_HandleTypeDef* h);
extern void HAL_UART_TxCpltCallback(UART_HandleTypeDef* h);
extern void HAL_UART_ErrorCallback(UART_HandleTypeDef* h);

#ifdef __cplusplus 
}
#endif