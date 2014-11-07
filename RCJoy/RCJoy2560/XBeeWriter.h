// XBeeWriter.h

#ifndef _XBEEWRITER_h
#define _XBEEWRITER_h

#include <Arduino.h>

#include "config.h"
#include "def.h"
#include "ppmGen.h"

#ifdef XBEE

class XBeeWriter
{
 private:

 public:
	void init();
	void send();
};

extern XBeeWriter XBEEWRITER;
#endif

#endif

