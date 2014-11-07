// ppmGen.h

#ifndef _PPMGEN_h
#define _PPMGEN_h

#include <Arduino.h>
#include "config.h"
#include "def.h"

#ifdef PPM_GENERATOR

extern uint16_t NUMBER_OF_CHANNELS;
extern uint32_t FRAME_TOTAL_LENGTH;
extern uint16_t PPM_DATA[PPM_CHANNELS];

void PPMSetup();

#endif 

#endif

