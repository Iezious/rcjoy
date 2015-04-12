#include <stm32f10x.h>
#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>
#include <stm32f10x_tim.h>
#include <stm32f10x_exti.h>

#include "SysTimer.h"
#include "PPMGenerator.h"
#include "SSD1306.h"
#include "font5x7.h"


int main()
{
	StartSysTimer();
	delay(100);
	initPPMGenerator(8);

	startPPMGenerator();

	OLED_Init();
	OLED_ClearDisplay();
	OLED_Fill(0, 0, 15, 15, 1);
	OLED_Fill(16, 16, 31, 31, 1);
	OLED_DrawString(10, 4, "Ab cd", font5x7);

	OLED_Refresh_Gram();

	for (;;)
	{
	}
}
