// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace UiManagerOutlookAddIn.AddinUtilities
{
    // Этот интерфейс не является CLS-совместимым из-за его свойства Office.
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("53ED8FA5-DBAD-40c4-8068-F20E7858DEB6")]
    [CLSCompliant(false)]
    public interface IFormRegionControls
    {
        Outlook.Inspector Inspector
        {
            get;
            set;
        }

        void SetControlText(String controlName, String text);
    }
}
