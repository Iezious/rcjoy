/* Includes */
#include <stdio.h>
#include <stdarg.h>
#include <stdint.h>
#include "stm32f4xx.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_usart.h"
//#include "stm32f4_discovery.h"
//#include "stm32f4xx_conf.h"
#include <stdio.h>
//#include <rt_misc.h>
#include <stdarg.h>
#include "uart_debug.h"
#include "config.h"
#include "def.h"

#ifdef DEBUG_USART2

//USART2
#define COM_USART         USART2
#define COM_TX_PORT       GPIOA
#define COM_RX_PORT       GPIOA
#define COM_USART_CLK     RCC_APB1Periph_USART2
#define COM_TX_PORT_CLK   RCC_AHB1Periph_GPIOA
#define COM_RX_PORT_CLK   RCC_AHB1Periph_GPIOA
#define COM_TX_PIN        GPIO_Pin_2
#define COM_RX_PIN        GPIO_Pin_3
#define COM_TX_PIN_SOURCE GPIO_PinSource2
#define COM_RX_PIN_SOURCE GPIO_PinSource3
#define COM_TX_AF         GPIO_AF_USART2
#define COM_RX_AF         GPIO_AF_USART2

#endif

#ifdef DEBUG_USART3 

#define COM_USART USART3
#define COM_TX_PORT GPIOC
#define COM_RX_PORT GPIOC
#define COM_USART_CLK     RCC_APB1Periph_USART3
#define COM_TX_PORT_CLK   RCC_AHB1Periph_GPIOC
#define COM_RX_PORT_CLK   RCC_AHB1Periph_GPIOC
#define COM_TX_PIN        GPIO_Pin_10
#define COM_RX_PIN        GPIO_Pin_11
#define COM_TX_PIN_SOURCE GPIO_PinSource10
#define COM_RX_PIN_SOURCE GPIO_PinSource11
#define COM_TX_AF         GPIO_AF_USART2
#define COM_RX_AF         GPIO_AF_USART2

#endif

extern void uart_debug_init(uint32_t baudrate)
{
	  USART_InitTypeDef USART_InitStructure;
	  GPIO_InitTypeDef GPIO_InitStructure;
	  /* USARTx configured as follow:
	        - BaudRate = 230400 baud
	        - Word Length = 8 Bits
	        - One Stop Bit
	        - No parity
	        - Hardware flow control disabled (RTS and CTS signals)
	        - Receive and transmit enabled
	  */
	  USART_InitStructure.USART_BaudRate =  baudrate;
	  USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	  USART_InitStructure.USART_StopBits = USART_StopBits_1;
	  USART_InitStructure.USART_Parity = USART_Parity_No;
	  USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	  USART_InitStructure.USART_Mode = USART_Mode_Tx | USART_Mode_Rx;
	  /* Enable GPIO clock */
	  RCC_AHB1PeriphClockCmd(COM_TX_PORT_CLK | COM_RX_PORT_CLK, ENABLE);
	  /* Enable UART clock */
	  RCC_APB1PeriphClockCmd(COM_USART_CLK, ENABLE);

	  /* Connect PXx to USARTx_Tx*/
	  GPIO_PinAFConfig(COM_TX_PORT, COM_TX_PIN_SOURCE, COM_TX_AF);

	  /* Connect PXx to USARTx_Rx*/
	  GPIO_PinAFConfig(COM_RX_PORT, COM_RX_PIN_SOURCE, COM_RX_AF);

	  /* Configure USART Tx as alternate function  */
	  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	  GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;

	  GPIO_InitStructure.GPIO_Pin = COM_TX_PIN;
	  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	  GPIO_Init(COM_TX_PORT, &GPIO_InitStructure);

	  /* Configure USART Rx as alternate function  */
	  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	  GPIO_InitStructure.GPIO_Pin = COM_RX_PIN; 
	  GPIO_Init(COM_RX_PORT, &GPIO_InitStructure);

	  /* USART configuration */
	  USART_Init(COM_USART, &USART_InitStructure);

	  /* Enable USART */
	  USART_Cmd(COM_USART, ENABLE);
		
		/* Flush the USART buffer */
		//while (USART_GetFlagStatus(USART2, USART_FLAG_TC) == RESET);
		
}

/*----------------------------------------------------------------------------
  SendChar
  Write character to Serial Port.
 *----------------------------------------------------------------------------*/
#define putc(z) SendChar(z)
#define puts(z) SendString(z)

int SendChar (int ch)  {

    if (ch=='\n')  {
    
        USART_SendData(COM_USART, (uint8_t)'\r');
        while (USART_GetFlagStatus(COM_USART, USART_FLAG_TC) == RESET)
	    {}   
    } 
      
	USART_SendData(COM_USART, (uint8_t) ch);

	/* Loop until the end of transmission */
	while (USART_GetFlagStatus(COM_USART, USART_FLAG_TC) == RESET)
	{}
    
  	return (ch);
}

void SendString(const char* s)
{
	while(*s) putc(*s++);
}

/*----------------------------------------------------------------------------
  GetKey
  Read character to Serial Port.
 *----------------------------------------------------------------------------*/
int GetKey (void)  {

  int  ret;  
  /* Loop until incomming data */
  while (USART_GetFlagStatus(COM_USART, USART_FLAG_RXNE) == RESET)
  {}

  ret = USART_ReceiveData(COM_USART);

  return ret;
}




void uart_print(
	const char*	fmt,	/* Pointer to the format string */
	va_list arp			/* Pointer to arguments */
	)
{
	unsigned int r, i, j, w, f;
	unsigned long v;
	char s[16], c, d, *p;
	
	for (;;) {
		c = *fmt++;					/* Get a char */
		if (!c) break;				/* End of format? */
		if (c != '%') {				/* Pass through it if not a % sequense */
			putc(c); continue;
		}
		f = 0;
		c = *fmt++;					/* Get first char of the sequense */
		if (c == '0') {				/* Flag: '0' padded */
			f = 1; c = *fmt++;
		} else {
			if (c == '-') {			/* Flag: left justified */
				f = 2; c = *fmt++;
			}
		}
		for (w = 0; c >= '0' && c <= '9'; c = *fmt++)	/* Minimum width */
			w = w * 10 + c - '0';
		if (c == 'l' || c == 'L') {	/* Prefix: Size is long int */
			f |= 4; c = *fmt++;
		}
		if (!c) break;				/* End of format? */
		d = c;
		if (d >= 'a') d -= 0x20;
		switch (d) {				/* Type is... */
		case 'S' :					/* String */
			p = va_arg(arp, char*);
			for (j = 0; p[j]; j++) ;
			while (!(f & 2) && j++ < w) putc(' ');
			puts(p);
			while (j++ < w) putc(' ');
			continue;
		case 'C' :					/* Character */
			putc((char)va_arg(arp, int)); continue;
		case 'B' :					/* Binary */
			r = 2; break;
		case 'O' :					/* Octal */
			r = 8; break;
		case 'D' :					/* Signed decimal */
		case 'U' :					/* Unsigned decimal */
			r = 10; break;
		case 'X' :					/* Hexdecimal */
			r = 16; break;
		default:					/* Unknown type (passthrough) */
			putc(c); continue;
		}

		/* Get an argument and put it in numeral */
		v = (f & 4) ? va_arg(arp, long) : ((d == 'D') ? (long)va_arg(arp, int) : (long)va_arg(arp, unsigned int));
		if (d == 'D' && (v & 0x80000000)) {
			v = 0 - v;
			f |= 8;
		}
		i = 0;
		do {
			d = (char)(v % r); v /= r;
			if (d > 9) d += (c == 'x') ? 0x27 : 0x07;
			s[i++] = d + '0';
		} while (v && i < sizeof(s));
		if (f & 8) s[i++] = '-';
		j = i; d = (f & 1) ? '0' : ' ';
		while (!(f & 2) && j++ < w) putc(d);
		do putc(s[--i]); while(i);
		while (j++ < w) putc(' ');
	}
}

void uart_print(			/* Put a formatted string to the default device */
	const char*	fmt,	/* Pointer to the format string */
	...					/* Optional arguments */
	)
{
	va_list arp;

	va_start(arp, fmt);
	uart_print(fmt, arp);
	va_end(arp);
}


#if FALSE

struct __FILE {
  int handle;                 // Add whatever you need here 
};
FILE __stdout;
FILE __stdin;

/*----------------------------------------------------------------------------
  fputc
 *----------------------------------------------------------------------------*/
int fputc(int ch, FILE *f) {
  return (SendChar(ch));
}

/*----------------------------------------------------------------------------
  fgetc
 *----------------------------------------------------------------------------*/
int fgetc(FILE *f) {
  return (SendChar(GetKey()));
}

/*----------------------------------------------------------------------------
  _ttywrch
 *----------------------------------------------------------------------------*/
void _ttywrch(int ch) {
 SendChar (ch);
}

/*----------------------------------------------------------------------------
  ferror
 *----------------------------------------------------------------------------*/
int ferror(FILE *f) {
                              // Your implementation of ferror
  return EOF;
}
/*----------------------------------------------------------------------------
  _sys_exit
 *----------------------------------------------------------------------------*/
void _sys_exit(int return_code) {
label:  goto label;           // endless loop
}

#endif