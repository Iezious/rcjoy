
#include <stdio.h>
#include <stdint.h>
//#include "misc.h"
#include "stm32f4xx_hal.h"
#include "stm32f4xx_hal_uart.h"
#include "stm32f4xx_hal_dma.h"
#include "stm32f4xx_hal_rcc.h"

#include "conf.h"
#include "def.h"
#include "ComCom.h"
//#include "EEPRom.h"

/******************************************************************************************************************************/
/**  UART Proc
/**
/**  **/

static void ReadByte(uint8_t data);
enum ComStateDef { Idle, WaitHeader, WaitSHeader, WaitLength, WaitCommand, WaitData, WaitAnswer, TX_Busy };

typedef struct
{
	ComStateDef RXState;
	ComStateDef TXState;

	uint16_t RXPosition;

	union 
	{
		uint8_t headerbuffer[4];
		struct 
		{
			uint8_t check1;
			uint8_t check2;
			uint8_t length;
			uint8_t command;
		};
	};

	uint8_t databuffer[256];
	uint32_t StartWaitData;

} ComConState;

static  ComConState State;

UART_HandleTypeDef huart1;
DMA_HandleTypeDef hdma_tx;
DMA_HandleTypeDef hdma_rx;

/* USART1 init function */

/******************************************************************************************************************************/
/**  HW Init
/**
/**  **/

void MX_USART1_UART_Init(uint32_t baud)
{

	huart1.Instance = USART1;
	huart1.Init.BaudRate = baud;
	huart1.Init.WordLength = UART_WORDLENGTH_8B;
	huart1.Init.StopBits = UART_STOPBITS_1;
	huart1.Init.Parity = UART_PARITY_NONE;
	huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
	huart1.Init.Mode = UART_MODE_TX_RX;

	HAL_UART_Init(&huart1);

}

void HAL_UART_MspInit(UART_HandleTypeDef* huart)
{

	GPIO_InitTypeDef GPIO_InitStruct;
	if (huart->Instance == USART1)
	{
		/* Peripheral clock enable */
		__GPIOA_CLK_ENABLE();
		__USART1_CLK_ENABLE();
		__DMA2_CLK_ENABLE();

		/**USART1 GPIO Configuration
		PA9     ------> USART1_TX
		PA10     ------> USART1_RX
		*/
		GPIO_InitStruct.Pin = GPIO_PIN_9 | GPIO_PIN_10;
		GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
		GPIO_InitStruct.Pull = GPIO_NOPULL;
		GPIO_InitStruct.Speed = GPIO_SPEED_FAST;
		GPIO_InitStruct.Alternate = GPIO_AF7_USART1;
		HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

		/* Peripheral DMA init*/
		hdma_tx.Instance = DMA2_Stream7;
		hdma_tx.Init.Channel = DMA_CHANNEL_4;
		hdma_tx.Init.Direction = DMA_MEMORY_TO_PERIPH;
		hdma_tx.Init.PeriphInc = DMA_PINC_DISABLE;
		hdma_tx.Init.MemInc = DMA_MINC_ENABLE;
		hdma_tx.Init.PeriphDataAlignment = DMA_PDATAALIGN_BYTE;
		hdma_tx.Init.MemDataAlignment = DMA_MDATAALIGN_BYTE;
		hdma_tx.Init.Mode = DMA_NORMAL;
		hdma_tx.Init.Priority = DMA_PRIORITY_LOW;
		hdma_tx.Init.FIFOMode = DMA_FIFOMODE_DISABLE;
		hdma_tx.Init.FIFOThreshold = DMA_FIFO_THRESHOLD_FULL;
		hdma_tx.Init.MemBurst = DMA_MBURST_INC4;
		hdma_tx.Init.PeriphBurst = DMA_PBURST_INC4;

		HAL_DMA_Init(&hdma_tx);

		__HAL_LINKDMA(huart, hdmatx, hdma_tx);

		HAL_NVIC_SetPriority(DMA2_Stream7_IRQn, 0, 1);
		HAL_NVIC_EnableIRQ(DMA2_Stream7_IRQn);

#ifdef RDMA
		/* Configure the DMA handler for Transmission process */
		hdma_rx.Instance = DMA2_Stream5;
		hdma_rx.Init.Channel = DMA_CHANNEL_4;
		hdma_rx.Init.Direction = DMA_PERIPH_TO_MEMORY;
		hdma_rx.Init.PeriphInc = DMA_PINC_DISABLE;
		hdma_rx.Init.MemInc = DMA_MINC_ENABLE;
		hdma_rx.Init.PeriphDataAlignment = DMA_PDATAALIGN_BYTE;
		hdma_rx.Init.MemDataAlignment = DMA_MDATAALIGN_BYTE;
		hdma_rx.Init.Mode = DMA_NORMAL;
		hdma_rx.Init.Priority = DMA_PRIORITY_HIGH;
		hdma_rx.Init.FIFOMode = DMA_FIFOMODE_DISABLE;
		hdma_rx.Init.FIFOThreshold = DMA_FIFO_THRESHOLD_FULL;
		hdma_rx.Init.MemBurst = DMA_MBURST_INC4;
		hdma_rx.Init.PeriphBurst = DMA_PBURST_INC4;

		HAL_DMA_Init(&hdma_rx);

		/* Associate the initialized DMA handle to the the UART handle */
		__HAL_LINKDMA(huart, hdmarx, hdma_rx);


		HAL_NVIC_SetPriority(DMA2_Stream5_IRQn, 0, 1);
		HAL_NVIC_EnableIRQ(DMA2_Stream5_IRQn);
#endif

		HAL_NVIC_SetPriority(USART1_IRQn, 2, 0);
		HAL_NVIC_EnableIRQ(USART1_IRQn);

		__HAL_UART_ENABLE_IT(&huart1, UART_IT_RXNE);

	}
}

void HAL_UART_MspDeInit(UART_HandleTypeDef* huart)
{

	if (huart->Instance == USART1)
	{
		/* Peripheral clock disable */
		__USART1_CLK_DISABLE();

		/**USART1 GPIO Configuration
		PA9     ------> USART1_TX
		PA10     ------> USART1_RX
		*/
		HAL_GPIO_DeInit(GPIOA, GPIO_PIN_9 | GPIO_PIN_10);

		/* Peripheral DMA DeInit*/
		HAL_DMA_DeInit(huart->hdmatx);

		/* Peripheral interrupt Deinit*/
		HAL_NVIC_DisableIRQ(USART1_IRQn);
	}
}

/******************************************************************************************************************************/
/**  IRq
/**
/**  **/

void USART1_IRQHandler(void)
{
	if (__HAL_UART_GET_FLAG(&huart1, USART_FLAG_RXNE) != RESET)
	{
		ReadByte((uint8_t)(huart1.Instance->DR & (uint8_t)0x00FF));
	}

//	HAL_NVIC_ClearPendingIRQ(USART1_IRQn);
//	HAL_UART_IRQHandler(&COMCOM_USART);
}

void DMA2_Stream7_IRQHandler(void)
{
	HAL_NVIC_ClearPendingIRQ(DMA2_Stream7_IRQn);
	HAL_DMA_IRQHandler(&hdma_tx);
}

#ifdef RDMA 
void DMA2_Stream5_IRQHandler(void)
{
	HAL_NVIC_ClearPendingIRQ(DMA2_Stream5_IRQn);
	HAL_DMA_IRQHandler(&hdma_rx);
}

#endif

/******************************************************************************************************************************/
/**  Transmit procedures
/**
/**  **/

typedef struct
{
	uint8_t *buffer;
	uint16_t len;
} SendRec;

static struct
{
	uint8_t head;
	uint8_t tail;

	SendRec Queue[16];
} DMA_QUEUE;


void dma_send_from_queue()
{
	if (State.TXState == TX_Busy)
		return;

	if (DMA_QUEUE.head == DMA_QUEUE.tail)
		return;

	State.TXState = TX_Busy;

	SendRec* rec = DMA_QUEUE.Queue + DMA_QUEUE.head;
	DMA_QUEUE.head = (DMA_QUEUE.head + 1) & 0x0F;

	HAL_UART_Transmit_DMA(&COMCOM_USART, rec->buffer, rec->len);
}

static void dma_init_queue()
{
	DMA_QUEUE.head = 0;
	DMA_QUEUE.tail = 0;
}

static void dma_enque(uint8_t *buffer, uint16_t len)
{
	if (len == 0) return;

	SendRec* rec = DMA_QUEUE.Queue + DMA_QUEUE.tail;
	rec->len = len;
	rec->buffer = buffer;

	DMA_QUEUE.tail = (DMA_QUEUE.tail + 1) & 0x0F;
	dma_send_from_queue();
}

void HAL_UART_TxCpltCallback(UART_HandleTypeDef* h)
{
	State.TXState = Idle;
	dma_send_from_queue();
}


/******************************************************************************************************************************/
/**  Rx procedures
/**
/**  **/


static void ReadByte(uint8_t data)
{
	switch (State.RXState)
	{
		case Idle:
		case WaitHeader:
			if (data == 0xFF)
				State.RXState = WaitSHeader;

			break;

		case WaitSHeader:
			if (data == 0xFF)
				State.RXState = WaitLength;
			else
				State.RXState = WaitHeader;

			break;

		case WaitLength:
			if (data > 0 && data < 254)
			{
				State.RXState = WaitCommand;
				State.length = data;
			}
			else
				State.RXState = WaitHeader;

			break;

		case WaitCommand:
			State.command = data;
			State.length--;
			State.RXPosition = 0;

			if (State.length)
				State.RXState = WaitData;
			else
				State.RXState = WaitAnswer;

			break;

		case WaitData:
			State.databuffer[State.RXPosition++] = data;
			State.length--;

			if (State.length)
				State.RXState = WaitData;
			else
				State.RXState = WaitAnswer;

			break;
	}
}

#ifdef RDMA

void StartListen()
{
	switch (State.RXState)
	{
		case Idle:
			State.RXState = RX_WaitHeader;

		case RX_WaitHeader:
			if (HAL_UART_Receive_DMA(&huart1, State.headerbuffer, 4) != HAL_OK)
				State.RXState = Idle;

			break;

		case RX_WaitData:
			State.StartWaitData = HAL_GetTick();

			if (HAL_UART_Receive_DMA(&huart1, State.databuffer, State.length-1) != HAL_OK)
				State.RXState = Idle;

			break;
	}
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef* h)
{
	switch (State.RXState)
	{
		case RX_WaitHeader:

			if (State.check1 == 0xFF && State.check2 == 0xFF)
				State.RXState = RX_WaitData;

			StartListen();
			break;

		case RX_WaitData:
			State.RXState = RX_WaitAnswer;
			break;
	}
}

#endif

/******************************************************************************************************************************/
/**  ComCon
/**
/**  **/

void HAL_UART_ErrorCallback(UART_HandleTypeDef* h)
{
	HAL_UART_DMAStop(h);

	State.RXState = WaitHeader;
	State.TXState = Idle;
}


static uint8_t comcon_send_header[5];

static ComCallBack CommandMap[255];


void ComCom::Send(char* str)
{

	uint32_t i = 0;
	while (*(str + i)) i++;

	comcon_send_header[2] = i >> 8 & 0xFF;
	comcon_send_header[3] = (i + 1) & 0xFF;
	comcon_send_header[4] = 0xFE;

	dma_enque(comcon_send_header, 5);
	dma_enque((uint8_t *)str, i);
}

void ComCom::Start(uint32_t baud)
{
	dma_init_queue();

	comcon_send_header[0] = 0xFF;
	comcon_send_header[1] = 0xFF;

	State.RXState = Idle;
	State.TXState = WaitHeader;

	MX_USART1_UART_Init(baud);
//	StartListen();
}

void ComCom::RegisterCommand(uint32_t cmd, ComCallBack cb)
{
	CommandMap[cmd] = cb;
}

void ComCom::Ping()
{
	if (State.TXState == Idle)
		dma_send_from_queue();
	
	/*
	if (State.RXState == RX_WaitData && (HAL_GetTick() - State.StartWaitData > 5000))
	{
		HAL_UART_DMAStop(&huart1);
		State.TXState == Idle;
		State.RXState == Idle;
		return;
	}
	*/

	if (State.RXState != WaitAnswer) return;

	ComCallBack cb = CommandMap[State.command];
	if (cb == 0)
	{
		comcon_send_header[0] = 0xFF;
		comcon_send_header[1] = 0xFE;
		comcon_send_header[2] = 0;
		comcon_send_header[3] = 1;
		comcon_send_header[4] = State.command;

		dma_enque(comcon_send_header, 5);
		dma_send_from_queue();

		State.RXState = WaitHeader;
//		StartListen();
		return;
	}

	uint8_t *abuffer;
	uint32_t alen;

	(*cb)(State.databuffer, State.RXPosition, &abuffer, &alen);

	comcon_send_header[0] = 0xFF;
	comcon_send_header[1] = 0xFF;
	comcon_send_header[2] = alen >> 8 & 0xFF;
	comcon_send_header[3] = (alen + 1) & 0xFF;
	comcon_send_header[4] = State.command;

	dma_enque(comcon_send_header, 5);
	if (alen > 0)	dma_enque(abuffer, alen);

	dma_send_from_queue();

	State.RXState = WaitHeader;
//	StartListen();
}
