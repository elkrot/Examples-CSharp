// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// indexer.cs
// аргументы: indexer.txt
using System;
using System.IO;

// Класс, предоставляющий доступ к большому файлу,
// как к массиву байтов.
public class FileByteArray
{
    Stream stream;      // Фиксация базового потока,
                        // используемого для доступа к файлу.
// Создание нового экземпляра FileByteArray, инкапсулирующего конкретный файл.
    public FileByteArray(string fileName)
    {
        stream = new FileStream(fileName, FileMode.Open);
    }

    // Закрытие потока. Эта операция должна быть последней
    // при завершении задачи.
    public void Close()
    {
        stream.Close();
        stream = null;
    }

    // Индексатор, обеспечивающий доступ к файлу для чтения и записи.
    public byte this[long index]   // long -- это 64-разрядное целое число
    {
        // Чтение одного байта в позиции, равной индексу смещения, и его возврат.
        get 
        {
            byte[] buffer = new byte[1];
            stream.Seek(index, SeekOrigin.Begin);
            stream.Read(buffer, 0, 1);
            return buffer[0];
        }
        // Запись одного байта в позицию, равную индексу смещения, и его возврат.
        set 
        {
            byte[] buffer = new byte[1] {value};
            stream.Seek(index, SeekOrigin.Begin);
            stream.Write(buffer, 0, 1);
        }
    }

    // Получение общей длины файла.
    public long Length 
    {
        get 
        {
            return stream.Seek(0, SeekOrigin.End);
        }
    }
}

// Демонстрация класса FileByteArray.
// Замена прямого порядка следования байтов файла на обратный.
public class Reverse 
{
    public static void Main(String[] args) 
    {
		// Проверка аргументов.
		if (args.Length != 1)
		{
			Console.WriteLine("Usage : Indexer <filename>");
			return;
		}

		// Проверка существования файла
		if (!System.IO.File.Exists(args[0]))
		{
			Console.WriteLine("File " + args[0] + " not found.");
			return;
		}

		FileByteArray file = new FileByteArray(args[0]);
		long len = file.Length;

		// Байты файла меняются местами, чтобы порядок их следования стал обратным.
		for (long i = 0; i < len / 2; ++i) 
		{
			byte t;

			// Следует заметить, что при индексации переменной "file" вызывается
			// индексатор класса FileByteStream, который  считывает
			// и записывает байты в файл.
			t = file[i];
			file[i] = file[len - i - 1];
			file[len - i - 1] = t;
		}

		file.Close();
    } 
}

