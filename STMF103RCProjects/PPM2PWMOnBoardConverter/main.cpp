#include "PPMReader.h"
#include "PWMGenerator.h"
#include "SysTimer.h"

int main()
{
	StartSysTimer();
	StartPPMReader();
	StartGenerators();

    for (;;)  
	{
		asm("nop");
	}
}
