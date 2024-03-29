// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <ppl.h>
#include <math.h>
#include <iostream>
#include <Windows.h>
#include <conio.h>

using namespace std;
using namespace concurrency;

long timeParallel = 0;
long timeSerial = 0;

void isTub(int number)
{
	
	for (int i = 2; i < sqrt(number); i++)
	{
		if (number % i == 0)
			return;
	}
	//cout << number <<endl;
	
}

int main()
{
	long begin = GetTickCount();
	parallel_for(0, 100000, 1, isTub);
	timeParallel = GetTickCount() - begin;
	
	begin = GetTickCount();
	for (int i = 0; i < 100000; i++)
	{
		isTub(i);
	}
	timeSerial = GetTickCount() - begin;

	cout << endl << endl << "Parallel : "<<timeParallel;
	cout << endl << endl << "Serial : "<<timeSerial;

	_getch();

    return 0;
}

