// (C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// cominteropPart2\COMClient.cpp
// Выполните построение этой части примера при помощи командной строки:
//  cl COMClient.cpp

#include <windows.h>
#include <stdio.h>

#pragma warning (disable: 4278)

// Чтобы использовать серверы с управляемым кодом, например сервер C#,
// необходимо импортировать общеязыковую среду выполнения:
#import <mscorlib.tlb> raw_interfaces_only

// Для упрощения мы игнорируем пространство имен сервера и используем именованные GUID:
#if defined (USINGPROJECTSYSTEM)
#import "..\RegisterCSharpServerAndExportTLB\CSharpServer.tlb" no_namespace named_guids
#else  // Компиляция из командной строки, все файлы -- в одном каталоге
#import "CSharpServer.tlb" no_namespace named_guids
#endif
int main(int argc, char* argv[])
{
    IManagedInterface *cpi = NULL;
    int retval = 1;

    // Инициализация COM и создание экземпляра класса InterfaceImplementation:
    CoInitialize(NULL);
    HRESULT hr = CoCreateInstance(CLSID_InterfaceImplementation,
                                  NULL,
                                  CLSCTX_INPROC_SERVER,
                                  IID_IManagedInterface,
                                  reinterpret_cast<void**>(&cpi));

    if (FAILED(hr))
    {
        printf("Couldn't create the instance!... 0x%x\n", hr);
    }
    else
    {
        if (argc > 1)
        {
            printf("Calling function.\n");
            // Переменная cpi теперь содержит указатель          // на управляемый интерфейс.
            // Если ваша ОС использует символы  ASCII в         //  командной строке, обратите внимание, что символы ASCII
            // для кода C# автоматически  маршалируются в формате Юникод.

            if (cpi->PrintHi(argv[1]) == 33)
                retval = 0;

            printf("Returned from function.\n");
        }
        else
            printf ("Usage:  COMClient <name>\n");
        cpi->Release();
        cpi = NULL;
    }

    // Не забудьте очистить COM:
    CoUninitialize();
    return retval;
}

