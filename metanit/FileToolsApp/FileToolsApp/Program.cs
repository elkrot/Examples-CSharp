using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace FileToolsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Название: {0}", drive.Name);
                Console.WriteLine("Тип: {0}", drive.DriveType);
                if (drive.IsReady)
                {
                    Console.WriteLine("Объем диска: {0}", drive.TotalSize);
                    Console.WriteLine("Свободное пространство: {0}", drive.TotalFreeSpace);
                    Console.WriteLine("Метка: {0}", drive.VolumeLabel);
                }
                Console.WriteLine();
            }



            #region Получение списка файлов и подкаталогов
            string dirName = "C:\\";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }
            }
            #endregion

            #region Создание каталога
            string path = @"C:\SomeDir";
            string subpath = @"program\avalon";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);
            #endregion

            #region Получение информации о каталоге
            string dirName2 = "C:\\Program Files";

            DirectoryInfo dirInfo2 = new DirectoryInfo(dirName2);

            Console.WriteLine("Название каталога: {0}", dirInfo2.Name);
            Console.WriteLine("Полное название каталога: {0}", dirInfo2.FullName);
            Console.WriteLine("Время создания каталога: {0}", dirInfo2.CreationTime);
            Console.WriteLine("Корневой каталог: {0}", dirInfo2.Root);
            #endregion

            #region Удаление каталога
            string dirName3 = @"C:\SomeFolder";

            try
            {
                DirectoryInfo dirInfo3 = new DirectoryInfo(dirName3);
                dirInfo3.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region Перемещение каталога
            string oldPath = @"C:\SomeFolder";
            string newPath = @"C:\SomeDir";
            DirectoryInfo dirInfo4 = new DirectoryInfo(oldPath);
            if (dirInfo4.Exists && Directory.Exists(newPath) == false)
            {
                dirInfo4.MoveTo(newPath);
            }
            #endregion

            #region Получение информации о файле
            string path2 = @"C:\apache\hta.txt";
            FileInfo fileInf = new FileInfo(path2);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
            #endregion

            #region Удаление файла
            string path6 = @"C:\apache\hta.txt";
            FileInfo fileInf6 = new FileInfo(path6);
            if (fileInf.Exists)
            {
                fileInf6.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
            #endregion

            #region Перемещение файла
            string path3 = @"C:\apache\hta.txt";
            string newPath3 = @"C:\SomeDir\hta.txt";
            FileInfo fileInf3 = new FileInfo(path3);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(newPath3);
                // альтернатива с помощью класса File
                // File.Move(path, newPath);
            }
            #endregion

            #region Копирование файла
            string path5 = @"C:\apache\hta.txt";
            string newPath5 = @"C:\SomeDir\hta.txt";
            FileInfo fileInf5 = new FileInfo(path5);
            if (fileInf5.Exists)
            {
                fileInf5.CopyTo(newPath5, true);
                // альтернатива с помощью класса File
                // File.Copy(path, newPath, true);
            }
            #endregion

            ReadFromFile(@"C:\SomeDir\hta.txt");

            WriteFile();

            ReadBinaryFromFile();

            ZipUnzip();

             BinarySerialize();

             BinarySerialize2();

             SOAPSerialize();

            XMLSerialize();

            XMLSerialize2();

            XMLSerialize3();

            JsonSerialize();

            JsonSerialize2();

            Console.ReadKey();
        }

        #region ReadFromFile
        private static void ReadFromFile(string path)
        {


            try
            {
                Console.WriteLine("******считываем весь файл********");
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }

                Console.WriteLine();
                Console.WriteLine("******считываем построчно********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("******считываем блоками********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    char[] array = new char[4];
                    // считываем 4 символа
                    sr.Read(array, 0, 4);

                    Console.WriteLine(array);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        #endregion

        #region WriteFile
        public static void WriteFile()
        {
            string readPath = @"C:\SomeDir\hta.txt";
            string writePath = @"C:\SomeDir\ath.txt";

            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region ReadBinaryFromFile
        public static void ReadBinaryFromFile()
        {
            State[] states = new State[2];
            states[0] = new State("Германия", "Берлин", 357168, 80.8);
            states[1] = new State("Франция", "Париж", 640679, 64.7);

            string path = @"C:\SomeDir\states.dat";

            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    // записываем в файл значение каждого поля структуры
                    foreach (State s in states)
                    {
                        writer.Write(s.name);
                        writer.Write(s.capital);
                        writer.Write(s.area);
                        writer.Write(s.people);
                    }
                }
                // создаем объект BinaryReader
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        string capital = reader.ReadString();
                        int area = reader.ReadInt32();
                        double population = reader.ReadDouble();

                        Console.WriteLine("Страна: {0}  столица: {1}  площадь {2} кв. км   численность населения: {3} млн. чел.",
                            name, capital, area, population);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region ZipReader


        public static void ZipUnzip() {
            string sourceFile = "D://test/book.pdf"; // исходный файл
            string compressedFile = "D://test/book.gz"; // сжатый файл
            string targetFile = "D://test/book_new.pdf"; // восстановленный файл

            // создание сжатого файла
            Compress(sourceFile, compressedFile);
            // чтение из сжатого файла
            Decompress(compressedFile, targetFile);
        }
        public static void Compress(string sourceFile, string compressedFile)
    {
        // поток для чтения исходного файла
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
        {
            // поток для записи сжатого файла
            using (FileStream targetStream = File.Create(compressedFile))
            {
                // поток архивации
               /* using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                    Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                        sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                }*/
            }
        }
    }
 
    public static void Decompress(string compressedFile, string targetFile)
    {
        // поток для чтения из сжатого файла
        using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
        {
            // поток для записи восстановленного файла
            using (FileStream targetStream = File.Create(targetFile))
            {
                // поток разархивации
               /* using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(targetStream);
                    Console.WriteLine("Восстановлен файл: {0}", targetFile);
                }*/
            }
        }
    }

        #endregion

        #region BinarySerialize
    

    public static void BinarySerialize2()
    {
        Person person1 = new Person("Tom", 29);
        Person person2 = new Person("Bill", 25);
        // массив для сериализации
        Person[] people = new Person[] { person1, person2 };

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
        {
            // сериализуем весь массив people
            formatter.Serialize(fs, people);

            Console.WriteLine("Объект сериализован");
        }

        // десериализация
        using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
        {
            Person[] deserilizePeople = (Person[])formatter.Deserialize(fs);

            foreach (Person p in deserilizePeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
            }
        }
    }
    public static void BinarySerialize()
    {
        // объект для сериализации
        Person person = new Person("Tom", 29);
        Console.WriteLine("Объект создан");

        // создаем объект BinaryFormatter
        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, person);

            Console.WriteLine("Объект сериализован");
        }

        // десериализация из файла people.dat
        using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
        {
            Person newPerson = (Person)formatter.Deserialize(fs);

            Console.WriteLine("Объект десериализован");
            Console.WriteLine("Имя: {0} --- Возраст: {1}", newPerson.Name, newPerson.Age);
        }
    }
    #endregion

        #region SOAPSerialize
    public  static void SOAPSerialize(){
        Person person = new Person("Tom", 29);
        Person person2 = new Person("Bill", 25);
        Person[] people = new Person[] { person, person2 };

        // создаем объект SoapFormatter
        SoapFormatter formatter = new SoapFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, people);

            Console.WriteLine("Объект сериализован");
        }

        // десериализация
        using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
        {
            Person[] newPeople = (Person[])formatter.Deserialize(fs);

            Console.WriteLine("Объект десериализован");
            foreach (Person p in newPeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
            }
        }
    }
    #endregion

        #region XMLSerialize
    public static void XMLSerialize3()
    {
        Person person1 = new Person("Tom", 29, new Company("Microsoft"));
        Person person2 = new Person("Bill", 25, new Company("Apple"));
        Person[] people = new Person[] { person1, person2 };

        XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, people);
        }

        using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
        {
            Person[] newpeople = (Person[])formatter.Deserialize(fs);

            foreach (Person p in newpeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1} --- Компания: {2}", p.Name, p.Age, p.Company.Name);
            }
        }
    }
    public static void XMLSerialize2() {
        Person person1 = new Person("Tom", 29);
        Person person2 = new Person("Bill", 25);
        Person[] people = new Person[] { person1, person2 };

        XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, people);
        }

        using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
        {
            Person[] newpeople = (Person[])formatter.Deserialize(fs);

            foreach (Person p in newpeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
            }
        }
    }

    public static void XMLSerialize() {
        // объект для сериализации
        Person person = new Person("Tom", 29);
        Console.WriteLine("Объект создан");

        // передаем в конструктор тип класса
        XmlSerializer formatter = new XmlSerializer(typeof(Person));

        // получаем поток, куда будем записывать сериализованный объект
        using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, person);

            Console.WriteLine("Объект сериализован");
        }

        // десериализация
        using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
        {
            Person newPerson = (Person)formatter.Deserialize(fs);

            Console.WriteLine("Объект десериализован");
            Console.WriteLine("Имя: {0} --- Возраст: {1}", newPerson.Name, newPerson.Age);
        }
    }
    #endregion

        #region JsonSerialize
    public static void JsonSerialize2() {
        Person person1 = new Person("Tom", 29, new Company("Microsoft"));
        Person person2 = new Person("Bill", 25, new Company("Apple"));
        Person[] people = new Person[] { person1, person2 };

        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, people);
        }

        using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
        {
            Person[] newpeople = (Person[])jsonFormatter.ReadObject(fs);

            foreach (Person p in newpeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1} --- Компания: {2}", p.Name, p.Age, p.Company.Name);
            }
        }
    }
    public static void JsonSerialize() {
        // объект для сериализации
        Person person1 = new Person("Tom", 29);
        Person person2 = new Person("Bill", 25);
        Person[] people = new Person[] { person1, person2 };

        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Person[]));

        using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, people);
        }

        using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
        {
            Person[] newpeople = (Person[])jsonFormatter.ReadObject(fs);

            foreach (Person p in newpeople)
            {
                Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
            }
        }
    }
    #endregion



    #region struct State
    struct State
        {
            public string name;
            public string capital;
            public int area;
            public double people;

            public State(string n, string c, int a, double p)
            {
                name = n;
                capital = c;
                people = p;
                area = a;
            }
        }
    #endregion
    
    }
}
