#pragma once
#include <inttypes.h>

extern uint8_t OLED_GRAM[128][8];

extern void OLED_ClearDisplay(void);
extern void OLED_Refresh_Gram(void);
extern void OLED_DrawPoint(uint8_t X, uint8_t Y, uint8_t Fill);
extern void OLED_Fill(uint8_t x1, uint8_t y1, uint8_t x2, uint8_t y2, uint8_t dot);
extern void OLED_ShowChar(uint8_t X, uint8_t Y, uint8_t Chr, uint8_t Size, uint8_t Mode);
extern void OLED_ShowString(uint8_t X, uint8_t Y, const uint8_t *Str);
extern void OLED_Init(void);
extern void OLED_DrawString(uint8_t x, uint8_t row, char* str, uint8_t* font);
extern void OLED_DrawChar(uint8_t x, uint8_t row, char c, uint8_t* font);
