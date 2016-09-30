// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// readfile.cs
// компилировать с /unsafe
// аргументы: readfile.txt

// Использование программы для чтения и отображения текстового файла.
using System;
using System.Runtime.InteropServices;
using System.Text;

class FileReader
{
	const uint GENERIC_READ = 0x80000000;
	const uint OPEN_EXISTING = 3;
	IntPtr handle;

	[DllImport("kernel32", SetLastError=true)]
	static extern unsafe IntPtr CreateFile(
		string FileName,				// имя файла
		uint DesiredAccess,				// режим доступа
		uint ShareMode,					// режим общего доступа
		uint SecurityAttributes,		// Атрибуты безопасности
		uint CreationDisposition,		// способ создания
		uint FlagsAndAttributes,		// атрибуты файла
		int hTemplateFile				// дескриптор для файла шаблона
		);



	[DllImport("kernel32", SetLastError=true)]
	static extern unsafe bool ReadFile(
		IntPtr hFile,					// дескриптор для файла
		void* pBuffer,				// буфер данных
		int NumberOfBytesToRead,	// число байтов для чтения
		int* pNumberOfBytesRead,		// число прочитанных байтов
		int Overlapped				// буфер с перекрытием
		);


	[DllImport("kernel32", SetLastError=true)]
	static extern unsafe bool CloseHandle(
		IntPtr hObject   // дескриптор объекта
		);
	
	public bool Open(string FileName)
	{
		// открытие существующего файла для чтения
		
		handle = CreateFile(
			FileName,
			GENERIC_READ,
			0, 
			0, 
			OPEN_EXISTING,
			0,
			0);
	
		if (handle != IntPtr.Zero)
			return true;
		else
			return false;
	}

	public unsafe int Read(byte[] buffer, int index, int count) 
	{
		int n = 0;
		fixed (byte* p = buffer) 
		{
			if (!ReadFile(handle, p + index, count, &n, 0))
				return 0;
		}
		return n;
	}

	public bool Close()
	{
		//закрытие дескриптора файла
		return CloseHandle(handle);
	}
}

class Test
{
	public static int Main(string[] args)
	{
		if (args.Length != 1)
		{
			Console.WriteLine("Usage : ReadFile <FileName>");
			return 1;
		}
		
		if (! System.IO.File.Exists(args[0]))
		{
			Console.WriteLine("File " + args[0] + " not found."); 
			return 1;
		}

		byte[] buffer = new byte[128];
		FileReader fr = new FileReader();
		
		if (fr.Open(args[0]))
		{
			
			// Предполагается, что выполняется чтение файла ASCII
			ASCIIEncoding Encoding = new ASCIIEncoding();
			
			int bytesRead;
			do 
			{
				bytesRead = fr.Read(buffer, 0, buffer.Length);
				string content = Encoding.GetString(buffer,0,bytesRead);
				Console.Write("{0}", content);
			}
			while ( bytesRead > 0);
			
			fr.Close();
			return 0;
		}
		else
		{
			Console.WriteLine("Failed to open requested file");
			return 1;
		}
	}
}
