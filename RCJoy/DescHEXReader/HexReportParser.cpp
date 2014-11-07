
#include "HexReportParser.h"

void HexReportParser::Parse(const uint16_t len, const uint8_t *pbuf, const uint16_t &offset)
{
	uint16_t i;

	for(i=0;i<len;i++)
	{
		PrintHex<uint8_t>(*(pbuf+i),0x80);
		Serial.print(" ");
	}
	Serial.print("\n");
}
