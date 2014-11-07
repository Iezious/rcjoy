#include "StaticList.h"

StaticList NotUsedList;
StaticList UsedList;

extern "C" void DeAllocateHID(uint32_t r)
{
	UsedList.Remove(r);
	NotUsedList.Add(r);
}


extern "C" void RegisterHID(uint32_t HID)
{
	NotUsedList.Add(HID);
}

extern "C" uint32_t AllocateHID()
{
	uint32_t r = NotUsedList.GetAndRemoveLast();
	UsedList.Add(r);

	return r;
}
