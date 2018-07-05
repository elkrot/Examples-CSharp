#include "stdafx.h"
#include "MyCDll.h"
#include <stdio.h>

__declspec(dllexport) int SayHello(char* pszBuffer, int nLength)
{
	::strcpy_s(pszBuffer, nLength, "Hello, from C DLL");
	return strlen(pszBuffer);
}
