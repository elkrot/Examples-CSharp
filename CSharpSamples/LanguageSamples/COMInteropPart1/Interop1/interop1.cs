// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// interop1.cs
// Построение с использованием "csc /R:QuartzTypeLib.dll interop1.cs"
using System;
class MainClass 
{ 
	/************************************************************ 	Аннотация. Этот метод находит имя AVI-файла для воспроизведения,	а затем создает экземпляр COM-объекта Quartz.	Чтобы воспроизвести AVI-файл, программа вызывает методы RenderFile и Run из 	IMediaControl. Quartz использует свои собственные поток и окно для воспроизведения 	AVI-файла. Главный поток заблокирован на ReadLine до тех пор, пока пользователь не нажмет клавишу	ВВОД.		Входные параметры: место размещения avi-файла, предназначенного для воспроизведения.		Возвращает: void	**************************************************************/ 
	public static void Main(string[] args) 
	{ 
		// Проверка передачи пользователем имени файла 
		if (args.Length != 1)
		{ 
			DisplayUsage();
			return;
		} 

		if (args[0] == "/?")
		{ 
			DisplayUsage();
			return;
		} 

		string filename = args[0]; 

		// Проверка наличия файла
		if (!System.IO.File.Exists(filename))
		{
			Console.WriteLine("File " + filename + " not found.");
			DisplayUsage();
			return;
		}
    
		// Создание экземпляра Quartz
		// (Calls CoCreateInstance(E436EBB3-524F-11CE-9F53-0020AF0BA770,
		// NULL, CLSCTX_ALL, IID_IUnknown, &graphManager).): 

		try
		{
			QuartzTypeLib.FilgraphManager graphManager = 
				new QuartzTypeLib.FilgraphManager();

			// QueryInterface для интерфейса IMediaControl:
			QuartzTypeLib.IMediaControl mc =
				(QuartzTypeLib.IMediaControl)graphManager;

			// Вызов некоторых методов COM-интерфейса 
			// Передача файла в метод RenderFile COM-объекта. 
			mc.RenderFile(filename);

			// Показать файл. 
			mc.Run();
		}
		catch(Exception ex)
		{
			Console.WriteLine("Unexpected COM exception: " + ex.Message);
		}

		// Дождитесь завершения.
		Console.WriteLine("Press Enter to continue."); 
		Console.ReadLine();
	}
    
	private static void DisplayUsage() 
	{ 
		// Пользователь  предоставил недостаточно параметров. 
		// Показать использование: 
		Console.WriteLine("VideoPlayer: Plays AVI files."); 
		Console.WriteLine("Usage: VIDEOPLAYER.EXE filename"); 
		Console.WriteLine("where filename is the full path and"); 
		Console.WriteLine("file name of the AVI to display."); 
	} 
}

