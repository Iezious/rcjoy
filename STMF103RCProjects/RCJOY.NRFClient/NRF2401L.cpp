
#include "NRF2401L.h"
#include "stm32f10x_spi.h"
#include "stdint.h"
#include "stm32f10x_gpio.h"
#include "stm32f10x_rcc.h"
#include "SysTimer.h"


//Define the commands for operate the nRF24L01P
#define READ_nRF_REG    0x00  // Command for read register
#define WRITE_nRF_REG   0x20 	// Command for write register
#define RD_RX_PLOAD     0x61  // Command for read Rx payload
#define WR_TX_PLOAD     0xA0  // Command for write Tx payload
#define FLUSH_TX        0xE1 	// Command for flush Tx FIFO
#define FLUSH_RX        0xE2  // Command for flush Rx FIFO
#define REUSE_TX_PL     0xE3  // Command for reuse Tx payload
#define NOP             0xFF  // Reserve

//Define the register address for nRF24L01P
#define CONFIG          0x00  //  Configurate the status of transceiver, mode of CRC and the replay of transceiver status
#define EN_AA           0x01  //  Enable the atuo-ack in all channels
#define EN_RXADDR       0x02  //  Enable Rx Address
#define SETUP_AW        0x03  // Configurate the address width
#define SETUP_RETR      0x04  //  setup the retransmit
#define RF_CH           0x05  // Configurate the RF frequency
#define RF_SETUP        0x06  // Setup the rate of data, and transmit power
#define STATUS		    0x07  //
#define OBSERVE_TX      0x08  //
#define CD              0x09  // Carrier detect
#define RX_ADDR_P0      0x0A  // Receive address of channel 0
#define RX_ADDR_P1      0x0B  // Receive address of channel 1
#define RX_ADDR_P2      0x0C  // Receive address of channel 2
#define RX_ADDR_P3      0x0D  // Receive address of channel 3
#define RX_ADDR_P4      0x0E  // Receive address of channel 4
#define RX_ADDR_P5      0x0F  // Receive address of channel 5
#define TX_ADDR         0x10  //       Transmit address
#define RX_PW_P0        0x11  //  Size of receive data in channel 0
#define RX_PW_P1        0x12  //  Size of receive data in channel 1
#define RX_PW_P2        0x13  //  Size of receive data in channel 2
#define RX_PW_P3        0x14  //  Size of receive data in channel 3
#define RX_PW_P4        0x15  // Size of receive data in channel 4
#define RX_PW_P5        0x16  //  Size of receive data in channel 5
#define FIFO_STATUS     0x17  // FIFO Status
// define  GPIO for SPI


#define CONFIG_PRIM_RX		0x01
#define CONFIG_PWR_UP		0x02
#define CONFIG_CRCO			0x04
#define CONFIG_EN_CRC		0x08
#define CONFIG_MASK_MAX_RT	0x10
#define CONFIG_MASK_TX_DS	0x20
#define CONFIG_MASK_RX_DR	0x30

#define SETUP_AW_3B			0x01
#define SETUP_AW_4B			0x10
#define SETUP_AW_5B			0x11


#define RF_SETUP_LNA_HCURR	0x01
#define RF_SETUP_RF_PWR_M18	0x00
#define RF_SETUP_RF_PWR_M12	0x02
#define RF_SETUP_RF_PWR_M6	0x04
#define RF_SETUP_RF_PWR_M0	0x06
#define RF_SETUP_RF_DR_1M	0x00
#define RF_SETUP_RF_DR_2M	0x08

#define STATUS_RX_DR		0x40
#define STATUS_TX_DS		0x20
#define STATUS_MAX_RT		0x10

#define SPI                   			  SPI1
#define GPIO_CS_CE                  	GPIOA
#define GPIO_Pin_CE              			GPIO_Pin_3
#define GPIO_Pin_CS              			GPIO_Pin_4

#define GPIO_SPI              			  GPIOA
#define GPIO_Pin_SPI_SCK      			  GPIO_Pin_5
#define GPIO_Pin_SPI_MISO     			  GPIO_Pin_6
#define GPIO_Pin_SPI_MOSI     			  GPIO_Pin_7

#define GPIO_Pin_SPI_CS_SOURCE        GPIO_PinSource4
#define GPIO_Pin_SPI_SCK_SOURCE       GPIO_PinSource5
#define GPIO_Pin_SPI_MISO_SOURCE      GPIO_PinSource6
#define GPIO_Pin_SPI_MOSI_SOURCE      GPIO_PinSource7

#define nRF24l01_IRQ    GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_2)

uint8_t TX_ADDRESS[5] = {0x11, 0x12, 0xFF, 0xAC, 0x15};
uint8_t RX_ADDRESS[5] = {0x11, 0x12, 0xFF, 0xAC, 0x15};

uint8_t RX_BUFFER[RX_PLOAD_WIDTH];
uint8_t TX_BUFFER[TX_PLOAD_WIDTH];

uint8_t CURRENT_CHANNEL = 24;
nRFState CurrentState = IDLE;

__attribute__((weak)) void nFR24l01_TX_Sent()  { }
__attribute__((weak)) void nFR24l01_TX_Fail() { }
__attribute__((weak)) void nFR24l01_RX_Data(u8* data) { }


inline void nRF24L01_SPI_NSS_H(void)
{
	GPIO_SetBits(GPIO_CS_CE, GPIO_Pin_CS);
}

inline void nRF24L01_SPI_NSS_L(void)
{
	GPIO_ResetBits(GPIO_CS_CE, GPIO_Pin_CS);
}

inline void nRF24L01_CE_L(void)
{
	GPIO_ResetBits(GPIO_CS_CE, GPIO_Pin_CE);
}

inline void nRF24L01_CE_H(void)
{
	GPIO_SetBits(GPIO_CS_CE, GPIO_Pin_CE);
}

#define nRF24L01_Delay_us spinWait

static void Initial_HW()
{
	GPIO_InitTypeDef GPIO_InitStruct;
	SPI_InitTypeDef SPI_InitStruct;

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_AFIO, ENABLE);
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_SPI1, ENABLE);

	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7;
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_AF_PP;
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;

	GPIO_Init(GPIOA, &GPIO_InitStruct);

	SPI_InitStruct.SPI_BaudRatePrescaler = SPI_BaudRatePrescaler_8;
	SPI_InitStruct.SPI_Direction = SPI_Direction_2Lines_FullDuplex;
	SPI_InitStruct.SPI_Mode = SPI_Mode_Master;
	SPI_InitStruct.SPI_DataSize = SPI_DataSize_8b;
	SPI_InitStruct.SPI_CPOL = SPI_CPOL_Low;
	SPI_InitStruct.SPI_CPHA = SPI_CPHA_1Edge;
	SPI_InitStruct.SPI_NSS = SPI_NSS_Soft;
	SPI_InitStruct.SPI_FirstBit = SPI_FirstBit_MSB;
	SPI_InitStruct.SPI_CRCPolynomial = 7;
	SPI_Init(SPI1, &SPI_InitStruct);
	SPI_CalculateCRC(SPI1, DISABLE);

	SPI_Cmd(SPI1, ENABLE);

	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_2;
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOA, &GPIO_InitStruct);

	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_4 | GPIO_Pin_3;
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOA, &GPIO_InitStruct);
}

static u8 nRF24L01_SPI_Send_Byte(u8 data)
{
	while (SPI_I2S_GetFlagStatus(SPI, SPI_I2S_FLAG_TXE) == RESET);
	SPI_I2S_SendData(SPI, data);

	while (SPI_I2S_GetFlagStatus(SPI, SPI_I2S_FLAG_RXNE) == RESET);
	return SPI_I2S_ReceiveData(SPI);
}

unsigned char SPI_Read_Buf(unsigned char reg, unsigned char *pBuf, unsigned char Len)
{
	unsigned int status, i;

	nRF24L01_SPI_NSS_L();                  // Set CSN low, init SPI tranaction
	nRF24L01_Delay_us(20);
	status = nRF24L01_SPI_Send_Byte(reg);  // Select register to write to and read status unsigned char

	for (i = 0; i<Len; i++)
	{
		pBuf[i] = nRF24L01_SPI_Send_Byte(0);
	}

	nRF24L01_SPI_NSS_H();

	return(status);                    // return nRF24L01 status unsigned char
}

unsigned char SPI_Write_Buf(unsigned char reg, unsigned char *pBuf, unsigned char Len)
{
	unsigned int status, i;

	nRF24L01_SPI_NSS_L();
	nRF24L01_Delay_us(20);
	status = nRF24L01_SPI_Send_Byte(reg);
	for (i = 0; i<Len; i++) //
	{
		nRF24L01_SPI_Send_Byte(*pBuf);
		pBuf++;
	}
	nRF24L01_SPI_NSS_H();
	return(status);
}

//Define the layer2 functions
static u8 SPI_RD_Reg(u8 reg)
{
	unsigned char reg_val;

	nRF24L01_SPI_NSS_L();                // CSN low, initialize SPI communication...
	nRF24L01_Delay_us(20);
	nRF24L01_SPI_Send_Byte(reg);            // Select register to read from..
	reg_val = nRF24L01_SPI_Send_Byte(0);    // ..then read register value
	nRF24L01_SPI_NSS_H();                // CSN high, terminate SPI communication

	return(reg_val);        // return register value
}

static u8 SPI_WR_Reg(u8 reg, u8 value)
{
	unsigned char status;

	nRF24L01_SPI_NSS_L();                  // CSN low, init SPI transaction
	nRF24L01_Delay_us(20);
	status = nRF24L01_SPI_Send_Byte(WRITE_nRF_REG | reg);// select register
	nRF24L01_SPI_Send_Byte(value);             // ..and write value to it..
	nRF24L01_SPI_NSS_H();                   // CSN high again

	return(status);            // return nRF24L01 status unsigned char
}

void nRF24_PowerUpRX()
{
	SPI_WR_Reg(RF_SETUP, RF_SETUP_LNA_HCURR | RF_SETUP_RF_DR_1M);
	SPI_WR_Reg(EN_AA, 0x1F); // all 5 pipes
	SPI_WR_Reg(RX_PW_P0, 32); 
	SPI_WR_Reg(EN_RXADDR, 0x01); 
	SPI_Write_Buf(RX_ADDR_P0, RX_ADDRESS, 5);
	SPI_WR_Reg(RF_CH, CURRENT_CHANNEL);

	SPI_WR_Reg(CONFIG, CONFIG_PWR_UP | CONFIG_EN_CRC | CONFIG_PRIM_RX | CONFIG_MASK_RX_DR);
	
	nRF24L01_CE_H();

	CurrentState = RX_ACTIVE;
}

void nRF24_PowerUpTX()
{
	SPI_WR_Reg(RF_SETUP, RF_SETUP_RF_PWR_M0 | RF_SETUP_RF_DR_1M);
	SPI_WR_Reg(EN_AA, 0x1F); // all 5 pipes
	SPI_WR_Reg(RX_PW_P0, 32);
	SPI_WR_Reg(EN_RXADDR, 0x01);
	SPI_Write_Buf(TX_ADDR, TX_ADDRESS, 5);
	SPI_Write_Buf(RX_ADDR_P0, TX_ADDRESS, 5);
	SPI_WR_Reg(RF_CH, CURRENT_CHANNEL);
	
	SPI_WR_Reg(CONFIG, CONFIG_PWR_UP | CONFIG_EN_CRC | CONFIG_MASK_TX_DS | CONFIG_MASK_MAX_RT);

	CurrentState = TX_READY;
}

void nRF24_SetChannel(uint8_t ch)
{
	CURRENT_CHANNEL = ch & 0x7F;
	SPI_WR_Reg(RF_CH, CURRENT_CHANNEL);
}

void nRF24_PowerDown()
{
	SPI_Write_Buf(FLUSH_TX, 0, 0);
	SPI_Write_Buf(FLUSH_RX, 0, 0);

	SPI_WR_Reg(CONFIG, 0);

	CurrentState = IDLE;
}


static void ReadRxData()
{
	nRF24L01_CE_L();

	nRF24L01_Delay_us(10);

	u8 status = SPI_RD_Reg(STATUS);
	if (status & STATUS_RX_DR)
	{
		while ((status & 0x0F) != 0x0E)
		{
			SPI_Read_Buf(RD_RX_PLOAD, RX_BUFFER, RX_PLOAD_WIDTH);
			nFR24l01_RX_Data(RX_BUFFER);

			status = SPI_RD_Reg(STATUS);
		}

		SPI_WR_Reg(STATUS, STATUS_RX_DR);
	}

	nRF24L01_CE_H();
}

static void CheckTxResult()
{
	nRF24L01_CE_L();

	nRF24L01_Delay_us(10);

	u8 status = SPI_RD_Reg(STATUS);

	if (status & STATUS_TX_DS)
	{
		SPI_WR_Reg(STATUS, STATUS_TX_DS);
		CurrentState = TX_READY;
		nFR24l01_TX_Sent();
	}
	else if (status & STATUS_MAX_RT)
	{
		SPI_WR_Reg(STATUS, STATUS_MAX_RT);
		CurrentState = TX_READY;
		nFR24l01_TX_Fail();
	}
}

uint8_t nRF24_SendData(uint8_t *buffer, uint8_t length)
{
	if (CurrentState != TX_READY) return 0;

	for (u8 i = 0; i < length && i < TX_PLOAD_WIDTH; i++) TX_BUFFER[i] = buffer[i];
	
	nRF24L01_CE_L();

	SPI_Write_Buf(FLUSH_TX, 0, 0);
	SPI_Write_Buf(WR_TX_PLOAD, TX_BUFFER, TX_PLOAD_WIDTH);

	CurrentState = TX_ACTIVE;
	nRF24L01_CE_H();
}

void nRF24_Tick()
{
	if (CurrentState == IDLE) return;

	if (CurrentState == RX_ACTIVE && nRF24l01_IRQ)
	{
		ReadRxData();
		return;
	}

	if (CurrentState == TX_ACTIVE && nRF24l01_IRQ)
	{
		CheckTxResult();
		return;
	}
}


