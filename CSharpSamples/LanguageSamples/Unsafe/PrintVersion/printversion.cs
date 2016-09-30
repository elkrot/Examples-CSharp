// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// printversion.cs
// компилировать с /unsafe
using System;
using System.Reflection;
using System.Runtime.InteropServices;

// Присвоение данной сборке номера версии:
[assembly:AssemblyVersion("4.3.2.1")]

public class Win32Imports 
{
	[DllImport("version.dll")]
	public static extern bool GetFileVersionInfo (string sFileName,
		int handle, int size, byte[] infoBuffer);
	[DllImport("version.dll")]
	public static extern int GetFileVersionInfoSize (string sFileName,
		out int handle);
   
	// Для 3-го параметра -- "out string pValue" -- автоматически выполняется
	//  маршалирование из Ansi в Юникод:
	[DllImport("version.dll")]
	unsafe public static extern bool VerQueryValue (byte[] pBlock,
		string pSubBlock, out string pValue, out uint len);
	// Эта перегрузка VerQueryValue помечается как "unsafe", поскольку 
	// она использует ближние указатели (short*):
	[DllImport("version.dll")]
	unsafe public static extern bool VerQueryValue (byte[] pBlock,
		string pSubBlock, out short *pValue, out uint len);
}

public class C 
{
	// Метод main помечается как "unsafe", поскольку он использует указатели:
	unsafe public static int Main () 
	{
		try 
		{
			int handle = 0;
			// Определение объема сведений о версии:
			int size =
				Win32Imports.GetFileVersionInfoSize("printversion.exe",
				out handle);

			if (size == 0) return -1;

			byte[] buffer = new byte[size];

			if (!Win32Imports.GetFileVersionInfo("printversion.exe", handle, size, buffer))
			{
				Console.WriteLine("Failed to query file version information.");
				return 1;
			}

			short *subBlock = null;
			uint len = 0;
			// Получение данных о  языковом стандарте из сведений о версии:
			if (!Win32Imports.VerQueryValue (buffer, @"\VarFileInfo\Translation", out subBlock, out len))
			{
				Console.WriteLine("Failed to query version information.");
				return 1;
			}

			string spv = @"\StringFileInfo\" + subBlock[0].ToString("X4") + subBlock[1].ToString("X4") + @"\ProductVersion";

			byte *pVersion = null;
			// Получение значения ProductVersion для данной программы:
			string versionInfo;
			
			if (!Win32Imports.VerQueryValue (buffer, spv, out versionInfo, out len))
			{
				Console.WriteLine("Failed to query version information.");
				return 1;
			}

			Console.WriteLine ("ProductVersion == {0}", versionInfo);
		}
		catch (Exception e) 
		{
			Console.WriteLine ("Caught unexpected exception " + e.Message);
		}
      
		return 0;
	}
}

