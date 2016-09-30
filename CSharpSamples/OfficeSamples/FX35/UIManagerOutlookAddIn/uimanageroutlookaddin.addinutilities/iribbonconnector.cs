// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace UiManagerOutlookAddIn.AddinUtilities
{
    // Этот интерфейс не является CLS-совместимым из-за его свойства Office.
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("43189577-8667-4c8f-8167-EBF23CC285CB")]
    [CLSCompliant(false)]
    public interface IRibbonConnector
    {
        Microsoft.Office.Core.IRibbonUI Ribbon
        {
            get;
            set;
        }
    }


    // Regasm не зарегистрирует сборку, содержащую только интерфейсы.
    // Необходимо определить создаваемый COM класс, чтобы получить typelib.
    // Этот класс ни для чего не используется, так как идет
    // реализация интерфейса в другой сборке.
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [CLSCompliant(false)]
    public class RibbonConnectorPlaceholder : IRibbonConnector 
    {
        public Microsoft.Office.Core.IRibbonUI Ribbon
        {
            get { return null; }
            set { }
        }
    }

}
