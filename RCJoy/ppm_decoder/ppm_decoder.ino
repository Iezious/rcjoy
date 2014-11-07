#include <arduino.h>

#define TIMESCALE 2

void setup()
{
	// Output pins as software pwm
	DDRB |= (1 << PIN2) | (1 << PIN3) | (1 << PIN4) | (1 << PIN5) | (1 << PIN1) | (1 << PIN0);
	PORTB &= ~((1 << PIN2) | (1 << PIN3) | (1 << PIN4) | (1 << PIN5) | (1 << PIN1) | (1 << PIN0));

	DDRC |= (1 << PIN0) | (1 << PIN1) | (1 << PIN2) | (1 << PIN3);
	PORTC &= ~((1 << PIN0) | (1 << PIN1) | (1 << PIN2) | (1 << PIN3));

	DDRD |= (1 << PIN6) | (1 << PIN7);
	PORTD &= ~((1 << PIN6) | (1 << PIN7));


	// input pin for EXT0 with pull up
	pinMode(2, INPUT_PULLUP);
//	PORTD |= (1 << PIN2);
//	DDRD &= ~(1 << PIN2);


/*
	EICRA |= (1 << ISC01);  // interrupt at faling edge of Ext0
	EIMSK |= (1 << INT0);   // Enabling invterrupt
*/
	TCCR1A = 0;             // TIMER2 for time counting
	TCCR1B = (1 << CS11);   // presacle 8
	TCNT1 = 0;
}

/***********************************************************************************************
/* time measurement part
/* */

volatile uint16_t tcount = 0;

/***********************************************************************************************
/* software pin switch for pwm channels
/* */

void setCh(uint8_t ch, uint8_t v)
{
	switch (ch)
	{
		case 0:
			PORTB = v ? PORTB | (1 << PIN2) : PORTB & ~(1 << PIN2);
			break;
		
		case 1:
			PORTB = v ? PORTB | (1 << PIN3) : PORTB & ~(1 << PIN3);
			break;

		case 2:
			PORTB = v ? PORTB | (1 << PIN4) : PORTB & ~(1 << PIN4);
			break;

		case 3:
			PORTB = v ? PORTB | (1 << PIN5) : PORTB & ~(1 << PIN5);
			break;

		case 4:
			PORTC = v ? PORTC | (1 << PIN0) : PORTC & ~(1 << PIN0);
			break;

		case 5:
			PORTC = v ? PORTC | (1 << PIN1) : PORTC & ~(1 << PIN1);
			break;

		case 6:
			PORTC = v ? PORTC | (1 << PIN2) : PORTC & ~(1 << PIN2);
			break;

		case 7:
			PORTC = v ? PORTC | (1 << PIN3) : PORTC & ~(1 << PIN3);
			break;

		case 8:
			PORTB = v ? PORTB | (1 << PIN1) : PORTB & ~(1 << PIN1);
			break;

		case 9:
			PORTB = v ? PORTB | (1 << PIN0) : PORTB & ~(1 << PIN0);
			break;

		case 10:
			PORTD = v ? PORTD | (1 << PIN7) : PORTD & ~(1 << PIN7);
			break;

		case 11:
			PORTD = v ? PORTD | (1 << PIN6) : PORTD & ~(1 << PIN6);
			break;


	}
}

//volatile uint32_t prev_count = 0;

/*
ISR(INT0_vect)
{
	uint32_t time = NOW;
	uint16_t diff = (time - prev_count) & 0xFFFF;
	prev_count = time;

	if (diff > 2500 * TIMESCALE)
	{
		inSync = 1;
		if (ch > 0) setCh(ch, 0);
		ch = 0;
		setCh(ch, 1);
	}
	else if (inSync)
	{
		setCh(ch, 0);
		ch++;
		setCh(ch, 1);
	}
}
*/

#define INPORT PIND
#define CHECK_MASK 0x4 // ( 1 << 2)

void loop()
{
	uint8_t inSync = 0;
	int8_t ch = -1;

	for (;;)
	{
		uint16_t time_in = TCNT1;

		while (digitalRead(2) == HIGH) __asm__("nop\n\t");
		while (digitalRead(2) == LOW) __asm__("nop\n\t");
//		while (INPORT & CHECK_MASK) __asm__("nop\n\t");
//		while (INPORT & CHECK_MASK == 0) __asm__("nop\n\t");

		uint16_t time_out = TCNT1;

		if (time_out - time_in > 5000)
		{
			inSync = 1;
			if (ch > 0) setCh(ch, 0);
			ch = 0;
			setCh(ch, 1);
		}
		else if (inSync)
		{
			setCh(ch, 0);
			ch++;
			setCh(ch, 1);
		}
	}
}
