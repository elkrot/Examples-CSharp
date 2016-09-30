// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//
// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов, чтобы изменить сведения,
// связанные со сборкой.
//
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//
// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//      Редакция
//      Номер построения
//
// Можно задать все значения или принять номер построения и номер редакции по умолчанию, 
// используя "*", как показано ниже:

[assembly: AssemblyVersion("1.0.*")]

//
// Чтобы подписать сборку, необходимо указать используемый для подписи ключ. Обратитесь 
// к документации по Microsoft .NET Framework для дополнительных сведений о подписывании сборок.
//
// Перечисленные ниже атрибуты служат для управления ключом, используемым для подписи. 
//
// Примечания: 
//   (*) Если ключ не задан, сборку подписать нельзя.
//  (*) KeyName ссылается на ключ, который был установлен в поставщике службы шифрования
//       (CSP) вашего компьютера. 
//   (*) Если указан и файл ключей, и атрибуты имени ключа, то 
//       выполняется следующая обработка:
// (1) Если KeyName можно найти в CSP -- используется этот ключ.
//       (2) Если KeyName не существует, но существует KeyFile, ключ 
//           в файле устанавливается в CSP и используется.
//   (*) Отложенное подписывание - это расширенный параметр; см. документацию Microsoft .NET Framework
//       для получения дополнительных сведений.
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]

[assembly: CLSCompliant(false)]

//Подавление анализа кода
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "PasteXmlAsLinq.Converter.#WriteNewElement(System.Xml.Linq.XElement)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "PasteXmlAsLinq.Converter.#IsSingleLineText(System.Xml.Linq.XElement)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "commandBarControl", Scope = "member", Target = "PasteXmlAsLinq.Connect.#OnConnection(System.Object,Extensibility.ext_ConnectMode,System.Object,System.Array&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Scope = "member", Target = "PasteXmlAsLinq.Converter.#FindNamespaces()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2209:AssembliesShouldDeclareMinimumSecurity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "PasteXmlAsLinq.Connect.#Exec(System.String,EnvDTE.vsCommandExecOption,System.Object&,System.Object&,System.Boolean&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "PasteXmlAsLinq.Connect.#OnConnection(System.Object,Extensibility.ext_ConnectMode,System.Object,System.Array&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "PasteXmlAsLinq.Converter.#CanConvert(System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Scope = "member", Target = "PasteXmlAsLinq.Connect.#Exec(System.String,EnvDTE.vsCommandExecOption,System.Object&,System.Object&,System.Boolean&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1017:MarkAssembliesWithComVisible")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1824:MarkAssembliesWithNeutralResourcesLanguage")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type", Target = "PasteXmlAsLinq.Converter")]
