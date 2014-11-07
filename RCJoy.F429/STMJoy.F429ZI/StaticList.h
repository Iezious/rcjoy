#pragma once
#include  <inttypes.h>

#define MAX_LIST 16

class StaticList
{
private:
	uint32_t Storage[16] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	uint32_t Position;

public:
	void Add(uint32_t ptr)
	{
		Storage[Position] = ptr;
		Position++;
	}
	
	uint32_t Get(uint32_t idx)
	{
		return Storage[idx];
	}

	uint32_t GetCount()
	{
		return Position;
	}

	uint32_t GetAndRemoveLast()
	{
		if (Position == 0) return 0;
		Position--;
		uint32_t v = Storage[Position];
		Storage[Position] = 0;
		return v;
	}

	void Remove(uint32_t ptr)
	{
		int32_t idx = -1;

		for (int32_t i = 0; i < MAX_LIST; i++)
		{
			if (Storage[i] == ptr)
			{
				idx = i;
				break;
			}
		}

		if (idx == -1) return;

		for (int32_t i = idx; i < MAX_LIST - 1; i++)
			Storage[i] = Storage[i + 1];

		Storage[MAX_LIST - 1] = 0;
	}
};

