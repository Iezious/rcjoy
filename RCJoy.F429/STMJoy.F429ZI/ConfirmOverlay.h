#pragma once

#include <inttypes.h>
#include "def.h"
#include "gui.h"

struct ConfirmOvelayData
{
  uint8_t *Caption;
  uint8_t *SubCaption;
  uint32_t Tag;
  void (*Confirmed)(uint32_t tag);
  void (*Declined) (uint32_t tag);
};

extern void ShowConfirmDialog(ConfirmOvelayData *data);