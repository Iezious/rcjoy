#include "CalcMap.h"
#include "Calculator.h"
#include "functions.h"
#include "StatusBox.h"
#include "EEPRom.h"
//#include "menu.h"

union bitmapper
{
	uint32_t val;
	uint8_t buffer[4];
};

extern "C" uint8_t *USB_HID_GetLastReport();
extern "C" uint8_t CheckJoystick(uint16_t vendor, uint16_t product);


axis_t ExtractJoysticBits(uint16_t start, uint8_t length)
{
	bitmapper val;

	uint8_t *buffer = USB_HID_GetLastReport();
	if (!buffer) return 0;

	buffer += start >> 3;

	val.buffer[0] = *(buffer++);
	val.buffer[1] = *(buffer++);
	val.buffer[2] = *(buffer++);
	val.buffer[3] = *(buffer++);

	uint32_t res = val.val;
	res = res >> (start & 0x7);
	res = res << (32 - length);
	res = res >> (32 - length);

	return (uint16_t)res;
}

uint8_t ExtractJoysticButton(uint16_t start)
{
	uint8_t *buffer = USB_HID_GetLastReport();
	if (!buffer) return 0;

	buffer += start >> 3;

	uint8_t sbyte = *(buffer);
	return (sbyte >> (start & 0x7)) & 0x1;
}

static bool ExtractJoyAxis(uint32_t *datapointer)
{
	uint16_t start = extract_u16;
	uint16_t length = extract_u16;
	idx_t dest = extract_u16;

	set_val(dest, ExtractJoysticBits(start, length));
	return true;
}

static bool MapButton(uint32_t *datapointer)
{
	uint16_t start = extract_u16;
	idx_t dest = extract_u16;

	uint8_t state = ExtractJoysticButton(start);
	uint16_t cstate = *(DATAMAP + dest);

	set_val(dest, (((cstate & 1) ^ state) << 1) | state);
	return true;

}

static bool MapHAT(uint32_t *datapointer)
{
	uint16_t start = extract_u16;
	uint16_t length = extract_u16;
	idx_t dest = extract_u16;
	idx_t dest_changed = extract_u16;

	axis_t value = ExtractJoysticBits(start, length);
	uint16_t cstate = *(DATAMAP + dest);

	set_val(dest, value);
	set_val(dest_changed, value != cstate);
	return true;

}

static bool MapAxis(uint32_t *datapointer)
{
	idx_t data = extract_u16;
	int16_t min = extract_u16;
	int16_t max = extract_u16;

	idx_t dest = extract_u16;

	set(dest, map(get(data), min, max, AXE_MIN, AXE_MAX));
	return true;

}

static bool SetPPMCustom(uint32_t *datapointer)
{
	idx_t data = extract_u16;
	uint16_t chan = extract_u16;
	uint16_t flags = extract_u16;

	uint16_t v;

	v = extract_u16;
	axis_t min = (flags & 1) ? get(v) : v;

	v = extract_u16;
	axis_t mid = (flags & 2) ? get(v) : v;

	v = extract_u16;
	axis_t max = (flags & 4) ? get(v) : v;

	map_ppm(data, min, mid, max, chan);
	return true;

}

static bool SetPPMDef(uint32_t *datapointer)
{
	idx_t data = extract_u16;
	uint16_t chan = extract_u16;
	map_ppm(data, CurrentModel->PPMMin, CurrentModel->PPMCenter, CurrentModel->PPMMax, chan);
	return true;
}

static bool SwitchModel(uint32_t *datapointer)
{
	idx_t model_index = extract_u16;
	idx_t model_button = extract_u16;

	if (CHECK_PRESSED(model_button))
	{
		SwitchCurrentModel(model_index);
		return false;
	}
	return true;
}

static bool ButtonHoldValue(uint32_t *datapointer)
{
	idx_t btn = extract_u16;
	axis_t pv = extract_s16;
	axis_t rv = extract_s16;
	idx_t idx_dest = extract_u16;

	axis_t val = get(idx_dest);

	if (CHECK_DOWN(btn))
	{
		set_val(idx_dest, pv);
		set_val(idx_dest + 1, pv == val);
	}
	else
	{
		set_val(idx_dest, rv);
		set_val(idx_dest + 1, rv != val);
	}

	return true;
}

static bool LogicalButton(uint32_t *datapointer)
{
	idx_t btn = extract_u16;
	idx_t pmbtn = extract_u16;
	idx_t rmbtn = extract_u16;
	idx_t idx_dest = extract_u16;

	axis_t down = CHECK_DOWN(btn)
		&& (pmbtn == 0 || CHECK_DOWN(pmbtn))
		&& (rmbtn == 0 || !CHECK_DOWN(rmbtn)) ? 1 : 0;

	axis_t v = get(idx_dest) & BUTTON_DOWN;

	set_val(idx_dest, down | ((v ^ down) << 1));
	return true;
}

static bool LogicalOrButton(uint32_t *datapointer)
{
	idx_t btn1 = extract_u16;
	idx_t btn2 = extract_u16;
	idx_t idx_dest = extract_u16;

	axis_t down = CHECK_DOWN(btn1) || CHECK_DOWN(btn2);
	axis_t v = get(idx_dest) & BUTTON_DOWN;

	set_val(idx_dest, down | ((v ^ down) << 1));
	return true;
}



static bool MapValueCond(uint32_t *datapointer)
{
	idx_t inidx = extract_u16;
	axis_t val = extract_u16;
	idx_t count = extract_u16;
	idx_t idx_dest = extract_u16;

	idx_t inval = get(inidx);



	for (u8 i = 0; i < count; i++)
	{
		axis_t _case = extract_s16;
		axis_t _value = extract_s16;

		if (inval == _case)
			val = _value;
	}

	set_val(idx_dest + 1, val != get(idx_dest));
	set_val(idx_dest, val);

	return true;
}

static bool InitValue(uint32_t *datapointer)
{
	idx_t _value = extract_s16;
	idx_t idx_dest = extract_u16;

	set_val(idx_dest, _value);
	set_val(idx_dest + 1, false);
	set_val(idx_dest + 2, 0);

	return true;
}

static bool SwitchValue(uint32_t *datapointer)
{
	idx_t btn = extract_u16;
	idx_t count = extract_u16;
	idx_t idx_dest = extract_u16;
	idx_t str = extract_u16;

	s16 *ptrs = (s16*)*datapointer;
	*datapointer += (count)* 4;

	if (CHECK_PRESSED(btn))
	{
		u16 state = get(idx_dest + 2);
		state++;
		if (state == count) state = 0;

		set_val(idx_dest, *(ptrs + state * 2));
		set_val(idx_dest + 1, true);
		set_val(idx_dest + 2, state);

		if (str && *(ptrs + state + 1))
		{
			STATUSBOX.SetData(STATE, CalcData->STRINGS[str], CalcData->STRINGS[*(ptrs + state * 2 + 1)]);
		}
	}
	else
	{
		set_val(idx_dest + 1, false);
	}

	return true;
}

static bool InitVariable(uint32_t *datapointer)
{
	idx_t idx_dest = extract_u16;
	axis_t val = extract_s16;

	set_val(idx_dest, val);
	set_val(idx_dest + 1, 0);

	return true;
}

static bool InitEEPVariable(uint32_t *datapointer)
{
	idx_t idx_variable = extract_u16;
	axis_t val = extract_s16;

	VariableDef* __var = CalcData->Variables + idx_variable;

	axis_t eval;

	u16 ea = __var->EEPROM_ADDR;
	u16 da = __var->DATAMAP_ADDR;

	if (EEPROM.Read(ea, &eval))
	{
		set_val(da, eval);
	}
	else
	{
		set_val(da, val);
		EEPROM.Write(ea, val);
	}
	set_val(da + 1, 0);

	return true;
}

static bool ExecVariable(uint32_t *datapointer)
{
	idx_t btnInc = extract_u16;
	idx_t btnDec = extract_u16;
	axis_t valDelta = extract_s16;

	idx_t btnReset = extract_u16;
	axis_t valReset = extract_s16;

	idx_t idx_variable = extract_u16;
	VariableDef* __var = CalcData->Variables + idx_variable;

	axis_t value = get(__var->DATAMAP_ADDR);

	if (btnInc && CHECK_PRESSED(btnInc))
	{
		set_val(__var->DATAMAP_ADDR, value = value + valDelta);
	}
	else if (btnDec && CHECK_PRESSED(btnDec))
	{
		set_val(__var->DATAMAP_ADDR, value = value - valDelta);
	}
	else if (btnReset && CHECK_PRESSED(btnReset))
	{
		set_val(__var->DATAMAP_ADDR, value = valReset);
	}
	else
	{
		set_val(__var->DATAMAP_ADDR + 1, 0);
		return true;
	}

	set_val(__var->DATAMAP_ADDR + 1, 1);

	if (__var->Name)
		STATUSBOX.SetData(VARIABLE, __var->Name, value);

	if (__var->EEPROM_ADDR)
		EEPROM.Write(__var->EEPROM_ADDR, value);

	return true;
}

static bool VariableToAxis(uint32_t *datapointer)
{
	idx_t idx_var = extract_u16;
	idx_t idx_dest = extract_u16;

	set(idx_dest, get(idx_var));
	return true;
}

static bool ApplyTrimmer(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t idx_var = extract_u16;
	idx_t idx_dest = extract_u16;

	trim(idx_input, idx_var, idx_dest);
	return true;
}

static bool ApplyCenteredExp(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	s16 idx_exp = extract_s16;
	idx_t idx_dest = extract_u16;

	map_exp(idx_input, idx_exp, idx_dest);
	return true;
}

static bool ApplyThExp(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	s16 idx_exp = extract_s16;
	idx_t idx_dest = extract_u16;

	map_th_exp(idx_input, idx_exp, idx_dest);
	return true;
}

static bool InvertAxis(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t idx_dest = extract_u16;

	invert(idx_input, idx_dest);
	return true;
}

static bool ReMapAxis(uint32_t *datapointer)
{
	idx_t count = extract_u16;
	idx_t vmaps = extract_u16;

	axis_t min;
	axis_t middle;
	axis_t max;

	if (vmaps & 1)
	{
		idx_t idx = extract_u16;
		min = get(idx);
	}
	else
	{
		min = extract_s16;
	}

	if (vmaps & 2)
	{
		idx_t idx = extract_u16;
		middle = get(idx);
	}
	else
	{
		middle = extract_s16;
	}

	if (vmaps & 4)
	{
		idx_t idx = extract_u16;
		max = get(idx);
	}
	else
	{
		max = extract_s16;
	}

	for (u8 i = 0; i < count; i++)
	{
		idx_t idx_input = extract_u16;
		idx_t idx_dest = extract_u16;

		map_value(idx_input, min, middle, max, idx_dest);
	}

	return true;
}

static bool AilRudderMix(uint32_t *datapointer)
{
	idx_t rudder_in = extract_u16;
	idx_t ail_in = extract_u16;
	idx_t q = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_aileron_to_rudder(rudder_in, ail_in, q, idx_dest);
	return true;
}

static bool ThElevatorMix(uint32_t *datapointer)
{
	idx_t th_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t q = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_throttle_to_elev(el_in, th_in, q, idx_dest);
	return true;
}

static bool DeltaMix(uint32_t *datapointer)
{
	idx_t ai_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t q = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_delta(el_in, ai_in, q, idx_dest, idx_dest + 1);
	return true;
}

static bool VTailMix(uint32_t *datapointer)
{
	idx_t ru_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t q = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_delta(ru_in, el_in, q, idx_dest, idx_dest + 1);
	return true;
}

static bool FlapMix(uint32_t *datapointer)
{
	idx_t ai_in = extract_u16;
	idx_t fl_in = extract_u16;
	idx_t q = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_flapperons(ai_in, fl_in, q, idx_dest, idx_dest + 1);
	return true;
}

static bool MenuStart(uint32_t *datapointer)
{
	idx_t btnStart = extract_u16;
	/*
	if (CHECK_PRESSED(btnStart))
	{
		SYSMENU.Start();
	}
	*/
	return true;
}

static bool MenuSelection(uint32_t *datapointer)
{
	idx_t btnStart = extract_u16;
	/*
	if (CHECK_PRESSED(btnStart))
	{
		SYSMENU.Start();
		return false;
	}
	*/

	return true;
}

static bool MenuButtons(uint32_t *datapointer)
{
	idx_t btnNextSc = extract_u16;
	idx_t btnPrevSc = extract_u16;
	idx_t btnNextSel = extract_u16;
	idx_t btnPrevSel = extract_u16;
	idx_t btnInc = extract_u16;
	idx_t btnDec = extract_u16;
	idx_t btnSet = extract_u16;
	/*
	if (CHECK_PRESSED(btnNextSc))
	{
		SYSMENU.NextSection();
	}
	else if (CHECK_PRESSED(btnPrevSc))
	{
		SYSMENU.PrevSection();
	}
	else if (CHECK_PRESSED(btnNextSel))
	{
		SYSMENU.NextSelect();
	}
	else if (CHECK_PRESSED(btnPrevSel))
	{
		SYSMENU.PrevSelect();
	}
	else if (CHECK_PRESSED(btnInc))
	{
		SYSMENU.IncVariable();
	}
	else if (CHECK_PRESSED(btnDec))
	{
		SYSMENU.DecVariable();
	}
	else if (CHECK_PRESSED(btnSet))
	{
		SYSMENU.Execute();
	}
	*/
	return true;
}

static bool HatToKey(uint32_t *datapointer)
{
	idx_t valIn = extract_u16;
	idx_t cnt = extract_u16;

	axis_t value = *(DATAMAP + valIn);

	for (u8 i = 0; i < cnt; i++)
	{
		s16 bval = extract_s16;
		idx_t dest = extract_u16;

		uint16_t state = (value == bval) ? 1 : 0;
		uint16_t cstate = *(DATAMAP + dest);
		set_val(dest, (((cstate & 1) ^ state) << 1) | state);
	}

	return true;
}

static bool ConstPPMMap(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;

	axis_t min = extract_s16;
	axis_t middle = extract_s16;
	axis_t max = extract_s16;


	idx_t idx_dest = extract_u16;

	map_value(idx_input, min, middle, max, idx_dest);

	return true;
}

static bool VariablePPMMap(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;

	idx_t idx_min = extract_s16;
	idx_t idx_middle = extract_s16;
	idx_t idx_max = extract_s16;

	idx_t idx_dest = extract_u16;

	map_value(idx_input, get_variable(idx_min), get_variable(idx_middle), get_variable(idx_max), idx_dest);

	return true;
}

static bool FlightModePPMMap(uint32_t *datapointer)
{
	idx_t idx_variable = extract_u16;
	idx_t test_state = extract_u16;
	idx_t count = extract_u16;

	if (get_variable(idx_variable) == test_state)
	{
		u16 data;
		u16 chan;

		for (u8 i = 0; i < count; i++)
		{
			data = extract_u16;
			chan = extract_u16;

			map_ppm(data, CurrentModel->PPMMin, CurrentModel->PPMCenter, CurrentModel->PPMMax, chan);
		}
	}
	else
	{
		for (u8 i = 0; i < count; i++)
		{
			extract_u16;
			extract_u16;
		}
	}

	return true;
}

static bool SwitchFlightMode(uint32_t *datapointer)
{
	idx_t button_idx = extract_u16;
	idx_t fm_idx = extract_u16;

	VariableDef* state_var = CalcData->Variables + CurrentModel->ModesStoreVariable;


	if (CHECK_PRESSED(button_idx) && (get(state_var->DATAMAP_ADDR) != fm_idx))
	{
		set_val(state_var->DATAMAP_ADDR, fm_idx);

		if (state_var->EEPROM_ADDR)
			EEPROM.Write(state_var->EEPROM_ADDR, (s16)fm_idx);

		STATUSBOX.SetFlightMode(*(CurrentModel->Modes + fm_idx));

		return false;
	}

	return true;
}

static bool ExecModeVariable(uint32_t *datapointer)
{
	idx_t btnInc = extract_u16;
	idx_t btnDec = extract_u16;
	axis_t valDelta = extract_s16;

	idx_t btnReset = extract_u16;
	axis_t valReset = extract_s16;

	idx_t idx_variable = extract_u16;
	idx_t idx_mode_variable = extract_u16;
	idx_t test_state = extract_u16;

	if (get_variable(idx_mode_variable) == test_state)
	{
		VariableDef* __var = CalcData->Variables + idx_variable;

		axis_t value = get(__var->DATAMAP_ADDR);

		if (btnInc && CHECK_PRESSED(btnInc))
		{
			set_val(__var->DATAMAP_ADDR, value = value + valDelta);
		}
		else if (btnDec && CHECK_PRESSED(btnDec))
		{
			set_val(__var->DATAMAP_ADDR, value = value - valDelta);
		}
		else if (btnReset && CHECK_PRESSED(btnReset))
		{
			set_val(__var->DATAMAP_ADDR, value = valReset);
		}
		else
		{
			set_val(__var->DATAMAP_ADDR + 1, 0);
			return true;
		}

		set_val(__var->DATAMAP_ADDR + 1, 1);

		if (__var->Name)
			STATUSBOX.SetData(VARIABLE, __var->Name, value);

		if (__var->EEPROM_ADDR)
			EEPROM.Write(__var->EEPROM_ADDR, value);
	}

	return true;
}

static bool ExecTrimmerEmulation(uint32_t *datapointer)
{
	idx_t btnSet = extract_u16;
	idx_t btnReset = extract_u16;

	idx_t idx_input = extract_u16;
	idx_t idx_dest = extract_u16;

	switch (get(idx_dest + 1))
	{
		case 0:
			if (CHECK_PRESSED(btnSet))
			{
				set_val(idx_dest + 1, 1);  //state = 1
				set_val(idx_dest + 2, get(idx_input)); // store current value
			}

			set(idx_dest, get(idx_input));
			break;
		
		case 1:
			if (!CHECK_DOWN(btnSet))
				set_val(idx_dest + 1, 2);  //state = 2

			set_val(idx_dest, get(idx_dest + 2));
			break;

		case 2:
		{
			if (CHECK_PRESSED(btnReset))
			{
				set_val(idx_dest + 1, 0);  //state = 0
				set(idx_dest, get(idx_input));
			}
			else if (CHECK_PRESSED(btnSet))
			{
				set_val(idx_dest + 1, 1);  //state = 1
				map_value(idx_input, AXE_MIN, get(idx_dest + 2), AXE_MAX, idx_dest);
				set(idx_dest + 2, get(idx_dest));
			}
			else
			{
				map_value(idx_input, AXE_MIN, get(idx_dest + 2), AXE_MAX, idx_dest);
			}
		}
	}

	return true;
}

static bool InitTrimmerEmulation(uint32_t *datapointer)
{
	idx_t idx_dest = extract_u16;
	
	set_val(idx_dest, 0); 
	set_val(idx_dest + 1, 0);
	set_val(idx_dest + 2, 0);

	return true;
}

static bool AilRudderMixStored(uint32_t *datapointer)
{
	idx_t rudder_in = extract_u16;
	idx_t ail_in = extract_u16;
	idx_t qaddr = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_aileron_to_rudder(rudder_in, ail_in, get(qaddr), idx_dest);
	return true;
}

static bool DeltaMixStored(uint32_t *datapointer)
{
	idx_t ai_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t qaddr = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_delta(el_in, ai_in, get(qaddr), idx_dest, idx_dest + 1);
	return true;
}

static bool FlapMixStored(uint32_t *datapointer)
{
	idx_t ai_in = extract_u16;
	idx_t fl_in = extract_u16;
	idx_t qaddr = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_flapperons(ai_in, fl_in, get(qaddr), idx_dest, idx_dest + 1);
	return true;
}

static bool ThElevatorMixStored(uint32_t *datapointer)
{
	idx_t th_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t qaddr = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_throttle_to_elev(el_in, th_in, get(qaddr), idx_dest);
	return true;
}

static bool VTailMixStored(uint32_t *datapointer)
{
	idx_t ru_in = extract_u16;
	idx_t el_in = extract_u16;
	idx_t qaddr = extract_s16;

	idx_t idx_dest = extract_u16;

	mix_delta(ru_in, el_in, get(qaddr), idx_dest, idx_dest + 1);
	return true;
}

static bool ApplyCenteredExpStored(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t idx_exp = extract_s16;
	idx_t idx_dest = extract_u16;

	map_exp(idx_input, get(idx_exp), idx_dest);
	return true;
}

static bool ApplyThExpStored(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t idx_exp = extract_s16;
	idx_t idx_dest = extract_u16;

	map_th_exp(idx_input, get(idx_exp), idx_dest);
	return true;
}

static bool SetValueIfButton(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	s16 value = extract_s16;
	idx_t idx_dest = extract_u16;

	if (CHECK_PRESSED(idx_input))
	{
		set_val(idx_dest, value);
	}

	return true;
}

static bool SetNamedValueIfButton(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t idx_name = extract_u16;
	s16 value = extract_s16;

	idx_t idx_dest = extract_u16;

	if (CHECK_PRESSED(idx_input))
	{
		STATUSBOX.SetData(DATA_SWITCH, STRING(idx_name));
		set_val(idx_dest, value);
	}

	return true;
}

static bool ShowTrimmer(uint32_t *datapointer)
{
	idx_t idx_var = extract_u16;
	idx_t trm_tp = extract_u16;

	if (trm_tp != 0) STATUSBOX.SetTrimmer(trm_tp, get(idx_var));
	return true;
}

static bool ShowModeTrimmer(uint32_t *datapointer)
{
	idx_t idx_var = extract_u16;
	idx_t trm_tp = extract_u16;
	idx_t mode_idx = extract_u16;
	idx_t idx_mode_variable = extract_u16;

	if (
			(get_variable(idx_mode_variable) == mode_idx) &&
			(trm_tp != 0)
		)
	{
		STATUSBOX.SetTrimmer(trm_tp, get(idx_var));
	}
	
	return true;
}

static bool InitThrottleCut(uint32_t *datapointer)
{
	idx_t idx_dest = extract_u16;
	set(idx_dest+1, 0);

	return true;
}

static bool SetThrottleCut(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t set_btn = extract_u16;
	idx_t clr_btn = extract_u16;
	idx_t idx_dest = extract_u16;

	if (CHECK_PRESSED(set_btn))
	{
		set_val(idx_dest + 1, 1);
		STATUSBOX.SetTrimmer(5, 1);
	}
	else if (CHECK_PRESSED(clr_btn))
	{
		set_val(idx_dest + 1, 0);
		STATUSBOX.SetTrimmer(5, 0);
	}

	if (get(idx_dest + 1))
	{
		set(idx_dest, 0);
	}
	else
	{
		set(idx_dest, get(idx_input));
	}

	return true;
}

static bool SwitchThrottleCut(uint32_t *datapointer)
{
	idx_t idx_input = extract_u16;
	idx_t sw_btn = extract_u16;
	idx_t idx_dest = extract_u16;

	s16 state = get(idx_dest + 1);

	if (CHECK_PRESSED(sw_btn))
	{
		state = state ? 0 : 1;
		set_val(idx_dest + 1, state);
		STATUSBOX.SetTrimmer(5, state);
	}

	if (state)
	{
		set(idx_dest, 0);
	}
	else
	{
		set(idx_dest, get(idx_input));
	}

	return true;
}

static bool CheckJoystickInfo(uint32_t *datapointer)
{
	uint16_t vendor = extract_u16;
	uint16_t product = extract_u16;

	return true;

//	return CheckJoystick(vendor, product) != 0;
}

static bool MultiHoldSwitch(uint32_t *datapointer)
{
	idx_t count = extract_u16;
	axis_t value = extract_s16;
	idx_t idx_dest = extract_u16;

	for (u8 i = 0; i < count; i++)
	{
		idx_t button = extract_u16;
		axis_t bval = extract_s16;

		if (CHECK_DOWN(button))
			value = bval;
	}

	set_val(idx_dest + 1, get(idx_dest) != value);
	set_val(idx_dest, value);
}


calcFunc FunctionsMap[] =
{
	(calcFunc)0,
	ExtractJoyAxis,			// 0x01
	MapButton,				// 0x02
	MapHAT,					// 0x03
	MapAxis,				// 0x04
	SwitchModel,			// 0x05
	SetPPMCustom,			// 0x06
	SetPPMDef,				// 0x07
	ButtonHoldValue,		// 0x08
	LogicalButton,			// 0x09	
	MapValueCond,			// 0x0A
	InitValue,				// 0x0B
	SwitchValue,			// 0x0C
	InitVariable,			// 0x0D
	ExecVariable,			// 0x0E
	InitEEPVariable,		// 0x0F
	VariableToAxis,			// 0x10
	ApplyTrimmer,			// 0x11
	ApplyCenteredExp,		// 0x12
	ApplyThExp,				// 0x13
	InvertAxis,				// 0x14
	ReMapAxis,				// 0x15
	AilRudderMix,			// 0x16
	ThElevatorMix,			// 0x17
	DeltaMix,				// 0x18
	VTailMix,				// 0x19
	FlapMix,				// 0x1A
	MenuStart,				// 0x1B
	MenuButtons,			// 0x1C
	HatToKey,				// 0x1D
	ConstPPMMap,			// 0x1E
	VariablePPMMap,			// 0x1F
	FlightModePPMMap,		// 0x20
	SwitchFlightMode,		// 0x21
	ExecModeVariable,		// 0x22
	ExecTrimmerEmulation,	// 0x23
	InitTrimmerEmulation,	// 0x24
	AilRudderMixStored,		// 0x25
	DeltaMixStored,			// 0x26
	FlapMixStored,			// 0x27
	ThElevatorMixStored,	// 0x28
	VTailMixStored,			// 0x29
	ApplyCenteredExpStored, // 0x2A
	ApplyThExpStored,		// 0x2B
	SetValueIfButton,		// 0x2C
	ShowTrimmer,			// 0x2D
	ShowModeTrimmer,		// 0x2E
	SetNamedValueIfButton,  // 0x2F
	InitThrottleCut,		// 0x30
	SetThrottleCut,			// 0x31
	SwitchThrottleCut,		// 0x32
	LogicalOrButton,        // 0x33
	CheckJoystickInfo,		// 0x34
	MultiHoldSwitch,		// 0x35
};