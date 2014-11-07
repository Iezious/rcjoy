
#include <stdint.h>
#include "conf.h"
#include "def.h"

#define extract_u16 *(uint16_t *)(*datapointer); *datapointer += 2;
#define extract_s16 *(int16_t *)(*datapointer); *datapointer += 2;

typedef bool (*calcFunc)(uint32_t *datapointer);

extern axis_t ExtractJoysticBits(uint16_t start, uint8_t length);
extern calcFunc FunctionsMap[];