using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов для изменения сведений,
// связанных со сборкой.
[assembly: AssemblyTitle("FormRegionOutlookAddIn")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("FormRegionOutlookAddIn")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Установка значения false атрибута ComVisible делает типы этой сборки невидимыми 
// для компонентов COM. Если необходимо обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение true для требуемого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект видим для COM
[assembly: Guid("2e4ee1e2-7c17-40a0-838e-ec64b4f7ab70")]

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

internal static class DesignTimeConstants {
    internal const string Version = "10.0.0.0";
    internal const string DesignerAssembly = "Microsoft.VisualStudio.Tools.Office.Designer, Version=" + Version + ", Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
    internal const string TypeCodeDomSerializer = "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design";
    internal const string RibbonTypeSerializer = "Microsoft.VisualStudio.Tools.Office.Ribbon.Serialization.RibbonTypeCodeDomSerializer, " + DesignerAssembly;
    internal const string FormRegionTypeCodeDomSerializer = "Microsoft.VisualStudio.OfficeTools.Outlook2007.Design.FormRegionControlTypeCodeDomSerializer, " + DesignerAssembly;
}
