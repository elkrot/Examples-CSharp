using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Office.Tools.Excel;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов для изменения сведений,
// связанных со сборкой.
[assembly: AssemblyTitle("RibbonControlsExcelWorkbook")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("RibbonControlsExcelWorkbook")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2007")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Установка значения false атрибута ComVisible делает типы этой сборки невидимыми 
// для компонентов COM. Если необходимо обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение true для требуемого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект видим для COM
[assembly: Guid("962463d0-c18e-48d4-a415-65a8c472939c")]

// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//      Номер построения
//      Редакция
//
// Можно задать все значения или принять номер построения и номер редакции по умолчанию 
// с помощью знака "*", как показано ниже:
// [сборка: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// 
// Атрибут ExcelLocale1033 управляет языковым стандартом, передаваемым в
// объектную модель Excel. Задание значения True для ExcelLocale1033 заставляет объектную модель Excel 
// действовать одинаково во всех языковых стандартах, что соответствует поведению Visual Basic для 
// приложений. Задание для ExcelLocale1033 значения False заставляет объектную модель Excel
// действовать по-разному, если у пользователей разные языковые настройки, что соответствует 
// поведению набора средств Visual Studio для Office версии 2003. Это может привести к непредвиденным 
// результатам в данных, зависящих от языка системы, например в названиях формул и форматах дат.
// 
// [сборка: ExcelLocale1033(true)]