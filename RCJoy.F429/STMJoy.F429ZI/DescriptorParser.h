#pragma once

#include <inttypes.h>

#define DATA_SIZE_MASK 0x03
#define TYPE_MASK 0x0C
#define TAG_MASK 0xF0

#define DATA_SIZE_0 0x00
#define DATA_SIZE_1 0x01
#define DATA_SIZE_2 0x02
#define DATA_SIZE_4 0x03

#define TYPE_MAIN 0x00
#define TYPE_GLOBAL 0x04
#define TYPE_LOCAL 0x08

#define TAG_MAIN_INPUT 0x80
#define TAG_MAIN_OUTPUT 0x90
#define TAG_MAIN_COLLECTION 0xA0
#define TAG_MAIN_FEATURE 0xB0
#define TAG_MAIN_ENDCOLLECTION 0xC0

#define TAG_GLOBAL_USAGEPAGE 0x00
#define TAG_GLOBAL_LOGICALMIN 0x10
#define TAG_GLOBAL_LOGICALMAX 0x20
#define TAG_GLOBAL_PHYSMIN 0x30
#define TAG_GLOBAL_PHYSMAX 0x40
#define TAG_GLOBAL_UNITEXP 0x50
#define TAG_GLOBAL_UNIT 0x60
#define TAG_GLOBAL_REPORTSIZE 0x70
#define TAG_GLOBAL_REPORTID 0x80
#define TAG_GLOBAL_REPORTCOUNT 0x90
#define TAG_GLOBAL_PUSH 0xA0
#define TAG_GLOBAL_POP 0xB0

#define TAG_LOCAL_USAGE 0x00
#define TAG_LOCAL_USAGEMIN 0x10
#define TAG_LOCAL_USAGEMAX 0x20

extern "C" void ParseReportDescriptor(uint8_t *report, uint16_t len, uint16_t* bitlen, uint8_t *mreport);