#ifndef __PWMREADER_H__
#define __PWMREADER_H__

#include "inttypes.h"

#define LOG_LENGTH 100
#define CHANNELS_COUNT 8

#ifdef __cplusplus
extern "C" {
#endif

void initCapture();
void startCapture();
void ReadPWM();

#ifdef __cplusplus
}
#endif

extern void EXTI0_IRQHandler();
extern void EXTI1_IRQHandler();
extern void EXTI2_IRQHandler();
extern void EXTI3_IRQHandler();
extern void EXTI4_IRQHandler();
extern void EXTI9_5_IRQHandler();

#endif