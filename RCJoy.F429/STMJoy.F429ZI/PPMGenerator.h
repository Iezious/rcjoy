#pragma once

#include <stdint.h>
#include "stm32f4xx_hal.h"
#include <inttypes.h>


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
	uint16_t getChannel(uint8_t idx);
};



#ifdef __cplusplus 
extern "C"
{
#endif
	extern PPMGenerator PPMGen;

	extern TIM_HandleTypeDef htim3;

	extern void HAL_TIM_Base_MspInit(TIM_HandleTypeDef* htim_base);
	extern void HAL_TIM_Base_MspDeInit(TIM_HandleTypeDef* htim_base);
	extern void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim);

	extern void TIM3_IRQHandler();

#ifdef __cplusplus 
}
#endif