// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;   
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;

// Версия Mad01

namespace SampleQueries {
    [Title("Примеры запросов к компоненту 101 LINQ")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness 
    {        
        private readonly static string dataPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\"));
        
        # region Sample Data Collections

        // Объекты примера, основанные на данных Northwnd, 
        // но не идентичные им во всех случаях.

        public class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public Order[] Orders { get; set; }
        }

        public class Order
        {
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal Total { get; set; }
        }

        public class Product
        {
            public int ProductID { get; set; } 
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
        }

        public class Supplier
        {
            public string SupplierName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
        
        private List<Product> productList;
        private List<Customer> customerList;
        private List<Supplier> supplierList;

        #endregion

        [Category("Операторы ограничений")]
        [Title("Where - Простой 1")]
        [Description("В этом примере предложение where используется для поиска всех элементов массива со значением, меньшим 5.")]
        public void Linq1() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;
        
            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums) {
                Console.WriteLine(x);
            }
        }
        
        [Category("Операторы ограничений")]
        [Title("Where - Простой 2")]
        [Description("В этом примере предложение where используется для поиска всех продуктов, которых нет на складе.")]
        public void Linq2() {
            List<Product> products = GetProductList();

            var soldOutProducts =
                from prod in products
                where prod.UnitsInStock == 0
                select prod;
            
            Console.WriteLine("Sold out products:");
            foreach (var product in soldOutProducts) {
                Console.WriteLine("{0} is sold out!", product.ProductName);
            }
        }
        
        [Category("Операторы ограничений")]
        [Title("Where - Простой 3")]
        [Description("В этом примере предложение where используется для поиска всех продуктов, находящихся на складе и " +
                     "с ценой более 3,00 за единицу.")]
        public void Linq3() {
            List<Product> products = GetProductList();

            var expensiveInStockProducts =
                from prod in products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M
                select prod;
            
            Console.WriteLine("In-stock products that cost more than 3.00:");
            foreach (var product in expensiveInStockProducts) {
                Console.WriteLine("{0} is in stock and costs more than 3.00.", product.ProductName);
            }
        }
        
        [Category("Операторы ограничений")]
        [Title("Where - углубленная детализация")]
        [Description("В этом примере предложение where используется для поиска всех клиентов в Вашингтоне, " +
                     "а затем в нем используется цикл foreach для итерации по коллекции заказов, относящейся к каждому клиенту.")]
        public void Linq4() {
            List<Customer> customers = GetCustomerList();

            var waCustomers =
                from cust in customers
                where cust.Region == "WA"
                select cust;

            Console.WriteLine("Customers from Washington and their orders:");
            foreach (var customer in waCustomers)
            {
                Console.WriteLine("Customer {0}: {1}", customer.CustomerID, customer.CompanyName);
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("  Order {0}: {1}", order.OrderID, order.OrderDate);
                }
            }
        }
        
        [Category("Операторы ограничений")]
        [Title("Where - индексированное")]
        [Description("В этом примере показано индексированное предложение where, возвращающее цифры, имена которых " +
                    "короче своего значения.")]
        public void Linq5() {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);
        
            Console.WriteLine("Short digits:");
            foreach (var d in shortDigits)
            {
                Console.WriteLine("The word {0} is shorter than its value.", d);
            }
        }

        [Category("Операторы проекции")]
        [Title("Select - простой 1")]
        [Description("В этом примере используется предложение select для получения последовательности целых чисел, на единицу превышающих " +
                     "целые числа в существующем массиве.")]
        public void Linq6() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numsPlusOne =
                from num in numbers
                select num + 1;
            
            Console.WriteLine("Numbers + 1:");
            foreach (var i in numsPlusOne) {
                Console.WriteLine(i);
            }
        } 
        
        [Category("Операторы проекции")]
        [Title("Select - простой 2")]
        [Description("В этом примере предложение select используется для возвращения последовательности имен продуктов.")]
        public void Linq7() {
            List<Product> products = GetProductList();

            var productNames =
                from prod in products
                select prod.ProductName;
            
            Console.WriteLine("Product Names:");
            foreach (var productName in productNames) {
                Console.WriteLine(productName);
            }
        }

        [Category("Операторы проекции")]
        [Title("Select - преобразование")]
        [Description("В этом примере предложение select используется для создания последовательности строк, представляющих " +
                     "текстовый вариант последовательности целых чисел.")]
        public void Linq8() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var textNums = 
                from num in numbers
                select strings[num];
            
            Console.WriteLine("Number strings:");
            foreach (var str in textNums) {
                Console.WriteLine(str);
            }           
        }

        [Category("Операторы проекции")]
        [Title("Select - анонимные типы 1")]
        [Description("В этом примере предложение select используется для создания последовательности символов верхнего регистра " +
                     "и в нижнем регистре каждого слова в исходном массиве.")]
        public void Linq9() {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var upperLowerWords =
                from word in words
                select new {Upper = word.ToUpper(), Lower = word.ToLower()};

            foreach (var wordPair in upperLowerWords) {
                Console.WriteLine("Uppercase: {0}, Lowercase: {1}", wordPair.Upper, wordPair.Lower);
            }
        }

        [Category("Операторы проекции")]
        [Title("Select - анонимные типы 2")]
        [Description("В этом примере предложение select используется для создания последовательности, содержащей текстовое представление " +
                     "цифр и значение типа Boolean, которое указывает, состоит текст из четного или нечетного числа символов.")]
        public void Linq10() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var digitOddEvens =
                from num in numbers
                select new {Digit = strings[num], Even = (num % 2 == 0)};

            foreach (var digit in digitOddEvens) {
                Console.WriteLine("The digit {0} is {1}.", digit.Digit, digit.Even ? "even" : "odd");
            }
        }
        
        [Category("Операторы проекции")]
        [Title("Select - анонимные типы 3")]
        [Description("В этом примере предложение select используется для создания последовательности, содержащей некоторые свойства " +
                     "продуктов, включая значение UnitPrice, переименованное в Price " +
                     "в результирующем типе.")]
        public void Linq11() {
            List<Product> products = GetProductList();

            var productInfos =
                from prod in products
                select new {prod.ProductName, prod.Category, Price = prod.UnitPrice};
            
            Console.WriteLine("Product Info:");
            foreach (var productInfo in productInfos) {
                Console.WriteLine("{0} is in the category {1} and costs {2} per unit.", productInfo.ProductName, productInfo.Category, productInfo.Price);
            }
        } 

        [Category("Операторы проекции")]
        [Title("Select - с индексом")]
        [Description("Образец использования индексированного предложения Select для определения соответствия значения целых чисел " +
                     "в массиве их позиции в этом массиве.")]
        public void Linq12() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numsInPlace = numbers.Select((num, index) => new {Num = num, InPlace = (num == index)});
        
            Console.WriteLine("Number: In-place?");
            foreach (var n in numsInPlace) {
                Console.WriteLine("{0}: {1}", n.Num, n.InPlace);
            }
        }

        [Category("Операторы проекции")]
        [Title("Select - с фильтром")]
        [Description("В этом примере объединяются предложения Select и Where, чтобы создать простой запрос, который возвращает " +
                     "текстовое представление каждой цифры, меньшей 5.")]
        public void Linq13() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var lowNums =
                from num in numbers
                where num < 5
                select digits[num];
            
            Console.WriteLine("Numbers < 5:");
            foreach (var num in lowNums) {
                Console.WriteLine(num);
            }       
        }
        
        [Category("Операторы проекции")]
        [Title("SelectMany - составное предложение From 1")]
        [Description("В этом примере используется составное предложение From, чтобы создать запрос, возвращающий все пары " +
                     "чисел из обоих массивов, в которой число из массива numbersA меньше числа " +
                     "из numbersB.")]
        public void Linq14() {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs =
                from a in numbersA
                from b in numbersB
                where a < b
                select new {a, b};
        
            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs) {
                Console.WriteLine("{0} is less than {1}", pair.a, pair.b);
            }
        }
        
        [Category("Операторы проекции")]
        [Title("SelectMany - составное предложение From 2")]
        [Description("Образец использования составного предложения From для выбора всех заказов, в которых " +
                     "сумма заказа меньше 500,00.")]
        public void Linq15() {
            List<Customer> customers = GetCustomerList();

            var orders =
                from cust in customers
                from order in cust.Orders
                    where order.Total < 500.00M
                    select new {cust.CustomerID, order.OrderID, order.Total};
        
            ObjectDumper.Write(orders);
        }

        [Category("Операторы проекции")]
        [Title("SelectMany - составное предложение From 3")]
        [Description("Образец использования составного предложения From для выбора всех заказов, в которых " +
                     "заказ сделан в 1998 г. или позднее.")]
        public void Linq16() {
            List<Customer> customers = GetCustomerList();

            var orders =
                from cust in customers
                from order in cust.Orders
                    where order.OrderDate >= new DateTime(1998, 1, 1)
                    select new {cust.CustomerID, order.OrderID, order.OrderDate};
        
            ObjectDumper.Write(orders);
        }

        [Category("Операторы проекции")]
        [Title("SelectMany - с")]
        [Description("Образец использования составного предложения From для выбора всех заказов, в которых " +
                     "общей суммой выданных заказов, превышающей 2000,00, и использует предложение let, чтобы избежать " +
                     "повторного запроса итога.")] 
        public void Linq17() {
            List<Customer> customers = GetCustomerList();

            var orders =
                from cust in customers
                from order in cust.Orders
                let total = order.Total
                where total >= 2000.0M
                select new {cust.CustomerID, order.OrderID, total};

            ObjectDumper.Write(orders);
        }
    
        [Category("Операторы проекции")]
        [Title("SelectMany - составное предложение From")]
        [Description("В этом примере предложение From используются так, чтобы фильтрация по клиентам могла " +
                     "быть выполнена до выбора их заказов.  Это делает запрос более эффективным, поскольку не требуются " +
                     "не выбираются, а затем отбрасываются заказы для клиентов не из Вашингтона.")]
        public void Linq18() {
            List<Customer> customers = GetCustomerList();

            DateTime cutoffDate = new DateTime(1997, 1, 1);

            var orders =
                from cust in customers
                where cust.Region == "WA"
                from order in cust.Orders
                    where order.OrderDate >= cutoffDate
                    select new {cust.CustomerID, order.OrderID};
        
            ObjectDumper.Write(orders);
        }
        
        [Category("Операторы проекции")]
        [Title("SelectMany - индексированное")]
        [Description("Образец использования индексированного предложения SelectMany для выбора всех заказов, " +
                     "ссылаясь на клиентов по заказам, в которых они возвращаются " +
                     "из запроса.")]
        public void Linq19() {
            List<Customer> customers = GetCustomerList();

            var customerOrders =
                customers.SelectMany(
                    (cust, custIndex) =>
                    cust.Orders.Select(o => "Customer #" + (custIndex + 1) +
                                            " has an order with OrderID " + o.OrderID));

            ObjectDumper.Write(customerOrders);
        }
        
        [Category("Операторы секционирования")]
        [Title("Оператор Take - простой")]
        [Description("Образец использования Take для получения только первых 3-х элементов " +
                     "массива.")]
        public void Linq20() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var first3Numbers = numbers.Take(3);
            
            Console.WriteLine("First 3 numbers:");
            foreach (var n in first3Numbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы секционирования")]
        [Title("Оператор Take - вложенный")]
        [Description("Образец использования Take для получения первых 3-х заказов от клиентов " +
                     "в Вашингтоне.")]
        public void Linq21() {
            List<Customer> customers = GetCustomerList();

            var first3WAOrders = (
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new {cust.CustomerID, order.OrderID, order.OrderDate} )
                .Take(3);
            
            Console.WriteLine("First 3 orders in WA:");
            foreach (var order in first3WAOrders) {
                ObjectDumper.Write(order);
            }
        }

        [Category("Операторы секционирования")]
        [Title("Skip - простое")]
        [Description("В этом примере предложение Skip используется для получения всех, кроме первых четырех элементов " +
                     "массива.")]
        public void Linq22() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var allButFirst4Numbers = numbers.Skip(4);
            
            Console.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4Numbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы секционирования")]
        [Title("Skip - вложенное")]
        [Description("Образец использования Take для получения всех, кроме первых 2-х заказов от клиентов " +
                     "в Вашингтоне.")]
        public void Linq23() {
            List<Customer> customers = GetCustomerList();

            var waOrders =
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new {cust.CustomerID, order.OrderID, order.OrderDate};
        
            var allButFirst2Orders = waOrders.Skip(2);
            
            Console.WriteLine("All but first 2 orders in WA:");
            foreach (var order in allButFirst2Orders) {
                ObjectDumper.Write(order);
            }
        }    
            
        [Category("Операторы секционирования")]
        [Title("TakeWhile - простой")]
        [Description("Образец использования TakeWhile для возврата элементов с " +
                     "начала массива до числа, значение которого не меньше 6.")]
        public void Linq24() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);
            
            Console.WriteLine("First numbers less than 6:");
            foreach (var num in firstNumbersLessThan6) {
                Console.WriteLine(num);
            }
        }
        
        [Category("Операторы секционирования")]
        [Title("TakeWhile - индексированный")]
        [Description("Образец использования TakeWhile для возврата элементов с " +
                    "начала массива, пока не будет найдено число, меньшее, чем его позиция " +
                    "в массиве.")]
        public void Linq25() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);
            
            Console.WriteLine("First numbers not less than their position:");
            foreach (var n in firstSmallNumbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы секционирования")]
        [Title("SkipWhile - простой")]
        [Description("Образец использования SkipWhile для получения элементов массива, " +
                    "начиная с первого элемента, кратного 3.")]
        public void Linq26() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            // В лямбда-выражении, "n" - это входной параметр, который определяет каждый
            // элемент подряд в коллекции. Предполагается, что это - значение
            // типа int, поскольку числа находятся в массиве типа int.
            var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0);
            
            Console.WriteLine("All elements starting from first element divisible by 3:");
            foreach (var n in allButFirst3Numbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы секционирования")]
        [Title("SkipWhile - индексированный")]
        [Description("Образец использования SkipWhile для получения элементов массива, " +
                    "начиная с первого элемента, меньшего своей позиции.")]
        public void Linq27() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var laterNumbers = numbers.SkipWhile((n, index) => n >= index);
            
            Console.WriteLine("All elements starting from first element less than its position:");
            foreach (var n in laterNumbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы упорядочения")]
        [Title("OrderBy - простой 1")]
        [Description("Пример использования orderby для сортировки списка слов в алфавитном порядке.")]
        public void Linq28() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            var sortedWords =
                from word in words
                orderby word
                select word;
            
            Console.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords) {
                Console.WriteLine(w);
            }
        }

        [Category("Операторы упорядочения")]
        [Title("OrderBy - простой 2")]
        [Description("Пример использования orderby для сортировки списка слов по длине.")]
        public void Linq29() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            var sortedWords =
                from word in words
                orderby word.Length
                select word;
            
            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords) {
                Console.WriteLine(w);
            }
        }
        
        [Category("Операторы упорядочения")]
        [Title("OrderBy - простой 3")]
        [Description("Пример использования orderby для сортировки списка продуктов по названию. " +
                    "Ключевое слово \"descending\" в конце предложения используется для выполнения обратного упорядочения.")]
        public void Linq30() {
            List<Product> products = GetProductList();

            var sortedProducts =
                from prod in products
                orderby prod.ProductName
                select prod;
        
            ObjectDumper.Write(sortedProducts);
        }

        // Пользовательское средство сравнения для использования в операторах упорядочения
        public class CaseInsensitiveComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }
        }

        [Category("Операторы упорядочения")]
        [Title("OrderBy - сравнение")]
        [Description("Образец использования предложения OrderBy с пользовательским методом сравнения для " +
                     "сортировки слов в массиве без учета регистра.")]
        [LinkedClass("CaseInsensitiveComparer")]
        public void Linq31() {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};
            
            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());
                
            ObjectDumper.Write(sortedWords);
        }
        
        [Category("Операторы упорядочения")]
        [Title("OrderByDescending - простой 1")]
        [Description("Пример использования orderby и descending для сортировки списка " +
                     "чисел с типом double от наибольшего к наименьшему.")]
        public void Linq32() {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            
            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;
            
            Console.WriteLine("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles) {
                Console.WriteLine(d);
            }
        }
        
        [Category("Операторы упорядочения")]
        [Title("OrderByDescending - простой 2")]
        [Description("Пример использования orderby для сортировки списка продуктов по их числу на складе " +
                     "от наибольшего к наименьшему.")]
        public void Linq33() {
            List<Product> products = GetProductList();

            var sortedProducts =
                from prod in products
                orderby prod.UnitsInStock descending
                select prod;
        
            ObjectDumper.Write(sortedProducts);
        }

        [Category("Операторы упорядочения")]
        [Title("OrderByDescending - сравнение")]
        [Description("В этом примере используется синтаксис метода, чтобы вызвать OrderByDescending, поскольку " +
                    " он позволяет использовать пользовательское средство сравнения.")]
        [LinkedClass("CaseInsensitiveComparer")]
        public void Linq34() {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};
            
            var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());
                
            ObjectDumper.Write(sortedWords);
        }
        
        [Category("Операторы упорядочения")]
        [Title("ThenBy - простой")]
        [Description("Пример использования составного orderby для сортировки списка цифр, " +
                     "сначала по длине их имени, а затем в алфавитном порядке собственно по имени.")]
        public void Linq35() {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var sortedDigits =
                from digit in digits 
                orderby digit.Length, digit
                select digit;
        
            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits) {
                Console.WriteLine(d);
            }
        }

        [Category("Операторы упорядочения")]
        [Title("ThenBy - сравнение")]
        [Description("Первый запрос в этом примере использует синтаксис метода для вызова OrderBy и ThenBy с пользовательским средством сравнения для " +
                     "сортировки сначала по длине слова, а затем для сортировки слов в массиве без учета регистра. " +
                     "Вторые два запроса демонстрируют другой способ выполнения той же задачи.")]
        [LinkedClass("CaseInsensitiveComparer")]
        public void Linq36() {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};
            
            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenBy(a => a, new CaseInsensitiveComparer());

            // Другой способ. TODO: это - правильный способ использования ThenBy? Кажется, он работает на этом образце массива.
            var sortedWords2 =
                from word in words
                orderby word.Length
                select word;

            var sortedWords3 = sortedWords2.ThenBy(a => a, new CaseInsensitiveComparer());
                
            ObjectDumper.Write(sortedWords);

            ObjectDumper.Write(sortedWords3);
        }
        
        [Category("Операторы упорядочения")]
        [Title("ThenByDescending - простой")]
        [Description("Образец использования orderby для сортировки списка продуктов " +
                     "сначала по категориям, а затем по цене за единицу, от самой высокой до самой низкой.")]
        public void Linq37() {
            List<Product> products = GetProductList();

            var sortedProducts =
                from prod in products
                orderby prod.Category, prod.UnitPrice descending
                select prod;
        
            ObjectDumper.Write(sortedProducts);
        }

        [Category("Операторы упорядочения")]
        [Title("ThenByDescending - сравнение")]
        [Description("Образец использования OrderBy и ThenBy с пользовательским средством сравнения для " +
                     "сортировки сначала по длине слова, а затем для сортировки с учетом регистра по убыванию " +
                     "слов в массиве.")]
        [LinkedClass("CaseInsensitiveComparer")]
        public void Linq38() {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};
            
            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenByDescending(a => a, new CaseInsensitiveComparer());
                
            ObjectDumper.Write(sortedWords);
        }
        
        [Category("Операторы упорядочения")]
        [Title("Reverse")]
        [Description("Образец использования Reverse для создания списка всех цифр в массиве, у которых " +
                     "вторая буква \"i\", в обратном порядка по сравнению с исходным массивом.")]
        public void Linq39() {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            
            var reversedIDigits = (
                from digit in digits
                where digit[1] == 'i'
                select digit)
                .Reverse();
            
            Console.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits) {
                Console.WriteLine(d);
            }             
        }

        [Category("Операторы группировки")]
        [Title("GroupBy - простой 1")]
        [Description("Образец использования Group By для разбиения списка чисел по " +
                    "их остатку от деления на 5.")]
        public void Linq40() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            var numberGroups =
                from num in numbers
                group num by num % 5 into numGroup
                select new { Remainder = numGroup.Key, Numbers = numGroup };
            
            foreach (var grp in numberGroups) {
                Console.WriteLine("Numbers with a remainder of {0} when divided by 5:", grp.Remainder);
                foreach (var n in grp.Numbers) {
                    Console.WriteLine(n);
                }
            }
        }
        
        [Category("Операторы группировки")]
        [Title("GroupBy - простой 2")]
        [Description("Образец использования Group By для разбиения списка слов по " +
                     "их первой букве.")]
        public void Linq41() {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            
            var wordGroups =
                from num in words
                group num by num[0] into grp
                select new { FirstLetter = grp.Key, Words = grp };
            
            foreach (var wordgrp in wordGroups) {
                Console.WriteLine("Words that start with the letter '{0}':", wordgrp.FirstLetter);
                foreach (var word in wordgrp.Words) {
                    Console.WriteLine(word);
                }
            }
        }
        
        [Category("Операторы группировки")]
        [Title("GroupBy - простой 3")]
        [Description("Образец использования Group By для разбиения списка продуктов по категориям.")]
        public void Linq42() {
            List<Product> products = GetProductList();

            var orderGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, Products = prodGroup };
                           
            ObjectDumper.Write(orderGroups, 1);
        }
        
        [Category("Операторы группировки")]
        [Title("GroupBy - вложенные")]
        [Description("Образец использования Group By для разбиения списка заказов всех клиентов " +
                     "сначала по годам, а затем по месяцам.")]
        public void Linq43() {
            List<Customer> customers = GetCustomerList();

            var customerOrderGroups = 
                from cust in customers
                select
                    new {cust.CompanyName, 
                         YearGroups =
                             from order in cust.Orders
                             group order by order.OrderDate.Year into yearGroup
                             select
                                 new {Year = yearGroup.Key,
                                      MonthGroups = 
                                          from order in yearGroup
                                          group order by order.OrderDate.Month into MonthGroup
                                          select new { Month = MonthGroup.Key, Orders = MonthGroup }
                                     }
                        };
                           
            ObjectDumper.Write(customerOrderGroups, 3);
        }
        
        public class AnagramEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) {
                return getCanonicalString(x) == getCanonicalString(y);
            }

            public int GetHashCode(string obj) {
                return getCanonicalString(obj).GetHashCode();
            }
            
            private string getCanonicalString(string word) {
                char[] wordChars = word.ToCharArray();
                Array.Sort<char>(wordChars);
                return new string(wordChars);
            }
        }
        
        [Category("Операторы группировки")]
        [Title("GroupBy - сравнение")]
        [Description("В этом примере используется GroupBy с синтаксисом метода для разбиения обрезанных элементов массива с помощью " +
                     "пользовательского средства, сопоставляющего слова, которые являются анаграммами друг друга.")]
        [LinkedClass("AnagramEqualityComparer")]
        public void Linq44() {
            string[] anagrams = {"from   ", " salt", " earn ", "  last   ", " near ", " form  "};

            var orderGroups = anagrams.GroupBy(w => w.Trim(), new AnagramEqualityComparer());
                           
            ObjectDumper.Write(orderGroups, 1);
        }
        
        [Category("Операторы группировки")]
        [Title("GroupBy - сравнение, сопоставление")]
        [Description("В этом примере метод GroupBy используется для разбиения обрезанных элементов массива с помощью " +
                     "пользовательского средства, сопоставляющего слова, которые являются анаграммами друг друга, " +
                     "а затем преобразующего результаты в верхний регистр.")]
        [LinkedClass("AnagramEqualityComparer")]
        public void Linq45() {
            string[] anagrams = {"from   ", " salt", " earn ", "  last   ", " near ", " form  "};

            var orderGroups = anagrams.GroupBy(
                        w => w.Trim(), 
                        a => a.ToUpper(),
                        new AnagramEqualityComparer()
                        );
                           
            ObjectDumper.Write(orderGroups, 1);
        }
                     
        [Category("Операторы наборов")]
        [Title("Distinct - 1")]
        [Description("Образец использования Distinct для удаления повторяющихся элементов в последовательности " +
                    "множителей 300.")]
        public void Linq46() {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };
            
            var uniqueFactors = factorsOf300.Distinct();

            Console.WriteLine("Prime factors of 300:");
            foreach (var f in uniqueFactors) {
                Console.WriteLine(f);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Distinct - 2")]
        [Description("Образец использования Distinct для поиска уникальных имен категорий.")]
        public void Linq47() {
            List<Product> products = GetProductList();
            
            var categoryNames = (
                from prod in products
                select prod.Category)
                .Distinct();
                                                 
            Console.WriteLine("Category names:");
            foreach (var n in categoryNames) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Union - 1")]
        [Description("Образец использования Union для создания последовательности, содержащей уникальные значения " +
                     "из обоих массивов.")]
        public void Linq48() {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            
            var uniqueNumbers = numbersA.Union(numbersB);
            
            Console.WriteLine("Unique numbers from both arrays:");
            foreach (var n in uniqueNumbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Union - 2")]
        [Description("В этом примере метод Union используется для создания последовательности, содержащей уникальные первые буквы " +
                     "из названия продукта и имени клиента. Union доступен только через синтаксис метода.")]
        public void Linq49() {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();
            
            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];
            
            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);
            
            Console.WriteLine("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars) {
                Console.WriteLine(ch);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Intersect - 1")]
        [Description("Образец использования Intersect для создания одной последовательности, содержащей общие значения, " +
                    "используемые совместно обоими массивами.")]
        public void Linq50() {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            
            var commonNumbers = numbersA.Intersect(numbersB);
            
            Console.WriteLine("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Intersect - 2")]
        [Description("Образец использования Intersect для создания последовательности, содержащей общие первые буквы " +
                     "из названия продукта и имени клиента.")]
        public void Linq51() {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();
            
            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];
            
            var commonFirstChars = productFirstChars.Intersect(customerFirstChars);
            
            Console.WriteLine("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars) {
                Console.WriteLine(ch);
            }
        }
        
        [Category("Операторы наборов")]
        [Title("Except - 1")]
        [Description("Образец использования Except для создания последовательности, содержащей значения из таблицы numbersA," +
                     "которых нет также в numbersB.")]
        public void Linq52() {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            
            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);
            
            Console.WriteLine("Numbers in first array but not second array:");
            foreach (var n in aOnlyNumbers) {
                Console.WriteLine(n);
            }
        }
    
        [Category("Операторы наборов")]
        [Title("Except - 2")]
        [Description("Образец использования Except для создания последовательности, содержащей первые буквы " +
                     "названий продуктов, которые также не являются первыми буквами имен клиентов.")]
        public void Linq53() {
            List<Product> products = GetProductList();
            List<Customer> customers = GetCustomerList();
            
            var productFirstChars =
                from prod in products
                select prod.ProductName[0];
            var customerFirstChars =
                from cust in customers
                select cust.CompanyName[0];
            
            var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);
            
            Console.WriteLine("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars) {
                Console.WriteLine(ch);
            }
        }
        
        [Category("Операторы преобразования")]
        [Title("ToArray")]
        [Description("Образец использования ToArray для немедленного преобразования последовательности в массив.")]
        public void Linq54() {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            
            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;
            var doublesArray = sortedDoubles.ToArray();
            
            Console.WriteLine("Every other double from highest to lowest:");
            for (int d = 0; d < doublesArray.Length; d += 2) {
                Console.WriteLine(doublesArray[d]);
            }             
        }
        
        [Category("Операторы преобразования")]
        [Title("ToList")]
        [Description("Пример использования ToList для немедленной вставки последовательности в List<T>.")]
        public void Linq55() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            var sortedWords =
                from w in words
                orderby w
                select w;
            var wordList = sortedWords.ToList();
            
            Console.WriteLine("The sorted word list:");
            foreach (var w in wordList) {
                Console.WriteLine(w);
            }             
        }
        
        [Category("Операторы преобразования")]
        [Title("ToDictionary")]
        [Description("Образец использования ToDictionary для немедленного преобразования последовательности и " +
                    "связанного ключевого выражения в словарь.")]
        public void Linq56() {
            var scoreRecords = new [] { new {Name = "Alice", Score = 50},
                                        new {Name = "Bob"  , Score = 40},
                                        new {Name = "Cathy", Score = 45}
                                    };
            
            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);
            
            Console.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]);
        }

        [Category("Операторы преобразования")]
        [Title("OfType")]
        [Description("Пример использования OfType для возврата только тех элементов массива, тип которых - double.")]
        public void Linq57() {
            object[] numbers = { null, 1.0, "two", 3, "four", 5, "six", 7.0 };

            var doubles = numbers.OfType<double>();
            
            Console.WriteLine("Numbers stored as doubles:");
            foreach (var d in doubles) {
                Console.WriteLine(d);
            }       
        }

        [Category("Операторы элементов")]
        [Title("First - простой")]
        [Description("Образец использования First для возврата первого подходящего элемента " +
                     "как \"Товар\", вместо того чтобы возвращать последовательность, содержащую \"Товар\".")]
        public void Linq58() {
            List<Product> products = GetProductList();

            Product product12 = (
                from prod in products
                where prod.ProductID == 12
                select prod)
                .First();
        
            ObjectDumper.Write(product12);
        }
        
        [Category("Операторы элементов")]
        [Title("First - условный")]
        [Description("Образец использования First для поиска первого элемента массива, начинающегося с \"o\".")]
        public void Linq59() {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string startsWithO = strings.First(s => s[0] == 'o');
        
            Console.WriteLine("A string starting with 'o': {0}", startsWithO);
        }
        
        [Category("Операторы элементов")]
        [Title("FirstOrDefault - простой")]
        [Description("Образец использования FirstOrDefault для возврата первого элемента последовательности. " +
                     "Если элементов нет, будет возвращено значение типа " +
                     "возвращается. FirstOrDefault полезен для создания внешних соединений.")]
        public void Linq61() {
            int[] numbers = {};

            int firstNumOrDefault = numbers.FirstOrDefault();
        
            Console.WriteLine(firstNumOrDefault);
        }
        
        [Category("Операторы элементов")]
        [Title("FirstOrDefault - условный")]
        [Description("Пример использования FirstOrDefault для возврата первого продукта, ProductID которого - 789, " +
                     "в виде единственного объекта Product, и null, если соответствие не найдено.")]
        public void Linq62() {
            List<Product> products = GetProductList();

            Product product789 = products.FirstOrDefault(p => p.ProductID == 789);
        
            Console.WriteLine("Product 789 exists: {0}", product789 != null);
        }
             
        [Category("Операторы элементов")]
        [Title("ElementAt")]
        [Description("Образец использования ElementAt для получения второго числа, превышающего 5, " +
                     "из массива.")]
        public void Linq64() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int fourthLowNum = (
                from num in numbers
                where num > 5
                select num )
                .ElementAt(1);  // Вторым числом является индекс 1, поскольку последовательности нумеруются с 0
        
            Console.WriteLine("Second number > 5: {0}", fourthLowNum);
        }
        
        [Category("Операторы создания")]
        [Title("Диапазон")]
        [Description("Образец использования Range для создания последовательности чисел от 100 до 149, " +
                     "которая используется для определения, какие числа в этом диапазоне являются четными, а какие нечетными.")]
        public void Linq65() {
            var numbers =
                from n in Enumerable.Range(100, 50)
                select new {Number = n, OddEven = n % 2 == 1 ? "odd" : "even"};
            
            foreach (var n in numbers) {
                Console.WriteLine("The number {0} is {1}.", n.Number, n.OddEven);
            }
        }
        
        [Category("Операторы создания")]
        [Title("Повторить")]
        [Description("Образец использования Repeat для создания последовательности, в которой десять раз встречается число 7.")]
        public void Linq66() {
            var numbers = Enumerable.Repeat(7, 10);
            
            foreach (var n in numbers) {
                Console.WriteLine(n);
            }
        }
        
        
        [Category("Кванторы")]
        [Title("Any - простой")]
        [Description("Образец использования Any для определения наличия в массиве слов, которые " +
                     "содержат подстроку \"ei\".")]
        public void Linq67() {
            string[] words = { "believe", "relief", "receipt", "field" };
            
            bool iAfterE = words.Any(w => w.Contains("ei"));

            //DONE fixed typo in writeline
            Console.WriteLine("There is a word in the list that contains 'ei': {0}", iAfterE);
        }
        
        [Category("Кванторы")]
        [Title("Any - с группировкой")]
        [Description("Образец использования Any для возврата сгруппированного списка продуктов для категорий, " +
                     "у которых по крайней мере одного продукта нет на складе.")]
        public void Linq69() {
            List<Product> products = GetProductList();

            var productGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                where prodGroup.Any(p => p.UnitsInStock == 0)
                select new { Category = prodGroup.Key, Products = prodGroup };

            ObjectDumper.Write(productGroups, 1);
        }
        
        [Category("Кванторы")]
        [Title("All - простой")]
        [Description("Образец использования All для определения, содержит ли массив " +
                     "только нечетные числа.")]
        public void Linq70() {
            int[] numbers = { 1, 11, 3, 19, 41, 65, 19 };
            
            bool onlyOdd = numbers.All(n => n % 2 == 1);

            Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
        }
                
        [Category("Кванторы")]
        [Title("All - с группировкой")]
        [Description("Образец использования Any для возврата сгруппированного списка продуктов для категорий, " +
                     "у которых все продукты есть в запасе.")]
        public void Linq72() {
            List<Product> products = GetProductList();

            var productGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                where prodGroup.All(p => p.UnitsInStock > 0)
                select new { Category = prodGroup.Key, Products = prodGroup };

            ObjectDumper.Write(productGroups, 1);
        }

        [Category("Статистические операторы")]
        [Title("Count - простой")]
        [Description("В этом примере метод Count используется для получения числа уникальных простых множителей числа 300.")]
        public void Linq73() {
            int[] primeFactorsOf300 = { 2, 2, 3, 5, 5 };
            
            int uniqueFactors = primeFactorsOf300.Distinct().Count();

            Console.WriteLine("Для числа 300 имеется {0} уникальных простых множителей.", uniqueFactors);
        }

        [Category("Статистические операторы")]
        [Title("Count - условный")]
        [Description("Образец использования Count для получения количества нечетных целых чисел в массиве.")]
        public void Linq74() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            
            Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
        }

        [Category("Статистические операторы")]
        [Title("Count - вложенный")]
        [Description("Образец использования Count для возврата списка клиентов, а также количества заказов, " +
                     "у каждого.")]
        public void Linq76() {
            List<Customer> customers = GetCustomerList();
            
            var orderCounts =
                from cust in customers
                select new {cust.CustomerID, OrderCount = cust.Orders.Count()};
        
            ObjectDumper.Write(orderCounts);
        }
        
        [Category("Статистические операторы")]
        [Title("Count - с группировкой")]
        [Description("Образец использования Count для возврата списка категорий, а также количества продуктов, " +
                     "у каждого.")]
        public void Linq77() {
            List<Product> products = GetProductList();

            var categoryCounts =
                from prod in products
                group prod by prod.Category into prodGroup
                select new {Category = prodGroup.Key, ProductCount = prodGroup.Count()};
            
            ObjectDumper.Write(categoryCounts);
        }
        
        //DONE Changed "get the total of" to "add all"
        [Category("Статистические операторы")]
        [Title("Sum - простой")]
        [Description("В этом примере метод Sum используется для сложения всех чисел в массиве.")]
        public void Linq78() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            double numSum = numbers.Sum();
            
            Console.WriteLine("The sum of the numbers is {0}.", numSum);
        }

        [Category("Статистические операторы")]
        [Title("Сумма - проекция")]
        [Description("Образец использования Sum для получения общего количества букв во всех словах " +
                     "в массиве.")]
        public void Linq79() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            double totalChars = words.Sum(w => w.Length);
            
            Console.WriteLine("There are a total of {0} characters in these words.", totalChars);
        }

        
        
        [Category("Статистические операторы")]
        [Title("Sum - с группировкой")]
        [Description("Образец использования Sum для получения общего количества единиц в запасе по каждой категории продуктов.")]
        public void Linq80() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new {Category = prodGroup.Key, TotalUnitsInStock = prodGroup.Sum(p => p.UnitsInStock)};
        
            ObjectDumper.Write(categories);
        }
        
        [Category("Статистические операторы")]
        [Title("Min - простой")]
        [Description("Образец использования Min для получения наименьшего числа в массиве.")]
        public void Linq81() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            int minNum = numbers.Min();
            
            Console.WriteLine("The minimum number is {0}.", minNum);
        }

        [Category("Статистические операторы")]
        [Title("Min - проекция")]
        [Description("Образец использования Min для вычисления длины самого короткого слова в массиве.")]
        public void Linq82() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            int shortestWord = words.Min(w => w.Length);
            
            Console.WriteLine("The shortest word is {0} characters long.", shortestWord);
        }
        
        [Category("Статистические операторы")]
        [Title("Min - с группировкой")]
        [Description("Образец использования Min для поиска наименьшей цены продуктов в каждой категории.")]
        public void Linq83() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new {Category = prodGroup.Key, CheapestPrice = prodGroup.Min(p => p.UnitPrice)};
        
            ObjectDumper.Write(categories);
        }
        
        [Category("Статистические операторы")]
        [Title("Min - элементы")]
        [Description("В этом примере метод Min используется для получения продуктов с наименьшей ценой в каждой категории.")]
        public void Linq84() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                let minPrice = prodGroup.Min(p => p.UnitPrice)
                select new {Category = prodGroup.Key, CheapestProducts = prodGroup.Where(p => p.UnitPrice == minPrice)};
        
            ObjectDumper.Write(categories, 1);
        }
        
        [Category("Статистические операторы")]
        [Title("Max - простой")]
        [Description("В этом примере метод Max используется для получения наибольшего числа в массиве. Обратите внимание, что метод возвращает единственное значение.")]
        public void Linq85() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            int maxNum = numbers.Max();
            
            Console.WriteLine("The maximum number is {0}.", maxNum);
        }

        [Category("Статистические операторы")]
        [Title("Max - проекция")]
        [Description("Образец использования Max для получения длины наибольшего слова в массиве.")]
        public void Linq86() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            int longestLength = words.Max(w => w.Length);
            
            Console.WriteLine("The longest word is {0} characters long.", longestLength);
        }
        
        [Category("Статистические операторы")]
        [Title("Max - с группировкой")]
        [Description("Образец использования Max для получения наибольшей цены в каждой категории продуктов.")]
        public void Linq87() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new {Category = prodGroup.Key, MostExpensivePrice = prodGroup.Max(p => p.UnitPrice)};
        
            ObjectDumper.Write(categories);
        }
        
        [Category("Статистические операторы")]
        [Title("Max - элементы")]
        [Description("Образец использования Max для получения продуктов с наибольшей ценой в каждой категории.")]
        public void Linq88() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                let maxPrice = prodGroup.Max(p => p.UnitPrice)
                select new {Category = prodGroup.Key, MostExpensiveProducts = prodGroup.Where(p => p.UnitPrice == maxPrice)};
        
            ObjectDumper.Write(categories, 1);
        }
        
        [Category("Статистические операторы")]
        [Title("Average - простой")]
        [Description("Образец использования Average для получения среднего значения чисел в массиве.")]
        public void Linq89() {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            double averageNum = numbers.Average();
            
            Console.WriteLine("The average number is {0}.", averageNum);
        }

        [Category("Статистические операторы")]
        [Title("Average - проекция")]
        [Description("Образец использования Average для вычисления средней длины слов в массиве.")]
        public void Linq90() {
            string[] words = { "cherry", "apple", "blueberry" };
            
            double averageLength = words.Average(w => w.Length);
            
            Console.WriteLine("The average word length is {0} characters.", averageLength);
        }
        
        [Category("Статистические операторы")]
        [Title("Average - с группировкой")]
        [Description("Образец использования Average для вычисления средней цены продуктов в каждой категории.")]
        public void Linq91() {
            List<Product> products = GetProductList();

            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new {Category = prodGroup.Key, AveragePrice = prodGroup.Average(p => p.UnitPrice)};
        
            ObjectDumper.Write(categories);
        }

        [Category("Статистические операторы")]
        [Title("Aggregate - простой")]
        [Description("Образец использования Aggregate для расчета текущего произведения для массива, в котором " +
                     "вычисляется общее произведение всех элементов.")]
        public void Linq92() {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            
            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
            
            Console.WriteLine("Total product of all numbers: {0}", product);
        }
        
        [Category("Статистические операторы")]
        [Title("Aggregate - начальное значение")]
        [Description("Образец использования Aggregate для создания текущего сальдо счета, которое " +
                     "отражает все вычеты с исходного сальдо, равного 100, пока " +
                     "сальдо не опустится ниже 0.")]
        public void Linq93() {
            double startBalance = 100.0;
            
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };
            
            double endBalance = 
                attemptedWithdrawals.Aggregate(startBalance,
                    (balance, nextWithdrawal) =>
                        ( (nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance ) );
            
            Console.WriteLine("Ending balance: {0}", endBalance);
        }
        
        [Category("Прочие операторы")]
        [Title("Concat - 1")]
        [Description("Образец использования Concat для создания последовательности, содержащей все значения " +
                     "массива, одно за другим.")]
        public void Linq94() {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            
            var allNumbers = numbersA.Concat(numbersB);
            
            Console.WriteLine("All numbers from both arrays:");
            foreach (var n in allNumbers) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Прочие операторы")]
        [Title("Concat - 2")]
        [Description("Образец использования Concat для создания последовательности, содержащей имена " +
                     "всех заказчиков и товаров, в том числе все дубликаты.")]
        public void Linq95() {
            List<Customer> customers = GetCustomerList();
            List<Product> products = GetProductList();
            
            var customerNames =
                from cust in customers
                select cust.CompanyName;
            var productNames =
                from prod in products
                select prod.ProductName;
            
            var allNames = customerNames.Concat(productNames);
            
            Console.WriteLine("Customer and product names:");
            foreach (var n in allNames) {
                Console.WriteLine(n);
            }
        }
        
        [Category("Прочие операторы")]
        [Title("EqualAll - 1")]
        [Description("В этом примере метод SequenceEquals используется для проверки совпадения двух последовательностей по всем элементам " +
                     "в одном и том же порядке.")]
        public void Linq96() {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };
            
            bool match = wordsA.SequenceEqual(wordsB);
            
            Console.WriteLine("The sequences match: {0}", match);
        }
        
        [Category("Прочие операторы")]
        [Title("EqualAll - 2")]
        [Description("Пример использования SequenceEqual для проверки того, все ли элементы двух последовательностей соответствуют друг другу" +
                     "в одном и том же порядке.")]
        public void Linq97() {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };

            bool match = wordsA.SequenceEqual(wordsB);
            
            Console.WriteLine("The sequences match: {0}", match);
        }

        [Category("Выполнение запроса")]
        [Title("Отложенное выполнение")]
        [Description("Пример задержки выполнения запроса до его " +
                     "перечисления в операторе For Each.")]
        public void Linq99() {
            
            // Запросы не исполняются, пока на них не будет выполнено перечисление.
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            
            int i = 0;
            var simpleQuery =
                from num in numbers
                select ++i;

            // Локальная переменная "i" не увеличивается до тех пор,
            // пока запрос не станет выполняться в цикле foreach.
            Console.WriteLine("The current value of i is {0}", i); //i is still zero

            foreach (var item in simpleQuery) {
                Console.WriteLine("v = {0}, i = {1}", item, i); // теперь i увеличивается          
            }  
        }
        
        [Category("Выполнение запроса")]
        [Title("Немедленное выполнение")]
        [Description("В следующем примере показано, как запросы могут немедленно выполняться, а их результаты " +
                    " сохраняться в памяти с помощью таких методов как ToList.")]
        public void Linq100() {

            // Такие методы как ToList(), Max() и Count() вызывают
            // немедленное выполнение запроса.            
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };        
            
            int i = 0;
            var immediateQuery = (
                from num in numbers
                select ++i )
                .ToList();

            Console.WriteLine("The current value of i is {0}", i); //i has been incremented
            
            foreach (var item in immediateQuery) {
                Console.WriteLine("v = {0}, i = {1}", item, i);
            }  
        }

        [Category("Выполнение запроса")]
        [Title("Повторное использование запроса")]
        [Description("Образец повторного использования запросов с отложенным выполнением " +
                     "после изменения данных и последующей работы с новыми данными.")]
        public void Linq101() {

            // Отложенное исполнение позволяет один раз определить запрос
            // а затем последующее его повторное использование различными способами.
            int[] numbers = new int[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var lowNumbers =
                from num in numbers
                where num <= 3
                select num;

            Console.WriteLine("First run numbers <= 3:");
            foreach (int n in lowNumbers) {
                Console.WriteLine(n);
            }

            // Запрос для исходного запроса.
            var lowEvenNumbers =
                from num in lowNumbers
                where num % 2 == 0
                select num;

            Console.WriteLine("Run lowEvenNumbers query:");
            foreach (int n in lowEvenNumbers)
            {
                Console.WriteLine(n);
            }

            // Измените исходные данные.
            for (int i = 0; i < 10; i++) {
                numbers[i] = -numbers[i];
            }

            // Во время второго прохода тот же самый объект запроса,
            // lowNumbers, будет выполнять итерацию с новым состоянием
            // numbers[], генерируя другие результаты:
            Console.WriteLine("Second run numbers <= 3:");
            foreach (int n in lowNumbers) {
                Console.WriteLine(n);
            }
        }

        [Category("Операторы соединения")]
        [Title("Внутреннее соединение")]
        [Description("В этом примере показано, как выполнить простое внутренне эквисоединение двух последовательностей " +
            "для создания фиксированного набора результатов, который включает каждый элемент у поставщиков, имеющих совпадающий элемент " +
            "у клиентов.")]
        public void Linq102() {

            List<Customer> customers = GetCustomerList();
            List<Supplier> suppliers = GetSupplierList();

            var custSupJoin = 
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country
                select new { Country = sup.Country, SupplierName = sup.SupplierName, CustomerName = cust.CompanyName };

            foreach (var item in custSupJoin) {
                Console.WriteLine("Country = {0}, Supplier = {1}, Customer = {2}", item.Country, item.SupplierName, item.CustomerName);
            }
        }      

        [Category("Операторы соединения")]
        [Title("Group Join")]
        [Description("Групповое соединение создает иерархическую последовательность. Следующий запрос является внутренним соединением, " +
                    " которое создает последовательность объектов, каждый из которых имеет ключ и внутреннюю последовательность всех совпадающих элементов.")]
        public void Linq103() {
            
               
            List<Customer> customers = GetCustomerList();
            List<Supplier> suppliers = GetSupplierList();

            var custSupQuery =
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country into cs
                select new { Key = sup.Country, Items = cs };

           
            foreach (var item in custSupQuery) {
                Console.WriteLine(item.Key + ":");
                foreach (var element in item.Items) 
                {
                    Console.WriteLine("   " + element.CompanyName); 
                }
            }
        }

        [Category("Операторы соединения")]
        [Title("Cross Join с Group Join")]
        [Description("Оператор group join является более общим, чем join, как  показывает несколько более подробный " +
            "вариант примера перекрестного объединения.")]
        public void Linq104() {
            string[] categories = new string[]{ 
                "Beverages", 
                "Condiments", 
                "Vegetables", 
                "Dairy Products", 
                "Seafood" };
               
            List<Product> products = GetProductList();

            var prodByCategory = 
                from cat in categories
                join prod in products on cat equals prod.Category into ps
                from p in ps   
                select new { Category = cat, p.ProductName };

            foreach (var item in prodByCategory) {
                Console.WriteLine(item.ProductName + ": " + item.Category);
            }
        }
        [Category("Операторы соединения")]
        [Title("Left Outer Join")]
        [Description("Левое внешнее соединение создает набор результатов, который включает все элементы с левой стороны, как " + 
            "минимум однажды, даже если они не соответствуют ни одному из элементов с правой стороны.")]
        public void Linq105() {
            List<Customer> customers = GetCustomerList();
            List<Supplier> suppliers = GetSupplierList();

            var supplierCusts = 
                from sup in suppliers
                join cust in customers on sup.Country equals cust.Country into cs
                from c in cs.DefaultIfEmpty()  // DefaultIfEmpty сохраняет элементы с левой стороны, не имеющие соответствия на правой стороне 
                orderby sup.SupplierName
                select new { Country = sup.Country, CompanyName = c == null ? "(No customers)" : c.CompanyName,
                             SupplierName = sup.SupplierName};

            foreach (var item in supplierCusts) {
                Console.WriteLine("{0} ({1}): {2}", item.SupplierName, item.Country, item.CompanyName);
            }
        }

        [Category("Операторы соединения")]
        [Title("Левое внешнее соединение №2")]
        [Description("Для каждого клиента в таблице клиентов этот запрос возвращает всех поставщиков " +
                     "из одной страны, либо строку, указывающую, что не найдены поставщики из этой страны.")]
        public void Linq106()
        {

            List<Customer> customers = GetCustomerList();
            List<Supplier> suppliers = GetSupplierList();

            var custSuppliers =
                from cust in customers
                join sup in suppliers on cust.Country equals sup.Country into ss
                from s in ss.DefaultIfEmpty()
                orderby cust.CompanyName
                select new { Country = cust.Country, CompanyName = cust.CompanyName,
                             SupplierName = s == null ? "(No suppliers)" : s.SupplierName };

            foreach (var item in custSuppliers) {
                Console.WriteLine("{0} ({1}): {2}", item.CompanyName, item.Country, item.SupplierName);
            }
        }

        [Category("Операторы соединения")]
        [Title("Левое внешнее соединение с составным ключом")]
        [Description("Для каждого поставщика в таблице поставщиков этот запрос возвращает всех клиентов " +
                     "из одного города и одной страны, либо строка, указывающая, что не найдены клиенты из этого города и этой страны. " +
                     "Обратите внимание на использование анонимных типов для инкапсуляции нескольких значений ключей.")]
        public void Linq107()
        {
            List<Customer> customers = GetCustomerList();
            List<Supplier> suppliers = GetSupplierList();

            var supplierCusts =
                from sup in suppliers
                join cust in customers on new { sup.City, sup.Country } equals new { cust.City, cust.Country } into cs
                from c in cs.DefaultIfEmpty() //Удалите вызов метода DefaultIfEmpty, чтобы сделать это внутреннее соединение
                orderby sup.SupplierName
                select new { Country = sup.Country, 
                             City = sup.City,
                             SupplierName = sup.SupplierName,
                             CompanyName = c == null ? "(No customers)" : c.CompanyName
                           };

            foreach (var item in supplierCusts)
            {
                Console.WriteLine("{0} ({1}, {2}): {3}", item.SupplierName, item.City, item.Country, item.CompanyName);
            }
        }

        [Category("* Sample Data *")]
        [Title("CustomerList / ProductList / SupplierList")]
        [Description("Этот метод отображает данные примеров, используемые упомянутыми выше запросами.  Ниже представлен также " +
                     "методом, формирующим списки.  ProductList и supplierList построены с использованием " +
                     "инициализаторов коллекций, а CustomerList использует XLinq для чтения их значений " +
                     "в память из XML-документа.")]
        [LinkedMethod("GetProductList")]
        [LinkedMethod("GetSupplierList")]
        [LinkedMethod("GetCustomerList")]
        [LinkedMethod("createLists")]
        public void Linq115() {
            ObjectDumper.Write(GetCustomerList(), 1);

            Console.WriteLine();

            ObjectDumper.Write(GetProductList());
        }



        public List<Product> GetProductList() {
            if (productList == null)
                createLists();
            
            return productList;
        }

        public List<Supplier> GetSupplierList()
        {
            if (supplierList == null)
                createLists();

            return supplierList;
        }

        public List<Customer> GetCustomerList() {
            if (customerList == null)
                createLists();
            
            return customerList;
        }

        private void createLists() {
            // Данные о продукте, созданные в памяти при помощи инициализатора коллекции:
            productList =
                new List<Product> {
                    new Product { ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 39 },
                    new Product { ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.0000M, UnitsInStock = 17 },
                    new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.0000M, UnitsInStock = 13 },
                    new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.0000M, UnitsInStock = 53 },
                    new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.3500M, UnitsInStock = 0 },
                    new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", UnitPrice = 25.0000M, UnitsInStock = 120 },
                    new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", UnitPrice = 30.0000M, UnitsInStock = 15 },
                    new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", UnitPrice = 40.0000M, UnitsInStock = 6 },
                    new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", UnitPrice = 97.0000M, UnitsInStock = 29 },
                    new Product { ProductID = 10, ProductName = "Ikura", Category = "Seafood", UnitPrice = 31.0000M, UnitsInStock = 31 },
                    new Product { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", UnitPrice = 21.0000M, UnitsInStock = 22 },
                    new Product { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", UnitPrice = 38.0000M, UnitsInStock = 86 },
                    new Product { ProductID = 13, ProductName = "Konbu", Category = "Seafood", UnitPrice = 6.0000M, UnitsInStock = 24 },
                    new Product { ProductID = 14, ProductName = "Tofu", Category = "Produce", UnitPrice = 23.2500M, UnitsInStock = 35 },
                    new Product { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", UnitPrice = 15.5000M, UnitsInStock = 39 },
                    new Product { ProductID = 16, ProductName = "Pavlova", Category = "Confections", UnitPrice = 17.4500M, UnitsInStock = 29 },
                    new Product { ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry", UnitPrice = 39.0000M, UnitsInStock = 0 },
                    new Product { ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood", UnitPrice = 62.5000M, UnitsInStock = 42 },
                    new Product { ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections", UnitPrice = 9.2000M, UnitsInStock = 25 },
                    new Product { ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections", UnitPrice = 81.0000M, UnitsInStock = 40 },
                    new Product { ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections", UnitPrice = 10.0000M, UnitsInStock = 3 },
                    new Product { ProductID = 22, ProductName = "Gustaf's Knäckebröd", Category = "Grains/Cereals", UnitPrice = 21.0000M, UnitsInStock = 104 },
                    new Product { ProductID = 23, ProductName = "Tunnbröd", Category = "Grains/Cereals", UnitPrice = 9.0000M, UnitsInStock = 61 },
                    new Product { ProductID = 24, ProductName = "Guaraná Fantástica", Category = "Beverages", UnitPrice = 4.5000M, UnitsInStock = 20 },
                    new Product { ProductID = 25, ProductName = "NuNuCa Nuß-Nougat-Creme", Category = "Confections", UnitPrice = 14.0000M, UnitsInStock = 76 },
                    new Product { ProductID = 26, ProductName = "Gumbär Gummibärchen", Category = "Confections", UnitPrice = 31.2300M, UnitsInStock = 15 },
                    new Product { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", UnitPrice = 43.9000M, UnitsInStock = 49 },
                    new Product { ProductID = 28, ProductName = "Rössle Sauerkraut", Category = "Produce", UnitPrice = 45.6000M, UnitsInStock = 26 },
                    new Product { ProductID = 29, ProductName = "Thüringer Rostbratwurst", Category = "Meat/Poultry", UnitPrice = 123.7900M, UnitsInStock = 0 },
                    new Product { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", UnitPrice = 25.8900M, UnitsInStock = 10 },
                    new Product { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", UnitPrice = 12.5000M, UnitsInStock = 0 },
                    new Product { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", UnitPrice = 32.0000M, UnitsInStock = 9 },
                    new Product { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", UnitPrice = 2.5000M, UnitsInStock = 112 },
                    new Product { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 111 },
                    new Product { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 20 },
                    new Product { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", UnitPrice = 19.0000M, UnitsInStock = 112 },
                    new Product { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", UnitPrice = 26.0000M, UnitsInStock = 11 },
                    new Product { ProductID = 38, ProductName = "Côte de Blaye", Category = "Beverages", UnitPrice = 263.5000M, UnitsInStock = 17 },
                    new Product { ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 69 },
                    new Product { ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood", UnitPrice = 18.4000M, UnitsInStock = 123 },
                    new Product { ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood", UnitPrice = 9.6500M, UnitsInStock = 85 },
                    new Product { ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals", UnitPrice = 14.0000M, UnitsInStock = 26 },
                    new Product { ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages", UnitPrice = 46.0000M, UnitsInStock = 17 },
                    new Product { ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments", UnitPrice = 19.4500M, UnitsInStock = 27 },
                    new Product { ProductID = 45, ProductName = "Rogede sild", Category = "Seafood", UnitPrice = 9.5000M, UnitsInStock = 5 },
                    new Product { ProductID = 46, ProductName = "Spegesild", Category = "Seafood", UnitPrice = 12.0000M, UnitsInStock = 95 },
                    new Product { ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections", UnitPrice = 9.5000M, UnitsInStock = 36 },
                    new Product { ProductID = 48, ProductName = "Chocolade", Category = "Confections", UnitPrice = 12.7500M, UnitsInStock = 15 },
                    new Product { ProductID = 49, ProductName = "Maxilaku", Category = "Confections", UnitPrice = 20.0000M, UnitsInStock = 10 },
                    new Product { ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections", UnitPrice = 16.2500M, UnitsInStock = 65 },
                    new Product { ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce", UnitPrice = 53.0000M, UnitsInStock = 20 },
                    new Product { ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals", UnitPrice = 7.0000M, UnitsInStock = 38 },
                    new Product { ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry", UnitPrice = 32.8000M, UnitsInStock = 0 },
                    new Product { ProductID = 54, ProductName = "Tourtière", Category = "Meat/Poultry", UnitPrice = 7.4500M, UnitsInStock = 21 },
                    new Product { ProductID = 55, ProductName = "Pâté chinois", Category = "Meat/Poultry", UnitPrice = 24.0000M, UnitsInStock = 115 },
                    new Product { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", UnitPrice = 38.0000M, UnitsInStock = 21 },
                    new Product { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", UnitPrice = 19.5000M, UnitsInStock = 36 },
                    new Product { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", UnitPrice = 13.2500M, UnitsInStock = 62 },
                    new Product { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", UnitPrice = 55.0000M, UnitsInStock = 79 },
                    new Product { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", UnitPrice = 34.0000M, UnitsInStock = 19 },
                    new Product { ProductID = 61, ProductName = "Sirop d'érable", Category = "Condiments", UnitPrice = 28.5000M, UnitsInStock = 113 },
                    new Product { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", UnitPrice = 49.3000M, UnitsInStock = 17 },
                    new Product { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", UnitPrice = 43.9000M, UnitsInStock = 24 },
                    new Product { ProductID = 64, ProductName = "Wimmers gute Semmelknödel", Category = "Grains/Cereals", UnitPrice = 33.2500M, UnitsInStock = 22 },
                    new Product { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", UnitPrice = 21.0500M, UnitsInStock = 76 },
                    new Product { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", UnitPrice = 17.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 52 },
                    new Product { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", UnitPrice = 12.5000M, UnitsInStock = 6 },
                    new Product { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", UnitPrice = 36.0000M, UnitsInStock = 26 },
                    new Product { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", UnitPrice = 15.0000M, UnitsInStock = 15 },
                    new Product { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", UnitPrice = 21.5000M, UnitsInStock = 26 },
                    new Product { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", UnitPrice = 34.8000M, UnitsInStock = 14 },
                    new Product { ProductID = 73, ProductName = "Röd Kaviar", Category = "Seafood", UnitPrice = 15.0000M, UnitsInStock = 101 },
                    new Product { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", UnitPrice = 10.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 75, ProductName = "Rhönbräu Klosterbier", Category = "Beverages", UnitPrice = 7.7500M, UnitsInStock = 125 },
                    new Product { ProductID = 76, ProductName = "Lakkalikööri", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 57 },
                    new Product { ProductID = 77, ProductName = "Original Frankfurter grüne Soße", Category = "Condiments", UnitPrice = 13.0000M, UnitsInStock = 32 }
                };

            supplierList = new List<Supplier>(){
                    new Supplier {SupplierName = "Exotic Liquids", Address = "49 Gilbert St.", City = "London", Country = "UK"},
                    new Supplier {SupplierName = "New Orleans Cajun Delights", Address = "P.O. Box 78934", City = "New Orleans", Country = "USA"},
                    new Supplier {SupplierName = "Grandma Kelly's Homestead", Address = "707 Oxford Rd.", City = "Ann Arbor", Country = "USA"},
                    new Supplier {SupplierName = "Tokyo Traders", Address = "9-8 Sekimai Musashino-shi", City = "Tokyo", Country = "Japan"},
                    new Supplier {SupplierName = "Cooperativa de Quesos 'Las Cabras'", Address = "Calle del Rosal 4", City = "Oviedo", Country = "Spain"},
                    new Supplier {SupplierName = "Mayumi's", Address = "92 Setsuko Chuo-ku", City = "Osaka", Country = "Japan"},
                    new Supplier {SupplierName = "Pavlova, Ltd.", Address = "74 Rose St. Moonie Ponds", City = "Melbourne", Country = "Australia"},
                    new Supplier {SupplierName = "Specialty Biscuits, Ltd.", Address = "29 King's Way", City = "Manchester", Country = "UK"},
                    new Supplier {SupplierName = "PB Knäckebröd AB", Address = "Kaloadagatan 13", City = "Göteborg", Country = "Sweden"},
                    new Supplier {SupplierName = "Refrescos Americanas LTDA", Address = "Av. das Americanas 12.890", City = "Sao Paulo", Country = "Brazil"},
                    new Supplier {SupplierName = "Heli Süßwaren GmbH & Co. KG", Address = "Tiergartenstraße 5", City = "Berlin", Country = "Germany"},
                    new Supplier {SupplierName = "Plutzer Lebensmittelgroßmärkte AG", Address = "Bogenallee 51", City = "Frankfurt", Country = "Germany"},
                    new Supplier {SupplierName = "Nord-Ost-Fisch Handelsgesellschaft mbH", Address = "Frahmredder 112a", City = "Cuxhaven", Country = "Germany"},
                    new Supplier {SupplierName = "Formaggi Fortini s.r.l.", Address = "Viale Dante, 75", City = "Ravenna", Country = "Italy"},
                    new Supplier {SupplierName = "Norske Meierier", Address = "Hatlevegen 5", City = "Sandvika", Country = "Norway"},
                    new Supplier {SupplierName = "Bigfoot Breweries", Address = "3400 - 8th Avenue Suite 210", City = "Bend", Country = "USA"},
                    new Supplier {SupplierName = "Svensk Sjöföda AB", Address = "Brovallavägen 231", City = "Stockholm", Country = "Sweden"},
                    new Supplier {SupplierName = "Aux joyeux ecclésiastiques", Address = "203, Rue des Francs-Bourgeois", City = "Paris", Country = "France"},
                    new Supplier {SupplierName = "New England Seafood Cannery", Address = "Order Processing Dept. 2100 Paul Revere Blvd.", City = "Boston", Country = "USA"},
                    new Supplier {SupplierName = "Leka Trading", Address = "471 Serangoon Loop, Suite #402", City = "Singapore", Country = "Singapore"},
                    new Supplier {SupplierName = "Lyngbysild", Address = "Lyngbysild Fiskebakken 10", City = "Lyngby", Country = "Denmark"},
                    new Supplier {SupplierName = "Zaanse Snoepfabriek", Address = "Verkoop Rijnweg 22", City = "Zaandam", Country = "Netherlands"},
                    new Supplier {SupplierName = "Karkki Oy", Address = "Valtakatu 12", City = "Lappeenranta", Country = "Finland"},
                    new Supplier {SupplierName = "G'day, Mate", Address = "170 Prince Edward Parade Hunter's Hill", City = "Sydney", Country = "Australia"},
                    new Supplier {SupplierName = "Ma Maison", Address = "2960 Rue St. Laurent", City = "Montréal", Country = "Canada"},
                    new Supplier {SupplierName = "Pasta Buttini s.r.l.", Address = "Via dei Gelsomini, 153", City = "Salerno", Country = "Italy"},
                    new Supplier {SupplierName = "Escargots Nouveaux", Address = "22, rue H. Voiron", City = "Montceau", Country = "France"},
                    new Supplier {SupplierName = "Gai pâturage", Address = "Bat. B 3, rue des Alpes", City = "Annecy", Country = "France"},
                    new Supplier {SupplierName = "Forêts d'érables", Address = "148 rue Chasseur", City = "Ste-Hyacinthe", Country = "Canada"},
                };           
        
        
            // Данные о клиентах или заказах считываются в память из XML-файла с использованием Xlinq:
            string customerListPath = Path.GetFullPath(Path.Combine(dataPath, "customers.xml"));

            customerList = (
                from e in XDocument.Load(customerListPath).
                          Root.Elements("customer")
                select new Customer {
                    CustomerID = (string)e.Element("id"),
                    CompanyName = (string)e.Element("name"),
                    Address = (string)e.Element("address"),
                    City = (string)e.Element("city"),
                    Region = (string)e.Element("region"),
                    PostalCode = (string)e.Element("postalcode"),
                    Country = (string)e.Element("country"),
                    Phone = (string)e.Element("phone"),
                    Fax = (string)e.Element("fax"),
                    Orders = (
                        from o in e.Elements("orders").Elements("order")
                        select new Order {
                            OrderID = (int)o.Element("id"),
                            OrderDate = (DateTime)o.Element("orderdate"),
                            Total = (decimal)o.Element("total") } )
                        .ToArray() } )
                .ToList();
        }
    }
}
