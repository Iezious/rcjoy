#ifndef _JOYSTICK_H_
#define _JOYSTICK_H_

#define RPT_JOY_LEN 8

#include <inttypes.h>
#include "functions.h"
#include "Config.h" 
#include "Def.h" 

struct JoyUSBData
{
	 union {
		 uint8_t raw_data[RPT_JOY_LEN];
		 struct {
			 uint16_t AX_ALIRONS : 10;
			 uint16_t AX_ELEVATOR : 10;
			 uint16_t AX_RUDDER : 8;
			 uint16_t AX_THROTTLE : 8;
			 uint16_t AX_FLAPS : 8;
			 uint16_t BTN_BUTTON : 14;
			 uint8_t HAT_MAINHAT : 4;
			 uint8_t DMMY_0 : 2;
		 };
	 };
};

struct JoyData
{
	 uint16_t AX_ALIRONS;
	 uint16_t AX_ELEVATOR;
	 uint16_t AX_RUDDER;
	 uint16_t AX_THROTTLE;
	 uint16_t AX_FLAPS;
	 uint16_t BTN_BUTTON;
	 uint16_t BTN_BUTTON_Change;
	 uint8_t HAT_MAINHAT;
	 uint8_t HAT_MAINHAT_Change;
};

void ParseJoyData(const JoyUSBData *report);
extern JoyData JoyInput;

#endif
