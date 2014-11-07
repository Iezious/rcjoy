/**
  ******************************************************************************
  * @file    stm32f4_discovery.h
  * @author  MCD Application Team
  * @version V1.1.0
  * @date    28-October-2011
  * @brief   This file contains definitions for STM32F4-Discovery Kit's Leds and 
  *          push-button hardware resources.
  ******************************************************************************
  * @attention
  *
  * THE PRESENT FIRMWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
  * WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE
  * TIME. AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY
  * DIRECT, INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING
  * FROM THE CONTENT OF SUCH FIRMWARE AND/OR THE USE MADE BY CUSTOMERS OF THE
  * CODING INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
  *
  * <h2><center>&copy; COPYRIGHT 2011 STMicroelectronics</center></h2>
  ******************************************************************************  
  */ 
  


/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __STM32F4_DISCOVERY_H
#define __STM32F4_DISCOVERY_H

#include "conf.h"

#ifdef __cplusplus
 extern "C" {
#endif
                                              
/* Includes ------------------------------------------------------------------*/
 #include "stm32f4xx.h"
   
#define LEDn                             4

#ifdef STMF4DISC

#define LED4_PIN                         GPIO_Pin_12
#define LED4_GPIO_PORT                   GPIOD
#define LED4_GPIO_CLK                    RCC_AHB1Periph_GPIOD  
  
#define LED3_PIN                         GPIO_Pin_13
#define LED3_GPIO_PORT                   GPIOD
#define LED3_GPIO_CLK                    RCC_AHB1Periph_GPIOD  
  
#define LED5_PIN                         GPIO_Pin_14
#define LED5_GPIO_PORT                   GPIOD
#define LED5_GPIO_CLK                    RCC_AHB1Periph_GPIOD  
  
#define LED6_PIN                         GPIO_Pin_15
#define LED6_GPIO_PORT                   GPIOD
#define LED6_GPIO_CLK                    RCC_AHB1Periph_GPIOD

#endif 

#ifdef PORT407


#endif
/**
  * @}
  */ 
  
void init_leds();


#ifdef PORT407
#define LED1OFF GPIOE->BSRRH = GPIO_Pin_7
#define LED1ON GPIOE->BSRRL = GPIO_Pin_7

#define LED2OFF GPIOE->BSRRH = GPIO_Pin_8
#define LED2ON GPIOE->BSRRL = GPIO_Pin_8

#define LED3OFF GPIOE->BSRRH = GPIO_Pin_9
#define LED3ON GPIOE->BSRRL = GPIO_Pin_9

#define LED4OFF GPIOE->BSRRH = GPIO_Pin_10
#define LED4ON GPIOE->BSRRL = GPIO_Pin_10

/*
#define LED1OFF
#define LED1ON 

#define LED2OFF 
#define LED2ON 

#define LED3OFF 
#define LED3ON 

#define LED4OFF 
#define LED4ON 
*/



#else

#define LED1OFF GPIOD->BSRRH = GPIO_Pin_12
#define LED1ON GPIOD->BSRRL = GPIO_Pin_12

#define LED2OFF GPIOD->BSRRH = GPIO_Pin_13
#define LED2ON GPIOD->BSRRL = GPIO_Pin_13

#define LED3OFF GPIOD->BSRRH = GPIO_Pin_14
#define LED3ON GPIOD->BSRRL = GPIO_Pin_14

#define LED4OFF GPIOD->BSRRH = GPIO_Pin_15
#define LED4ON GPIOD->BSRRL = GPIO_Pin_15

#endif

#ifdef __cplusplus
}
#endif

#endif /* __STM32F4_DISCOVERY_H */
/**
  * @}
  */ 

/**
  * @}
  */ 

/**
  * @}
  */

 

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/

