#ifndef __MENU_H__
#define __MENU_H__

#include "StatusBox.h"
#include "Calculator.h"

class SysMenu
{
public:
	bool MenuActive = false;

	void Start();

	void NextSection();
	void PrevSection();
	
	void NextSelect();
	void PrevSelect();

	void IncVariable();
	void DecVariable();

	void Execute();
};


extern SysMenu SYSMENU;

#endif