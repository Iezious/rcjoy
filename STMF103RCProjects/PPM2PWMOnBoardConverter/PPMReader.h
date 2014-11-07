#pragma once

#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>

#define MAX_CHANNELS 12

extern uint16_t PPMChannels[MAX_CHANNELS];

void StartPPMReader();