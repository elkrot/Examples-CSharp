// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Text;
using System.Data;
using System.IO;

namespace DataAnalysisExcel
{
    /// <summary>
    /// Этот класс конструирует текстовый файл в кодировке Юникод с табуляцией в качестве разделителей,
    /// содержащий данные из таблицы данных "Продажи". Сначала создается 
    /// папка с произвольным именем во временной папке текущего пользователя. 
    /// Затем в этой папке создается файл с именем data.txt. Еще один файл
    /// с именем schema.ini и с настройками для создания сводной таблицы создается в
    /// этой папке. Этот файл автоматически обнаруживается сводной таблицей благодаря
    /// общему доступу к папке с файлом данных.
    /// </summary>
    internal class TextFileGenerator : IDisposable
    {
        /// <summary>
        /// Полный путь к созданному временному файлу.
        /// </summary>
        private string fullPath;

        /// <summary>
        /// Полный путь к созданной папке верхнего уровня.
        /// </summary>
        private string rootPath;

        /// <summary>
        /// Поле для указания, что все папки и каталоги, созданные этим
        /// объектом, удалены, например, вызовом DeleteCreatedFiles().
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Полный путь к файлу data.txt.
        /// </summary>
        /// <value>Полный путь к файлу data.txt.</value>
        internal string FullPath
        {
            get
            {
                return fullPath;
            }
        }

        /// <summary>
        /// Конструктор. Создает временную папку, а также файлы data.txt и schema.ini.
        /// </summary>
        /// <param name="dt">Таблица "Продажи".</param>
        internal TextFileGenerator(DataTable dt)
        {
            string directoryName;
            string rootName;
            
            GenerateSecureTempFolder(out directoryName, out rootName);

            this.rootPath = rootName;
            this.fullPath = Path.Combine(directoryName, "data.txt");

            Encoding textEncoding;

            textEncoding = Encoding.Unicode;

            System.IO.Directory.CreateDirectory(directoryName);
            using (StreamWriter writer = new StreamWriter(this.fullPath, false, textEncoding, 512))
            {
                int remaining = dt.Columns.Count;

                foreach (DataColumn column in dt.Columns)
                {
                    writer.Write(QuoteString(column.ColumnName));

                    if (--remaining != 0)
                    {
                        writer.Write('\t');
                    }
                }

                writer.Write("\r\n");
                foreach (DataRow row in dt.Rows)
                {
                    int remainingItems = row.ItemArray.Length;

                    foreach (object item in row.ItemArray)
                    {
                        writer.Write(QuoteString(item.ToString()));

                        if (--remainingItems != 0)
                        {
                            writer.Write('\t');
                        }
                    }
                    writer.Write("\r\n");
                }
            }

            CreateSchemaIni();
        }

        ~TextFileGenerator()
        {
            InternalDispose();
        }

        /// <summary>
        /// Создает файл schema.ini для конфигурации сводной таблицы.
        /// </summary>
        private void CreateSchemaIni()
        {
            string contentsFormat = @"[{0}]
ColNameHeader=True
Format=TabDelimited
MaxScanRows=0
CharacterSet=Unicode
Col1=Date Char Width 255
Col2=Flavor Char Width 255
Col3=Inventory Integer
Col4=Sold Integer
Col5=Estimated Float
Col6=Recommendation Char Width 255
Col7=Profit Float
";
            string fileName = Path.Combine(Path.GetDirectoryName(this.fullPath), "schema.ini");

            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Default, contentsFormat.Length + this.fullPath.Length))
            {
                writer.Write(contentsFormat, Path.GetFileName(this.fullPath));
            }
        }

        /// <summary>
        // Реализуйте IDisposable.
        // Не делайте этот метод виртуальным.
        // Производный класс не должен иметь возможность переопределять этот метод.
        public void Dispose()
        {
            InternalDispose();

            // Этот объект будет очищен методом Dispose.
            // Поэтому следует вызвать GC.SupressFinalize, чтобы
            // убрать этот объект из очереди финализации и 
            // и запретить выполнение кода финализации для этого объекта
            // второй раз.
            GC.SuppressFinalize(this);
        }

        private void InternalDispose()
        {
            // Проверьте, не был ли уже вызван метод Dispose.
            if (!this.disposed)
            {
                // Вызовите соответствующие методы для очистки 
                // неуправляемых ресурсов здесь.
                // Если параметр удаления имеет значение False, 
                // выполняется только следующий код.
                DeleteCreatedFiles();
            }
            disposed = true;
        }

        /// Удаляет созданную папку и ее содержимое.
        /// </summary>
        private void DeleteCreatedFiles()
        {
            if (this.rootPath != null)
            {
                System.IO.Directory.Delete(rootPath, true);
                this.rootPath = null;
            }
        }

        /// <summary>
        /// Вспомогательный метод для создания безопасных имен файлов
        /// структуры каталога.
        /// </summary>
        /// <param name="createdFolder">
        /// Случайный безопасный путь.
        /// </param>
        /// <param name="createdRoot">
        /// Создан путь верхнего уровня. 
        /// </param>
        private static void GenerateSecureTempFolder(out string createdFolder, out string createdRoot)
        {
            string directoryName = Path.Combine(Path.GetTempPath(), GenerateRandomName());
            createdRoot = directoryName;
            directoryName = Path.Combine(directoryName, GenerateRandomName());
            createdFolder = directoryName;
        }

        /// <summary>
        /// Вспомогательный метод для создания случайного 
        /// структуры каталога.
        /// </summary>
        /// <returns>Случайное безопасное имя каталога.</returns>
        private static string GenerateRandomName()
        {
            byte[] data = new byte[9];
            StringBuilder randomString;

            // Получите несколько случайных байтов.
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(data);

            // Преобразуйте байты в строку. Будет создана строка из 12 знаков.
            randomString = new StringBuilder(System.Convert.ToBase64String(data));

            // Преобразуйте к формату имени файла "8.3"
            randomString[8] = '.';

            // Замените любые недопустимые знаки в имени файла.
            randomString = randomString.Replace('/', '-');
            randomString = randomString.Replace('+', '_');

            // Возвратите строку.
            return randomString.ToString();
        }

        /// <summary>
        /// Заключите строку в кавычки ("). Любые кавычки внутри строки удваиваются.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string QuoteString(string s)
        {
            StringBuilder sb = new StringBuilder("\"", s.Length + 2);

            sb.Append(s.Replace("\"", "\"\""));
            sb.Append('"');
            return sb.ToString();
        }
    }
}
