#ifndef __PWM_TIMER_READER_H__
#define __PWM_TIMER_READER_H__

#include "inttypes.h"


#ifdef __cplusplus
extern "C" {
#endif

void InitPWMReader();
void StartPWMReader();
void CalcChans();


#ifdef __cplusplus
}
#endif 

extern void TIM2_IRQHandler();
extern void TIM3_IRQHandler();
extern void TIM4_IRQHandler();

#endif