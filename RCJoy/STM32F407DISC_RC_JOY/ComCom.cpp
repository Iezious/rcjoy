
#include <stdio.h>
#include <stdint.h>
#include "misc.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_usart.h"
#include "stm32f4xx_dma.h"

#include "conf.h"
#include "def.h"
#include "ComCom.h"
#include "EEPRom.h"

extern void SendUSBReportHeader();
extern void SendDataMap();
extern void SendPPMState();
extern void SetVariable(uint8_t idx, axis_t val);
extern void GetEEPVariable(u16 idx);
extern void SetEEPVariable(u16 idx, s16 val);


/******************************************************************************************************************************/
/**  UART Proc
/**
/**  **/

enum ComConStep { WaitHeader, WaitSHeader, WaitLength, WaitCommand, WaitData, WaitAnswer };

typedef struct
{
	ComConStep step;
	uint8_t position;
	uint8_t command;
	uint8_t length;
	uint8_t comandbuffer[32];
} ComConState;

static  ComConState State;

static void uart_init(uint32_t baudrate)
{
	USART_InitTypeDef USART_InitStructure;
	GPIO_InitTypeDef GPIO_InitStructure;
	NVIC_InitTypeDef nvic;

	/* USARTx configured as follow:
	- BaudRate = 230400 baud
	- Word Length = 8 Bits
	- One Stop Bit
	- No parity
	- Hardware flow control disabled (RTS and CTS signals)
	- Receive and transmit enabled
	*/

	/* Enable GPIO clock */
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);

	/* Enable UART clock */
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);

	/* USARTx configured */
	USART_InitStructure.USART_BaudRate = baudrate;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_InitStructure.USART_Mode = USART_Mode_Tx | USART_Mode_Rx;
	USART_Init(USART3, &USART_InitStructure);

	/* Connect PXx to USARTx_Tx*/
	GPIO_PinAFConfig(GPIOC, GPIO_PinSource10, GPIO_AF_USART3);
	GPIO_PinAFConfig(GPIOC, GPIO_PinSource11, GPIO_AF_USART3);

	/* Configure USART Tx as alternate function  */
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;

	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11 | GPIO_Pin_10;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);


	/* Enable USART */
	USART_Cmd(USART3, ENABLE);

	USART_ITConfig(USART3, USART_IT_RXNE, ENABLE);

	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);
	nvic.NVIC_IRQChannel = USART3_IRQn;
	nvic.NVIC_IRQChannelCmd = ENABLE;
	nvic.NVIC_IRQChannelPreemptionPriority = 0;
	nvic.NVIC_IRQChannelSubPriority = 0;
	NVIC_Init(&nvic);
}



void USART3_IRQHandler()
{
	if (USART_GetITStatus(USART3, USART_IT_RXNE) != RESET)
	{
		uint8_t data = USART3->DR;

		switch (State.step)
		{
		case WaitHeader:
			if (data == 0xFF)
				State.step = WaitSHeader;

			break;

		case WaitSHeader:
			if (data == 0xFF)
				State.step = WaitLength;
			else
				State.step = WaitHeader;

			break;

		case WaitLength:
			if (data > 0 && data < 32)
			{
				State.step = WaitCommand;
				State.length = data;
			}
			else
				State.step = WaitHeader;

			break;

		case WaitCommand:
			State.command = data;
			State.length--;
			State.position = 0;

			if (State.length)
				State.step = WaitData;
			else
				State.step = WaitAnswer;

			break;

		case WaitData:
			State.comandbuffer[State.position++] = data;
			State.length--;

			if (State.length)
				State.step = WaitData;
			else
				State.step = WaitAnswer;

			break;
		}

		//		USART_ClearITPendingBit(USART3, USART_IT_RXNE);
	}
}


/******************************************************************************************************************************/
/**  DMA Proc
/**
/**  **/

typedef struct
{
	uint8_t *buffer;
	uint16_t len;
} SendRec;

struct
{
	uint8_t head;
	uint8_t tail;

	SendRec Queue[16];
} DMA_QUEUE;

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
}

static void uart_dma_init()
{
	dma_init_queue();

	NVIC_InitTypeDef dma_nvic;

	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_DMA1, ENABLE);

	dma_nvic.NVIC_IRQChannel = DMA1_Stream3_IRQn;
	dma_nvic.NVIC_IRQChannelCmd = ENABLE;
	dma_nvic.NVIC_IRQChannelPreemptionPriority = 1;
	dma_nvic.NVIC_IRQChannelSubPriority = 0;
	NVIC_Init(&dma_nvic);

	USART_DMACmd(USART3, USART_DMAReq_Tx, DISABLE);
	DMA_Cmd(DMA1_Stream3, DISABLE);
}

void dma_send_from_queue()
{
	if (DMA_QUEUE.head == DMA_QUEUE.tail) return;

	while (DMA1_Stream3->CR & DMA_SxCR_EN);

	SendRec* rec = DMA_QUEUE.Queue + DMA_QUEUE.head;
	DMA_QUEUE.head = (DMA_QUEUE.head + 1) & 0x0F;

	DMA_InitTypeDef dma;

	DMA_DeInit(DMA1_Stream3);
	dma.DMA_Channel = DMA_Channel_4;
	dma.DMA_DIR = DMA_DIR_MemoryToPeripheral;
	dma.DMA_FIFOMode = DMA_FIFOMode_Disable;
	dma.DMA_FIFOThreshold = DMA_FIFOThreshold_Full;
	dma.DMA_MemoryBurst = DMA_MemoryBurst_Single;
	dma.DMA_MemoryDataSize = DMA_MemoryDataSize_Byte;
	dma.DMA_MemoryInc = DMA_MemoryInc_Enable;
	dma.DMA_Mode = DMA_Mode_Normal;
	dma.DMA_PeripheralBaseAddr = (uint32_t)&(USART3->DR);
	dma.DMA_PeripheralBurst = DMA_PeripheralBurst_Single;
	dma.DMA_PeripheralDataSize = DMA_PeripheralDataSize_Byte;
	dma.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
	dma.DMA_Priority = DMA_Priority_Medium;
	DMA_Init(DMA1_Stream3, &dma);

	DMA_ITConfig(DMA1_Stream3, DMA_IT_TC | DMA_IT_TE, ENABLE);

	DMA1_Stream3->NDTR = rec->len;
	DMA1_Stream3->M0AR = (uint32_t)rec->buffer;

	USART_DMACmd(USART3, USART_DMAReq_Tx, ENABLE);
	DMA_Cmd(DMA1_Stream3, ENABLE);
}

void DMA1_Stream3_IRQHandler()
{
	if (DMA_GetITStatus(DMA1_Stream3, DMA_IT_TCIF3))
	{
		USART_DMACmd(USART3, USART_DMAReq_Tx, DISABLE);
		DMA_Cmd(DMA1_Stream3, DISABLE);
		DMA1->LIFCR |= DMA_LIFCR_CTCIF3;

		//		dma_send_from_queue();
		return;
	}

	USART_DMACmd(USART3, USART_DMAReq_Tx, DISABLE);
	DMA_Cmd(DMA1_Stream3, DISABLE);

	DMA1->LIFCR |= DMA_LIFCR_CDMEIF3 | DMA_LIFCR_CFEIF3 | DMA_LIFCR_CTEIF3 | DMA_LIFCR_CTCIF3 | DMA_LIFCR_CHTIF3;
}

/******************************************************************************************************************************/
/**  ComCon
/**
/**  **/

static uint8_t comcon_send_header[5];

void ComCom::Start(uint16_t baud)
{
	uart_init(baud);
	uart_dma_init();

	comcon_send_header[0] = 0xFF;
	comcon_send_header[1] = 0xFF;


	State.step = WaitHeader;
	State.position = 0;
}

void ComCom::Send(uint8_t command, uint16_t len, uint8_t *buffer)
{
	comcon_send_header[2] = len >> 8 & 0xFF;
	comcon_send_header[3] = (len + 1) & 0xFF;
	comcon_send_header[4] = command;

	dma_enque(comcon_send_header, 5);
	if (len > 0)	dma_enque(buffer, len);
}

void ComCom::Send(char* str)
{
	int l = 0;
	while (*(str + l)) l++;
	Send((uint8_t*)str, l);
}

void ComCom::Send(uint8_t *buffer, uint32_t len)
{
	dma_enque(buffer, len);

	/*
	while (--len)
	{
	while (!(USART3->SR & USART_SR_TXE));
	USART3->DR = *(buffer++);
	}

	return;
	*/

}

void ComCom::Ping()
{
	if (!(DMA1_Stream3->CR & DMA_SxCR_EN))
		dma_send_from_queue();

	if (State.step != WaitAnswer) return;

	switch (State.command)
	{
		case 0x01:
			SendUSBReportHeader();
			break;

		case 0x02:
			SendDataMap();
			break;

		case 0x03:
		{
			uint8_t idx = State.comandbuffer[0];
			axis_t val = State.comandbuffer[1] << 8 | State.comandbuffer[2];
			SetVariable(idx, val);
			break;
		}

		case 0x04:
			COMCOM.Send("Echo");
			break;

		case 0x05:
			SendPPMState();
			break;

		case 0x06:
		{
			u16 idx = State.comandbuffer[0] << 8 | State.comandbuffer[1];
			GetEEPVariable(idx);
			break;
		}

		case 0x07:
		{
			u16 idx = State.comandbuffer[0] << 8 | State.comandbuffer[1];
			s16 val = State.comandbuffer[2] << 8 | State.comandbuffer[3];
			SetEEPVariable(idx, val);
			break;
		}



	}


	State.step = WaitHeader;
}
