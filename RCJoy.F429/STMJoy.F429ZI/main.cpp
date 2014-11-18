#include <stm32f4xx_hal.h>


#ifdef __cplusplus
extern "C" {
#endif

#include "stm32f4xx.h"
#include "inttypes.h"
#include "stm32f4xx_hal_rcc.h"
#include "stm32f429i_discovery.h"


#include "usb_host.h"
//#include "usbh_hid.h"

#include "gpio.h"
#include "fonts.h"
#include "font12.h"
#include "font20.h"
#include "font24.h"
#include "fontPkts.h"
#include "Time.h"

#ifdef __cplusplus
	}
#endif

#include "PPMGenerator.h"
#include "Calculator.h"
#include "CalcMap.h"
#include "EEPRom.h"
#include "ComCom.h"
#include "GUI.h"
#include "GUIInit.h"
#include "GUIModeFlight.h"
#include "GUIModelListMode.h"
#include "GUIModeVars.h"
#include "ComProg.h"

void SystemClock_Config(void)
{

	RCC_ClkInitTypeDef RCC_ClkInitStruct;
	RCC_PeriphCLKInitTypeDef PeriphClkInitStruct;
	RCC_OscInitTypeDef RCC_OscInitStruct;

	__PWR_CLK_ENABLE();

	__HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);

	RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
	RCC_OscInitStruct.HSEState = RCC_HSE_ON;
	RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
	RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
	RCC_OscInitStruct.PLL.PLLM = 8;
	RCC_OscInitStruct.PLL.PLLN = 336;
	RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
	RCC_OscInitStruct.PLL.PLLQ = 7;
	HAL_RCC_OscConfig(&RCC_OscInitStruct);

	RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_SYSCLK | RCC_CLOCKTYPE_PCLK1
		| RCC_CLOCKTYPE_PCLK2;
	RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
	RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
	RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
	RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;
	HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5);

	PeriphClkInitStruct.PeriphClockSelection = RCC_PERIPHCLK_LTDC;
	PeriphClkInitStruct.PLLSAI.PLLSAIN = 49;
	PeriphClkInitStruct.PLLSAI.PLLSAIR = 2;
	PeriphClkInitStruct.PLLSAIDivR = RCC_PLLSAIDIVR_2;
	HAL_RCCEx_PeriphCLKConfig(&PeriphClkInitStruct);

}

ComCom COMCOM;
EEPRom EEPROM;
PPMGenerator PPMGen;
bool code_enabled; 

#define MODES_CONT 3
GUIModeDef *GUIModes[MODES_CONT] = { &ModeFlight, &ModelSelectMode, &ModeVariables };


void ComComEcho(uint8_t* b, uint32_t l, uint8_t **ab, uint32_t *al)
{
	*al = l;
	*ab = b;
}


int main(void)
{
	HAL_Init();

	/* Configure the system clock */
	SystemClock_Config();

	MX_GPIO_Init();
//	MX_DMA_Init();
//	MX_DMA2D_Init();
//	MX_I2C3_Init();
//	MX_LTDC_Init();
//	MX_SPI5_Init();

//	MX_USART1_UART_Init();
//	MX_WWDG_Init();
	HAL_Delay(100);
	code_enabled = (BSP_PB_GetState(BUTTON_KEY) == RESET);

	MX_USB_HOST_Init();

	HAL_NVIC_SetPriorityGrouping(NVIC_PRIORITYGROUP_0);
	HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);

	BSP_LED_Init(LED3);
	BSP_LED_Init(LED4);

	COMCOM.Start(115200);
	COMCOM.RegisterCommand(0x4, &ComComEcho);
	
	EEPROM.Init();

	PPMGen.init(8);
	PPMGen.start();

	GUIRoot.Init(GUIModes, MODES_CONT);
	GUIRoot.Draw();

	if (!PROGRAM_PRESENT)
	{
		SetModelName("No settings");
		//SetModelMode("Normal");
	} else if (!code_enabled)
	{
		SetModelName("Disabled mode");
	}

	ApplicationTypeDef pre_state = APPLICATION_DISCONNECT;

	bool ledstate = false;
	uint32_t ledswitch = 0;

	InitComProgrammer();
	InitCalc();

	if(code_enabled)
		ExecuteStartup();

	uint32_t recalc_time = 0;
	uint32_t gui_time = 0;
	uint32_t ping_time = 0;

	for (;;)
	{
		if (USB_Poll_Time < 5)
			HAL_Delay(4);
		else
			HAL_Delay(USB_Poll_Time-2);

		MX_USB_HOST_Process();

//		if (HAL_GetTick() - ping_time > 10)
//		{
			COMCOM.Ping();
			EEPROM.Ping();

//			ping_time = HAL_GetTick();
//		}

		if (code_enabled && HAL_GetTick() - recalc_time > 10)
		{
			ExecuteCommon();
			ExecuteModel();

			recalc_time = HAL_GetTick();
		}

		if (HAL_GetTick() - gui_time > 100)
		{
			if (pre_state != Appli_state)
			{
				if (Appli_state == APPLICATION_READY)
					GUIRoot.SetUSBConnected(true);
				else
					GUIRoot.SetUSBConnected(false);

				pre_state = Appli_state;
			}

			GUIRoot.SetNotSaved(EEPROM.GetNotSaved(), EEPROM.GetError());
			GUIRoot.Tick();
		}

		if (HAL_GetTick() - ledswitch > 500)
		{
			ledswitch = HAL_GetTick();
			ledstate = !ledstate;

			if (ledstate)
				BSP_LED_On(LED3);
			else
				BSP_LED_Off(LED3);
		}
	}
}
