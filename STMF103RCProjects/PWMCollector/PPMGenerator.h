#ifndef __PPMGENERATOR_H__
#define __PPMGENERATOR_H__

#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>
#include <stm32f10x_exti.h>
#include <misc.h>

#ifdef __cplusplus
extern "C" {
#endif

extern void initPPMGenerator(uint16_t chans);
extern void setPPMGeneratorChan(uint16_t chan, uint32_t val);
extern void startPPMGenerator();
extern void stopPPMGenerator();
extern void setPPMGeneratorChannelCount(uint8_t channels);


#ifdef __cplusplus
}
#endif

extern void TIM3_IRQHandler();

#endif