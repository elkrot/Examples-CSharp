// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Vbe.Interop.Forms;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

// Необходимо добавить ссылку на Microsoft.Vbe.Interop.Forms.
// Вкладка COM: Библиотека Объектов Microsoft Forms 2.0 (первая, если всего их несколько).
namespace UiManagerOutlookAddIn
{

    [ComVisible(true)]
    [Guid("38F28415-204F-479a-B9B2-A386A462F7DE")]
    [ProgId("UiManager.FormRegionConnector")]
    public class FormRegionConnector : Outlook.FormRegionStartup
    {

        #region Поля

        private const String _formRegionName = "SimpleFormRegion";

        #endregion


        #region Реализация FormRegionStartup

        // Этот метод не является CLSCompliant, так как имеет параметры Office.
        [CLSCompliant(false)]
        public object GetFormRegionStorage(
            string FormRegionName, object Item, int LCID,
            Outlook.OlFormRegionMode FormRegionMode, 
            Outlook.OlFormRegionSize FormRegionSize)
        {
            Application.DoEvents();
            switch (FormRegionName)
            {
                case _formRegionName:
                    return Properties.Resources.SimpleFormRegionOfs;
                default:
                    return null;
            }
        }

        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public object GetFormRegionIcon(
            string FormRegionName, int LCID, Outlook.OlFormRegionIcon Icon)
        {
            return PictureConverter.ImageToPictureDisp(
                Properties.Resources.espressoCup);
        }

        public object GetFormRegionManifest(string FormRegionName, int LCID)
        {
            return Properties.Resources.SimpleFormRegionXml;
        }

        // Этот метод не является CLS-совместимым из-за его параметра Office.
        [CLSCompliant(false)]
        public void BeforeFormRegionShow(Outlook.FormRegion FormRegion)
        {
            if (FormRegion != null)
            {
                // Создайте новую программу-оболочку для элементов управления области формы, 
                // и добавьте ее в коллекцию.
                Globals.ThisAddIn._uiElements.AttachFormRegion(
                    FormRegion.Inspector, new FormRegionControls(FormRegion));
            }
        }

        #endregion

    }
}
