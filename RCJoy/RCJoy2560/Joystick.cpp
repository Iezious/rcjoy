#include "Joystick.h" 
#include "Config.h" 
#include "Def.h" 

JoyData JoyInput;

void ParseJoyData(const JoyUSBData *report)
{
	 JoyInput.AX_ALIRONS = report->AX_ALIRONS;
	 JoyInput.AX_ELEVATOR = report->AX_ELEVATOR;
	 JoyInput.AX_RUDDER = report->AX_RUDDER;
	 JoyInput.AX_THROTTLE = report->AX_THROTTLE;
	 JoyInput.AX_FLAPS = report->AX_FLAPS;
	 JoyInput.BTN_BUTTON_Change = JoyInput.BTN_BUTTON ^ report->BTN_BUTTON;
	 JoyInput.BTN_BUTTON = report->BTN_BUTTON;
	 JoyInput.HAT_MAINHAT_Change = report->HAT_MAINHAT != JoyInput.HAT_MAINHAT;
	 JoyInput.HAT_MAINHAT = report->HAT_MAINHAT;
}
