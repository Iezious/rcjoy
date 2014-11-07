#include "ppmGen.h"

#ifdef PPM_GENERATOR


uint16_t NUMBER_OF_CHANNELS = 0;
uint32_t FRAME_TOTAL_LENGTH = 22500 * TIMER_SCALE;
uint16_t PPM_DATA[PPM_CHANNELS];

uint32_t  frame_spent = 0;
uint16_t  channel_number;
byte ondelay = 0;
byte onsync = 0;

void PPMSetup()
{
	for(uint8_t i=0; i<PPM_CHANNELS;i++) *(PPM_DATA + i) = 1520;

	PPM_TIMER_INITA;
	PPM_TIMER_INITB;
	PPM_TIMER_INITMSK;
	PPM_PORT_SETUP;
	
	onsync = 1;
	PPM_TIMER_OCR = DEAD_TIME;   
}


ISR(PPM_TIMER_VECTOR) 
{
	int wait_next;

	if (onsync) 
	{
		PPM_TIMER_FRAMESTART;

		channel_number = 0;
		onsync = 0;
		ondelay = 1;

		frame_spent = DEAD_TIME;
		PPM_TIMER_OCR = DEAD_TIME;
	}
	else 
	{
		if (channel_number == 0) 
		{
			PPM_TIMER_FRAMESECONDARY;
		}
		
		if(channel_number < NUMBER_OF_CHANNELS)
		{
			if(ondelay)
			{
				ondelay = 0;
				wait_next = PPM_DATA[channel_number] * TIMER_SCALE - DEAD_TIME;
				PPM_TIMER_OCR = wait_next;
				frame_spent += wait_next;
				channel_number++;
			}  
			else
			{
				ondelay = 1;  
				frame_spent += DEAD_TIME;
				PPM_TIMER_OCR = DEAD_TIME;
			}
		}
		else if(!ondelay)
		{
			ondelay = 1;  
			frame_spent += DEAD_TIME;
			PPM_TIMER_OCR = DEAD_TIME; 
		}
		else 
		{
			onsync = 1;
			PPM_TIMER_OCR = FRAME_TOTAL_LENGTH - frame_spent; //FRAME_LENGTH-5;
		}
	}
}

#endif
