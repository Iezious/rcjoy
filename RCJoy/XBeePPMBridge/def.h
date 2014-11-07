#ifndef __DEF_H__
#define __DEF_H__

#include "config.h"

#define TIMER_SCALE 2
#define DEAD_TIME 300
#define FRAME_SYNC_LENGTH 4500
#define PPM_CHAN_LENGTH 2000

#define AXE_MIN 0
#define AXE_MIDDLE 500
#define AXE_MAX 1000


#ifdef PPM_TIMER_1A

#ifdef MEGA2560

#define PPM_TIMER_OCR OCR1A
#define PPM_TIMER_VECTOR TIMER1_COMPA_vect
#define PPM_TIMER_INITA	TCCR1A = (0<<WGM10) | (0<<WGM11) | (0<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0)
#define PPM_TIMER_INITB	TCCR1B = (0<<ICNC1) | (0<<ICES1)| (0<<CS10) | (1<<CS11) | (0<<CS12) | (0<<WGM13)| (1<<WGM12)
#define PPM_TIMER_INITMSK TIMSK1 = (1<<OCIE1A) | (0<<OCIE1B) | (0<<TOIE1)
#define PPM_PORT_SETUP DDRB |= (1 << DDB5)

#define PPM_TIMER_FRAMESTART TCCR1A = (0<<WGM10) | (0<<WGM11) | (1<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0)
#define PPM_TIMER_FRAMESECONDARY TCCR1A = (0<<WGM10) | (0<<WGM11) | (0<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0); 

#endif

#ifdef MEGA238

#define PPM_TIMER_OCR OCR1A
#define PPM_TIMER_VECTOR TIMER1_COMPA_vect
#define PPM_TIMER_INITA	TCCR1A = (0<<WGM10) | (0<<WGM11) | (0<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0)
#define PPM_TIMER_INITB	TCCR1B = (0<<ICNC1) | (0<<ICES1)| (0<<CS10) | (1<<CS11) | (0<<CS12) | (0<<WGM13)| (1<<WGM12)
#define PPM_TIMER_INITMSK TIMSK1 = (1<<OCIE1A) | (0<<OCIE1B) | (0<<TOIE1)
#define PPM_PORT_SETUP DDRB |= (( 1 << 1 ) | ( 1 << 2));

#define PPM_TIMER_FRAMESTART TCCR1A = (0<<WGM10) | (0<<WGM11) | (1<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0)
#define PPM_TIMER_FRAMESECONDARY TCCR1A = (0<<WGM10) | (0<<WGM11) | (0<<COM1A1) | (1<<COM1A0) | (0<<COM1B1) | (0<<COM1B0); 

#endif

#endif
#endif