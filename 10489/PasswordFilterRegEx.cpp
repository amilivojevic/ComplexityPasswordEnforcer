// This is the main DLL file.

#include "stdafx.h"
#include <stdio.h>
#include <boost/regex.hpp>
#include <string>
#include <atlbase.h>


#define LOGFILE "c:\\FilterLog.txt"
#define MAX_REGEX_LENGTH 1024

using namespace boost;
using namespace std;

#define DEFAULT_REGEX L"^(A)$"

// Default DllMain implementation
BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}
    return TRUE;
}

// This is helper function to write log lines to file
void WriteToLog(const char* str)
{
#ifdef _WRITE_LOGS
	if (NULL == str)
	{
		return;
	}
	FILE* log;
	log = fopen(LOGFILE, "a+");
	if (NULL == log)
	{
		return;
	}
	fprintf(log, "%s\r\n", str);
	fclose(log);
#endif

	return;
}

// This function retrieves Regular Expression value
// from registry and updates passed by ref wregex object
void GetPasswordRegExFromRegistry(wregex& wrePassword)
{
	WriteToLog("GetPasswordRegExFromRegistry: start");
	CRegKey rkRegEx;

	// Open registry key and check if we succeeded
	WriteToLog("GetPasswordRegExFromRegistry: opening key");
	LONG res = rkRegEx.Open(HKEY_LOCAL_MACHINE,
							"Software\\DevX\\PasswordFilter",
							KEY_READ);
	if (ERROR_SUCCESS != res)
	{
		WriteToLog("GetPasswordRegExFromRegistry: failed open key");
		return;
	}

	ULONG ulSize = MAX_REGEX_LENGTH;
	char szRegEx[MAX_REGEX_LENGTH];

	// Query the RegEx value on open key
	// and check whether we succeeded
	WriteToLog("GetPasswordRegExFromRegistry: Query value");
	res = rkRegEx.QueryStringValue("RegEx", szRegEx, &ulSize);
	if ((ERROR_SUCCESS != res) ||
		(0 != szRegEx[ulSize-1]))
	{
		WriteToLog("GetPasswordRegExFromRegistry: Failed Query value");
		return;
	}

	// Close registry key
	rkRegEx.Close();

	WriteToLog(szRegEx);

	// Use the _bstr_t class for easier string handling
	_bstr_t bstrRegEx = szRegEx;

	// In case of empty RegEx string,
	// do not update wrePassword reference
	if (0 == bstrRegEx.length())
	{
		return;
	}

	wstring wstrRegEx;
	wstrRegEx = (wchar_t*)bstrRegEx;
	
	// Update wrePassword reference
	//with a new RegEx from Registry
	wrePassword = wstrRegEx;
	return;
}

/////////////////////////////////////////////
// Exported function
// -----------------
// Initialization of Password filter.
// This implementation just returns TRUE
// to let LSA know everything is fine
BOOLEAN __stdcall InitializeChangeNotify(void)
{
	WriteToLog("InitializeChangeNotify()");
	return TRUE;
}

////////////////////////////////////////////
// Exported function
// -----------------
// This function is called by LSA when password
// was successfully changed.
//
// This implementation just returns 0 (Success)
NTSTATUS __stdcall PasswordChangeNotify(
  PUNICODE_STRING UserName,
  ULONG RelativeId,
  PUNICODE_STRING NewPassword
)
{
	WriteToLog("PasswordChangeNotify()");
	return 0;
}

////////////////////////////////////////////
// Exported function
// -----------------
// This function actually validates
// a new password.
// LSA calls this function when a password is assign to a new user
// or password is changed on exisiting user.
// 
// This function return TRUE is password meets requirements
// that filter checks; FALSE is password does NOT meet these requirements
//
// In our implementation, specified Regular Expression must match new password
BOOLEAN __stdcall PasswordFilter(
  PUNICODE_STRING AccountName,
  PUNICODE_STRING FullName,
  PUNICODE_STRING Password,
  BOOLEAN SetOperation
)
{
	WriteToLog("Entering PasswordFilter()");

	wchar_t* wszPassword = NULL;
	wstring wstrPassword;
	bool bMatch = FALSE;
	try
	{
		// create a zero-terminating copy of a password string and 
		// assign it to an STL wstring object 
		// (STL is used in conjunction with the boost regex library)
		wszPassword = new wchar_t[Password->Length + 1];  
		if (NULL == wszPassword)
		{
			throw E_OUTOFMEMORY;
		}
		wcsncpy(wszPassword, Password->Buffer, Password->Length);
		wszPassword[Password->Length] = 0;

		WriteToLog("Going to check password");
		//Initailize STL string
		wstrPassword = wszPassword;

		// Validate password against regular expression
		wregex wrePassword(DEFAULT_REGEX);
		
		WriteToLog("Going to retrieve RegEx from Registry");
		GetPasswordRegExFromRegistry(wrePassword);

		WriteToLog("Going to run match");
		// Prepare iterators
		wstring::const_iterator start = wstrPassword.begin();
		wstring::const_iterator end = wstrPassword.end();
		
		match_results<wstring::const_iterator> what;
		unsigned int flags = match_default;
		bMatch = regex_match(start, end, what, wrePassword);
		if (bMatch)
		{
			WriteToLog("Password matches specified RegEx");
		}
		else
		{
			WriteToLog("Password does NOT match specified RegEx");
		}
		throw S_OK;
	}
	catch(HRESULT)
	{
	}
	catch(...)
	{
		WriteToLog("catch(...)");
	}
	// Erase all temporary password data
	// for security reasons
	wstrPassword.replace(0, wstrPassword.length(), wstrPassword.length(), (wchar_t)'?');
	wstrPassword.erase();
	if (NULL != wszPassword)
	{
		ZeroMemory(wszPassword, Password->Length);

		// Assure that there is no compiler optimizations and read random byte
		// from cleaned password string
		srand(time(NULL));
		wchar_t wch = wszPassword[rand() % Password->Length];
		delete [] wszPassword;
		wszPassword = NULL;
	}
	return bMatch;
}