#include "I2C.h"


static void I2C1_Init(uint32_t speed)
{
	GPIO_InitTypeDef GPIO_InitStruct;
	I2C_InitTypeDef I2C_InitStruct;

	// enable APB1 peripheral clock for I2C1
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_I2C1, ENABLE);
	// enable clock for SCL and SDA pins 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOB, ENABLE);

	/* setup SCL and SDA pins
	* You can connect I2C1 to two different
	* pairs of pins:
	* 1. SCL on PB6 and SDA on PB7 
	* 2. SCL on PB8 and SDA on PB9
	*/
	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_6 | GPIO_Pin_7; // we are going to use PB6 and PB7
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_AF;			// set pins to alternate function
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;		// set GPIO speed
	GPIO_InitStruct.GPIO_OType = GPIO_OType_OD;			// set output to open drain --> the line has to be only pulled low, not driven high
	GPIO_InitStruct.GPIO_PuPd = GPIO_PuPd_UP;			// enable pull up resistors
	GPIO_Init(GPIOB, &GPIO_InitStruct);					// init GPIOB

	// Connect I2C1 pins to AF  
	GPIO_PinAFConfig(GPIOB, GPIO_PinSource6, GPIO_AF_I2C1);	// SCL
	GPIO_PinAFConfig(GPIOB, GPIO_PinSource7, GPIO_AF_I2C1); // SDA

	// configure I2C1 
	I2C_InitStruct.I2C_ClockSpeed = speed; 		// 100kHz
	I2C_InitStruct.I2C_Mode = I2C_Mode_I2C;			// I2C mode
	I2C_InitStruct.I2C_DutyCycle = I2C_DutyCycle_2;	// 50% duty cycle --> standard
	I2C_InitStruct.I2C_OwnAddress1 = 0x00;			// own address, not relevant in master mode
	I2C_InitStruct.I2C_Ack = I2C_Ack_Enable;		// disable acknowledge when reading (can be changed later on)
	I2C_InitStruct.I2C_AcknowledgedAddress = I2C_AcknowledgedAddress_7bit; // set address length to 7 bit addresses
	I2C_Init(I2C1, &I2C_InitStruct);				// init I2C1

	// enable I2C1
	I2C_Cmd(I2C1, ENABLE);
}

static void I2C2_Init(uint32_t speed)
{
	GPIO_InitTypeDef GPIO_InitStruct;
	I2C_InitTypeDef I2C_InitStruct;

	// enable APB1 peripheral clock for I2C1
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_I2C2, ENABLE);
	// enable clock for SCL and SDA pins 
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOB, ENABLE);

	/* setup SCL and SDA pins
	* You can connect I2C1 to two different
	* pairs of pins:
	* 1. SCL on PB6 and SDA on PB7 
	* 2. SCL on PB8 and SDA on PB9
	*/
	GPIO_InitStruct.GPIO_Pin = GPIO_Pin_10 | GPIO_Pin_11; // we are going to use PB6 and PB7
	GPIO_InitStruct.GPIO_Mode = GPIO_Mode_AF;			// set pins to alternate function
	GPIO_InitStruct.GPIO_Speed = GPIO_Speed_50MHz;		// set GPIO speed
	GPIO_InitStruct.GPIO_OType = GPIO_OType_OD;			// set output to open drain --> the line has to be only pulled low, not driven high
	GPIO_InitStruct.GPIO_PuPd = GPIO_PuPd_UP;			// enable pull up resistors
	GPIO_Init(GPIOB, &GPIO_InitStruct);					// init GPIOB

	// Connect I2C1 pins to AF  
	GPIO_PinAFConfig(GPIOB, GPIO_PinSource10, GPIO_AF_I2C2);	// SCL
	GPIO_PinAFConfig(GPIOB, GPIO_PinSource11, GPIO_AF_I2C2); // SDA

	// configure I2C1 
	I2C_InitStruct.I2C_ClockSpeed = speed; 		// 100kHz
	I2C_InitStruct.I2C_Mode = I2C_Mode_I2C;			// I2C mode
	I2C_InitStruct.I2C_DutyCycle = I2C_DutyCycle_2;	// 50% duty cycle --> standard
	I2C_InitStruct.I2C_OwnAddress1 = 0x00;			// own address, not relevant in master mode
	I2C_InitStruct.I2C_Ack = I2C_Ack_Enable;		// disable acknowledge when reading (can be changed later on)
	I2C_InitStruct.I2C_AcknowledgedAddress = I2C_AcknowledgedAddress_7bit; // set address length to 7 bit addresses
	I2C_Init(I2C2, &I2C_InitStruct);				// init I2C1

	// enable I2C1
	I2C_Cmd(I2C2, ENABLE);
}


static inline bool WaitI2CBusy(I2C_TypeDef* I2Cx)
{
	for(int i=0;i<I2C_TIMEOUT;i++)
		if(!I2C_GetFlagStatus(I2Cx, I2C_FLAG_BUSY)) return true;

	return false;
}

static inline bool WaitI2CFlag(I2C_TypeDef* I2Cx, uint32_t event_flags)
{
	for(int i=0;i<I2C_TIMEOUT;i++)
		if(I2C_CheckEvent(I2Cx, event_flags)) return true;

	return false;
}

static inline bool I2C_StartTransmission(I2C_TypeDef* I2Cx)
{

	// Генерируем старт - тут все понятно )
	I2C_GenerateSTART(I2Cx, ENABLE);
	// Ждем пока взлетит нужный флаг
	return WaitI2CFlag(I2Cx, I2C_EVENT_MASTER_MODE_SELECT);
}

static inline bool I2C_SelectWriteMode(I2C_TypeDef* I2Cx, uint8_t slaveAddress)
{
	I2C_Send7bitAddress(I2Cx, slaveAddress << 1, I2C_Direction_Transmitter);
	return WaitI2CFlag(I2Cx, I2C_EVENT_MASTER_TRANSMITTER_MODE_SELECTED);
}

static inline bool I2C_SelectReadMode(I2C_TypeDef* I2Cx, uint8_t slaveAddress)
{
	I2C_Send7bitAddress(I2Cx, slaveAddress << 1, I2C_Direction_Receiver);
	return WaitI2CFlag(I2Cx, I2C_EVENT_MASTER_RECEIVER_MODE_SELECTED);
}

static inline bool I2C_WriteData(I2C_TypeDef* I2Cx, uint8_t data)
{
	I2C_SendData(I2Cx, data);
	return WaitI2CFlag(I2Cx, I2C_EVENT_MASTER_BYTE_TRANSMITTED);
}

static inline bool I2C_ReadData(I2C_TypeDef* I2Cx,  uint8_t* buffer, uint16_t length)
{
	while(length)
	{
		/* Prepare an NACK for the next data received */
		if(--length)
			I2C_AcknowledgeConfig(I2Cx, ENABLE);
		else
			I2C_AcknowledgeConfig(I2Cx, DISABLE);

		if(!WaitI2CFlag(I2Cx, I2C_EVENT_MASTER_BYTE_RECEIVED)) return false;
		*(buffer++) = I2C_ReceiveData(I2Cx);
	}

	return true;
}

static inline void I2C_StopTransmittion(I2C_TypeDef* I2Cx)
{
	I2C_GenerateSTOP(I2Cx, ENABLE);
	WaitI2CBusy(I2Cx);
}


void I2C::Init(I2C_TypeDef* I2Cx, uint32_t speed) 
{
	__I2Cx = I2Cx;

	switch ((uint32_t)__I2Cx)
	{
	case I2C1_BASE:
		I2C1_Init(speed);
		break;

	case I2C2_BASE:
		I2C2_Init(speed);
		break;

	default:
		break;
	}
}

bool I2C::Write(uint8_t data)
{
	return I2C_WriteData(__I2Cx, data);
}

bool I2C::Write(uint8_t* data, int length)
{
	while (length--)
	{
		if(!I2C_WriteData(__I2Cx,*(data++))) 
			return false;
	}
	return true;
}

bool I2C::Receive(uint8_t* buffer, uint16_t length)
{
	return I2C_ReadData(__I2Cx, buffer, length);
}

bool I2C::ReadSelect(uint8_t addr)
{
	return
		I2C_SelectReadMode(__I2Cx,  addr);
}

bool I2C::StartRead(uint8_t addr)
{
	return
		I2C_StartTransmission(__I2Cx) &&
		I2C_SelectReadMode(__I2Cx,  addr);
}

bool I2C::StartWrite(uint8_t addr)
{
	return
		I2C_StartTransmission(__I2Cx) &&
		I2C_SelectWriteMode(__I2Cx,  addr);
}

bool I2C::Stop()
{
	I2C_GenerateSTOP(__I2Cx, ENABLE);
	return true;
}

bool I2C::Transmit(uint8_t addr, uint8_t data)
{
	return StartWrite(addr) && Write(data) && Stop();
}

bool I2C::Transmit(uint8_t addr, uint8_t* data, int length)
{
	return StartWrite(addr) && Write(data, length) && Stop();
}

