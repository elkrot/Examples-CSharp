// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using UiManagerOutlookAddIn.AddinUtilities;

namespace UiManagerOutlookAddIn
{
    [ComVisible(true)]
    [ProgId("UiManager.SimpleControl")]
    [Guid("E2F1F0E8-254A-4ddc-A500-273D6EFB172B")]
    public partial class SimpleControl : UserControl
    {
        public SimpleControl()
        {
            InitializeComponent();
        }

        private const String _mailServiceGroup = "mailServiceGroup";

        private void closeCoffeeList_Click(object sender, EventArgs e)
        {
            // Очистите и скройте поле со списком.
            this._coffeeList.Items.Clear();
            this._coffeeGroup.Visible = false;

            // Снова сделайте невидимыми кнопки служебной ленты надстроек.
            UserInterfaceContainer uiContainer =
                Globals.ThisAddIn._uiElements.GetUIContainerForUserControl(
                this);
            uiContainer.HideRibbonControl(_mailServiceGroup);
        }
    }
}
