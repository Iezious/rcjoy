#pragma once

#include <stdint.h>

class PPMGenerator
{
private:

public:
	PPMGenerator(void);

	void init(uint16_t channels);
	void start();
	void stop();
	void setChannels(uint8_t channels);

	void setChan(uint16_t chan, uint16_t val);
};

extern PPMGenerator PPMGen;


#ifdef __cplusplus 
extern "C"
{
#endif

	extern void TIM3_IRQHandler();

#ifdef __cplusplus 
}
#endif