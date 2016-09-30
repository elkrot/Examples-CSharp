// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleLinqToObjects
{
    class SimpleLinqToObjects
    {

        /// <summary>
        /// Этот пример иллюстрирует суть программы
        /// LINQ to Objects. Обратите внимание, что строка "using System.Linq" включена в
        /// конструкцию using. В код включены следующие примеры:
        ///     
        ///     * Инициализатор коллекции
        ///     * Выражение запроса
        ///     * Определение типа        
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            // Инициализатор коллекции C# 3.0
            List<int> numberList = new List<int> { 1, 2, 3, 4 };

            // ключевое слово var в этом выражении запроса иллюстрирует
            // способ использования определения типа. Установите курсор на слове
            // var чтобы увидеть тип идентификатора, вызвавшего запрос.
            var query = from i in numberList
                        where i < 4
                        select i;

            // Итерация по элементам типа IEnumerable, возвращенного запросом.
            foreach (var number in query)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
        }
    }
}
