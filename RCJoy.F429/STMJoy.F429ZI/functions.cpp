#include "conf.h"
#include "def.h"
#include "functions.h"
#include "Calculator.h"
#include "PPMGenerator.h"
#include "EEPRom.h"
 
#include <math.h>
// 
// 
// 


#include "functions.h"


void map_exp(idx_t in, int16_t exp, idx_t out)
{
	axis_t val = get(in);
	int32_t lval = val;

	if(lval == AXE_MAX || lval == AXE_MIN) 
	{
		set(out, lval);
		return;
	}


	lval = abs(lval - AXE_MIDDLE);
	int32_t lexp = exp;

	lval = (lexp*lval*lval/(AXE_MAX - AXE_MIDDLE) + (10-lexp)*lval)/10;
	//((10-lexp)*lval*lval/10000) + lexp*lval/10;
	val = (axis_t) (val >= AXE_MIDDLE ? (AXE_MIDDLE + lval) : (AXE_MIDDLE - lval));
	set(out, val);
}

void map_th_exp(idx_t in, int16_t exp, idx_t out)
{
	int32_t lval = get(in);
	if(lval == AXE_MAX || lval == AXE_MIN) 
	{
		set(out, lval);
		return;
	}

	int32_t lexp = exp;

	lval = (lexp*lval*lval/(AXE_MAX - AXE_MIN) + (10-lexp)*lval)/10;
	set(out, (axis_t) lval);
}


/*
void mix_aileron_to_rudder(idx_t ailerons, idx_t rudder, uint16_t mix_q, idx_t out)
{
	int32_t ch_a = get(ailerons) - AXE_MIDDLE;
	int32_t ch_r = get(rudder) - AXE_MIDDLE;

	int32_t och1 = (ch_a * mix_q + ch_r * 100)/100 + AXE_MIDDLE;

	set(out, (axis_t)och1);
}

void mix_throttle_to_elev(idx_t throt, idx_t elev, uint16_t mix_q, idx_t out)
{
	int32_t ch_t = (get(throt) - AXE_MIN) / 2;
	int32_t ch_e = get(elev) - AXE_MIDDLE;

	int32_t och1 = (ch_t * mix_q + ch_e * 100)/100 + AXE_MIDDLE;

	set(out, (axis_t)och1);
}

void mix_flapperons(idx_t aileron_in, idx_t flaps_in, uint16_t flap_q, idx_t out_l, idx_t out_r)
{
	int32_t ail_v = get(aileron_in) - AXE_MIDDLE;
	int32_t flap_v = (get(flaps_in) - AXE_MIN) / 2;

	int32_t ail_q = 100-flap_q;

	int32_t och1 = (flap_v * flap_q + ail_v * ail_q) + AXE_MIDDLE;
	int32_t och2 = (flap_v * flap_q - ail_v * ail_q) + AXE_MIDDLE;

	set(out_l, (uint16_t)och1);
	set(out_r, (uint16_t)och2);
}
*/

void mix_one_chan(idx_t ch1_in, idx_t ch2_in, uint16_t ch1q, uint16_t ch2q, idx_t out)
{
	int32_t ch1 = get(ch1_in) - AXE_MIDDLE;
	int32_t ch2 = get(ch2_in) - AXE_MIDDLE;

	int32_t och1 = (ch1 * ch1q + ch2 * ch2q)/100 + AXE_MIDDLE;
	set(out, (axis_t) och1);
}


void mix_two_chan(idx_t ch1_in, idx_t ch2_in, uint16_t ch1q, uint16_t ch2q, idx_t out_1, idx_t out_2)
{
	int32_t ch1 = get(ch1_in) - AXE_MIDDLE;
	int32_t ch2 = get(ch2_in) - AXE_MIDDLE;

	int32_t och1 = (ch1 * ch1q + ch2 * ch2q)/100 + AXE_MIDDLE;
	int32_t och2 = (ch1 * ch1q - ch2 * ch2q)/100 + AXE_MIDDLE;

	set(out_1, (axis_t)och1);
	set(out_2, (axis_t)och2);
}


void switchModel(uint16_t model, uint16_t ppm_channels)
{
	CurrentModel = CalcData->Models + model;
	CurrentModelIdx = model;
	PPMGen.setChannels(CurrentModel->PPMChannels);
}

void saveEepVal(axis_t val, idx_t addr)
{
	EEPROM.Write(addr, val);
}


axis_t getEepVal(uint16_t addr)
{
	axis_t res;
	return EEPROM.Read(addr, &res) ? res : 0;
}

void setEep(idx_t idx, axis_t val, idx_t addr)
{
	set_val(idx, val); saveEepVal(val, addr);
}
