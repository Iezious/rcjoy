
#include <arduino.h>

#define TIMER_SCALE 2 

// Dead-time between each channel in the PPM-stream. 
#define DEAD_TIME 300 

// Number of PPM channels out. 1 - 12 channels supported (both incl). 
#define NUMBER_OF_CHANNELS 8
#define TRESHHOLD 100

// Set frame-length depending on channels
#define FRAME_TOTAL_LENGTH 22500 * TIMER_SCALE
#define FILTER_DEPTH 7

volatile uint32_t frame_spent = 0;
volatile uint8_t channel_number;
volatile uint8_t ondelay = 0;
volatile uint8_t onsync = 0;

uint32_t timer_switches[8] = { 0, 0, 0, 0, 0, 0, 0, 0 };
uint16_t values[8] = { 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE, 1200 * TIMER_SCALE };
uint16_t filter_array[FILTER_DEPTH][8];
uint8_t filterIdx[FILTER_DEPTH];

volatile uint16_t tcount = 0;

void setup() 
{
  Serial.begin(115200);

  for (byte i = 0; i < FILTER_DEPTH; i++)
  {
	  filterIdx[i] = 0;
	  for (byte j = 0; j < 8; j++) filter_array[i][j] = 2400;
  }
  

  
  #define PINS_SETUP_D ((1<<2) | (1<<4) | (1<<5) | (1<<6) | (1<<7));
  DDRD &= ~PINS_SETUP_D;
  PCMSK2 |= PINS_SETUP_D;
  PORTD = PINS_SETUP_D;
  PCICR |= (1<<PCIE2); 
  

  #define PINS_SETUP_B ((1 << 0) | (1 << 3) | (1<<4))
  DDRB &= ~PINS_SETUP_B;
  PCMSK0 |= PINS_SETUP_B;
  PORTB = PINS_SETUP_B;
  PCICR |= (1<<PCIE0); 

  TCCR1A = 
   (0<<WGM10) |
   (0<<WGM11) |
   (0<<COM1A1) |
   (1<<COM1A0) | // Toggle pin om compare-match
   (0<<COM1B1) |
   (0<<COM1B0);  
  
  TCCR1B =
    (0<<ICNC1)| // 
    (0<<ICES1)| //     
    (0<<CS10) | //Prescale 8  
    (1<<CS11) | //Prescale 8  
    (0<<CS12) | //Prescale 8
    (0<<WGM13)|    
    (1<<WGM12); // CTC mode (Clear timer on compare match) with OCR1A as top.       
    
  TIMSK1 = 
    (1<<OCIE1A) | // Interrupt on compare A
    (0<<OCIE1B) | // Disable interrupt on compare B    
    (0<<TOIE1);       
    
  DDRB |= (( 1 << 1 ) | ( 1 << 2));
  onsync = 1;
  OCR1A = FRAME_TOTAL_LENGTH;   
  
  TCCR2A = 0;
  TCCR2B = (1<<CS21);  // presacle 8
  TCNT2 = 0;
  
  TIMSK2 = (1 << TOIE2);

  sei();
}



void loop() 
{

	Serial.print(values[0]);
	Serial.print(",");
	Serial.print(values[1]);
	Serial.print(",");
	Serial.print(values[2]);
	Serial.print(",");
	Serial.print(values[3]);
	Serial.print(",");
	Serial.print(values[4]);
	Serial.print(",");
	Serial.print(values[5]);
	Serial.print(",");
	Serial.print(values[6]);
	Serial.print(",");
	Serial.println(values[7]);
	Serial.print(",");

	Serial.println(tcount);
	
/*
	Serial.print(timer_switches[0]);
	Serial.print(",");
	Serial.print(timer_switches[1]);
	Serial.print(",");
	Serial.print(timer_switches[2]);
	Serial.print(",");
	Serial.print(timer_switches[3]);
	Serial.print(",");
	Serial.print(timer_switches[4]);
	Serial.print(",");
	Serial.print(timer_switches[5]);
	Serial.print(",");
	Serial.print(timer_switches[6]);
	Serial.print(",");
	Serial.println(timer_switches[7]);
*/

	delay(500);
}


// ============================================
// Read PWM
// ============================================



#define DIFF_OVFL(v1, v2) (v1 - v2) & 0xFFFF

#define TIME_DIFF(idx) (timer_switches[idx] == 0 ? 1500 * TIMER_SCALE : DIFF_OVFL (time, timer_switches[idx]))

#define NOW (((uint32_t)tcount) << 8) | TCNT2

#define checkPort(pin, chan) checkPortPins(pin, chan, &state, pins, time)

inline uint16_t CHECKVAL(uint32_t val, uint32_t prev)
{
	return (val >850 * TIMER_SCALE && val <2200 * TIMER_SCALE)
		? val //(abs(val - prev) > TRESHHOLD ? val : prev)
		: prev;
}

inline void checkPortPins(uint8_t pin, uint8_t chan, uint8_t *state, uint8_t pins, uint32_t time)
{
	uint8_t diff = pins ^ *state;

	uint8_t mask = 1 << pin;
	if (mask & diff)
	{
		if (pins & mask)
		{
			timer_switches[chan] = time;
			*state |= mask;
		}
		else
		{
			//values[chan] = 

			filter_array[filterIdx[chan]][chan] = CHECKVAL(TIME_DIFF(chan), values[chan]);
			filterIdx[chan] = filterIdx[chan] + 1; 
			if (filterIdx[chan] == FILTER_DEPTH) filterIdx[chan] = 0;

			*state &= ~mask;
		}
	}
}
	
ISR(PCINT0_vect)
{
	static byte state = 0;

	byte mask;

	byte pins = PINB;
	
  	uint32_t time = NOW;
        
	checkPort(0,5);
	checkPort(3,6);
	checkPort(4,7);
}

ISR(PCINT2_vect) 
{
	static byte state = 0;

	byte mask;

	byte pins = PIND;
        
	uint32_t time = NOW;
        
	checkPort(2,0);
	checkPort(4,1);
	checkPort(5,2);
	checkPort(6,3);
	checkPort(7,4);
}

ISR(TIMER2_OVF_vect)
{
	tcount++;
}

// ========================================
// Generate PPM
// ========================================

inline uint16_t getChannel(uint8_t ch, int16_t vv)
{
	int32_t v = 0;
	for (uint8_t i = 0; i < FILTER_DEPTH; i++) v += filter_array[i][ch];
	v = v / FILTER_DEPTH;

	return abs(v - vv) > TRESHHOLD ? v : vv;
}

ISR(TIMER1_COMPA_vect) 
{
  int wait_next;
  
  if (onsync) 
  {
      TCCR1A = 
     (0<<WGM10) |
     (0<<WGM11) | 
     (1<<COM1A1) |
     (1<<COM1A0) |
     (0<<COM1B1) |
     (0<<COM1B0);   
  
    channel_number = 0;
    onsync = 0;
    ondelay = 1;
    
    frame_spent = DEAD_TIME;
    OCR1A = DEAD_TIME;
  }
  else 
  {
      if (channel_number == 0) 
      {
            // After first time, when pin have been set hgih, we toggle the pin at each interrupt
              TCCR1A = 
             (0<<WGM10) |
             (0<<WGM11) |
             (0<<COM1A1) |
             (1<<COM1A0) |
             (0<<COM1B1) |
             (0<<COM1B0);   
       }
          
       if(channel_number < NUMBER_OF_CHANNELS)
       {
           if(ondelay)
           {
               ondelay = 0;
//               wait_next = values[channel_number] * TIMER_SCALE - DEAD_TIME;
			   wait_next = values[channel_number] = getChannel(channel_number, values[channel_number]);// values[channel_number] - DEAD_TIME;
               OCR1A = wait_next;
               frame_spent += wait_next;
               channel_number++;
           }  
           else
           {
               ondelay = 1;  
               frame_spent += DEAD_TIME;
               OCR1A = DEAD_TIME;
           }
       }
       else if(!ondelay)
       {
           ondelay = 1;  
           frame_spent += DEAD_TIME;
           OCR1A = DEAD_TIME; 
       }
       else 
       {
           onsync = 1;
           OCR1A = FRAME_TOTAL_LENGTH - frame_spent; //FRAME_LENGTH-5;
       }
  }
}
