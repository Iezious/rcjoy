#ifndef _FUNCTIONS_H_
#define _FUNCTIONS_H_

#include <Arduino.h>
#include <avr/eeprom.h>
#include <eeprom.h>
#include "config.h"
#include "def.h"
#include "Joystick.h"
#include "datamap.h"
#include "ppmGen.h"

#define set(idx, value) *(CalcData+idx) = value < AXE_MIN ? AXE_MIN : value > AXE_MAX ? AXE_MAX : value
#define set_val(idx, value) *(CalcData+idx) = value
#define get(idx) *(CalcData+idx)

#define map_axe(val, vmin, vmax, out) set(out, map(val, vmin, vmax, AXE_MIN, AXE_MAX))

void map_exp(idx_t in, int16_t exp, idx_t out);
void map_th_exp(idx_t in, int16_t exp, idx_t out);
void map_ppm(idx_t in, uint16_t chan_min, uint16_t chan_middle, uint16_t chan_max, idx_t ppm_chan); 
void map_value(idx_t in, uint16_t chan_min, uint16_t chan_middle, uint16_t chan_max, idx_t out);


void saveEepVal(idx_t val, axis_t addr);
axis_t getEepVal(axis_t addr);
void setEep(idx_t idx, axis_t val, idx_t addr);

#define trim(in, trimmer, out) set(out, get(in) + get(trimmer))
#define invert(in, out) set(out, AXE_MAX + AXE_MIN - get(in))

void mix_one_chan(idx_t ch1_in, idx_t ch2_in, uint16_t ch1q, uint16_t ch2q, idx_t out);
void mix_two_chan(idx_t ch1_in, idx_t ch2_in, uint16_t ch1q, uint16_t ch2q, idx_t out_1, idx_t out_2);


#define mix_delta(elevator_in, aileron_in, ail_q, out_l, out_r) mix_two_chan(elevator_in, aileron_in, 100, ail_q, out_l, out_r)
#define mix_flapperons(aileron_in, flaps_in, flap_q, out_l, out_r) mix_two_chan(aileron_in, flaps_in, 100-flap_q, flap_q, out_l, out_r)
#define mix_vtail(elevatir_in, ruidder_in, rudder_q, out_l, out_r) mix_two_chan(elevatir_in, ruidder_in, 100-rudder_q, rudder_q, out_l, out_r)
#define mix_aileron_to_rudder(rudder, ailerons, mix_q, out) mix_one_chan(rudder, ailerons, 100, mix_q, out)
#define mix_throttle_to_elev(elev, throt, mix_q, out) mix_one_chan(elev, throt, 100, mix_q, out)

void switchModel(uint16_t model, uint16_t ppm_channels);

#ifdef DEBUG
#define WRITE_DEBUG SCOM.WriteDebug()
#endif

#endif