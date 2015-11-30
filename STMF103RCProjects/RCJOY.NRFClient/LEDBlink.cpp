#include <stm32f10x_gpio.h>
#include <stm32f10x_rcc.h>

void Delay()
{
	int i;
	for (i = 0; i < 1000000; i++)
		asm("nop");
}

int main()
{
  GPIO_InitTypeDef GPIO_InitStructure;
  
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC, ENABLE);

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;
  
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_Init(GPIOC, &GPIO_InitStructure);

  for (;;)
  {
	  GPIO_WriteBit(GPIOC, GPIO_Pin_6, Bit_SET);
	  Delay();
	  GPIO_WriteBit(GPIOC, GPIO_Pin_6, Bit_RESET);
	  Delay();
  }
}
