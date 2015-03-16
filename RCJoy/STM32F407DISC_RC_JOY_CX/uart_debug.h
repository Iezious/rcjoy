/**
  ******************************************************************************
  * @file    uart_debug.h
  * @author  Yuuichi Akagawa
  * @version V1.0.0
  * @date    2012/02/27
  * @brief   This file contains all the prototypes for the uart_debug.c
  ******************************************************************************
  * @attention
  *
  * Licensed under the Apache License, Version 2.0 (the "License");
  * you may not use this file except in compliance with the License.
  * You may obtain a copy of the License at
  *
  *      http://www.apache.org/licenses/LICENSE-2.0
  *
  * Unless required by applicable law or agreed to in writing, software
  * distributed under the License is distributed on an "AS IS" BASIS,
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  * See the License for the specific language governing permissions and
  * limitations under the License.
  * <h2><center>&copy; COPYRIGHT (C)2012 Yuuichi Akagawa</center></h2>
  *
  ******************************************************************************
  */

#include <stdint.h>

void uart_debug_init(uint32_t baudrate);
#ifdef __cplusplus
extern "C" void uart_print(const char* s, ...);
#else
void uart_print(const char* s, ...);
#endif
	

#ifdef 	DEBUG
#define	DBG(...)  uart_print(__VA_ARGS__)
#else	  
#define	DBG(...)
#endif

#define	INFO(...)  uart_print(__VA_ARGS__)
#define	ERR(...)  uart_print(__VA_ARGS__)

void LogicAnalyzerTirggerConfig(void);
void LogicAnalyzerTirgger(uint8_t t);
