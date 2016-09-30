using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Resources;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов для изменения сведений,
// связанных со сборкой.
[assembly: AssemblyTitle("EmployeeTracker")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("EmployeeTracker")]
[assembly: AssemblyCopyright("Copyright © 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

// Установка значения false атрибута ComVisible делает типы этой сборки невидимыми 
// для компонентов COM. Если необходимо обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение true для требуемого типа.
[assembly: ComVisible(false)]

//Для построения приложений с поддержкой локализации установите 
//<UICulture>CultureYouAreCodingWith</UICulture> в файле .csproj
//внутри <PropertyGroup>. Например, при использовании американского английского
//в своих исходных файлах установите <UICulture> в en-US. Затем отмените преобразование в комментарий
//атрибута NeutralResourceLanguage ниже. Обновите "en-US" в
//строка внизу для обеспечения соответствия настройки UICulture в файле проекта.

[assembly: NeutralResourcesLanguage("en-US")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //расположение словарей ресурсов для конкретной темы
    //(используется если ресурс не найден на странице, 
    // или в словарях ресурсов приложения)
    ResourceDictionaryLocation.SourceAssembly //расположение словаря общих ресурсов
    //(используется если ресурс не найден на странице, 
    // в приложении или в каких-либо словарях ресурсов для конкретной темы)
)]


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
