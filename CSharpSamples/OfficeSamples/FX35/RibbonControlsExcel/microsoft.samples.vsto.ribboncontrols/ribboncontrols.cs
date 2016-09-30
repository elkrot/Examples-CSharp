﻿// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;


namespace RibbonControlsExcelWorkbook
{
    public partial class RibbonControls : OfficeRibbon
    {
        ActionsPaneControl1 AP1 = new ActionsPaneControl1();
        
        public RibbonControls()
        {
            InitializeComponent();
        }

        // Событие Load создается при загрузке ленты
        // Панель действий добавляется к проекту, но остается скрытой, пока не будет отображена пользователем
        private void RibbonControls_Load(object sender, RibbonUIEventArgs e)
        {
            Globals.ThisWorkbook.ActionsPane.Controls.Add(AP1);
            AP1.Hide();
            Globals.ThisWorkbook.Application.DisplayDocumentActionTaskPane = false;
        }

        // По щелчку мыши событие ToggleButton отображает или скрывает панель действий
        private void btnActionsPane_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisWorkbook.Application.DisplayDocumentActionTaskPane = btnActionsPane.Checked;
            AP1.Visible = btnActionsPane.Checked;
        }

        private void btnHappy_Click(object sender, RibbonControlEventArgs e)
        {
            if (btnHappy.Checked)
            {
                ChangeFace("J");
                btnNeutral.Checked = false;
                btnSad.Checked = false;
            }
            else
            {
                ChangeFace("");
            }
        }

        private void btnNeutral_Click(object sender, RibbonControlEventArgs e)
        {
            if (btnNeutral.Checked)
            {
                ChangeFace("K");
                btnHappy.Checked = false;
                btnSad.Checked = false;
            }
            else
            {
                ChangeFace("");
            }
        }

        private void btnSad_Click(object sender, RibbonControlEventArgs e)
        {
            if (btnSad.Checked)
            {
                ChangeFace("L");
                btnHappy.Checked = false;
                btnNeutral.Checked = false;
            }
            else
            {
                ChangeFace("");
            }
        }

        private void ChangeFace(string faceLetter)
        {
            var xlCell = Globals.Sheet1.Range["A1", "A1"];
            xlCell.FormulaR1C1 = faceLetter;
            xlCell.Font.Name = "Wingdings";
            xlCell.Font.FontStyle = "Regular";
            xlCell.Font.Size = 24;
            xlCell.Font.Color = -16776361;
            xlCell.Select();
        }

        private void btnLeft_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Range xlcell;

            xlcell = Globals.Sheet1.Range["A3","A3"];
            xlcell.Value2 = "Left";
            xlcell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
            xlcell.Select();
        }

        private void btnCenter_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Range xlcell;

            xlcell = Globals.Sheet1.Range["A3", "A3"];
            xlcell.Value2 = "Center";
            xlcell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            xlcell.Select();
        }

        private void btnRight_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Range xlcell;

            xlcell = Globals.Sheet1.Range["A3", "A3"];
            xlcell.Value2 = "Right";
            xlcell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;
            xlcell.Select();
        }

        private void sbtnAlign_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Range xlcell;

            xlcell = Globals.Sheet1.Range["A3", "A3"];
            xlcell.Value2 = "Center";
            xlcell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            xlcell.Select();
        }

        // При выборе пользователем элемента из раскрывающегося списка создается событие изменения выбора
        // из раскрывающегося списка.  
        private void ddFormatChart_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            var xlChart = (Microsoft.Office.Tools.Excel.Chart)Globals.Sheet1.Controls["Chart_1"];

            switch (ddFormatChart.SelectedItem.Label)
            {
                case "Bar":
                    xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DBarClustered;
                    break;
                case "Column":
                    xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered;
                    break;
                case "Area":
                    xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DArea;
                    break;
                case "Pie":
                    xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;
                    break;
            }
        }

        // Событие изменения текста создается при выборе пользователем элемента 
        // из раскрывающегося списка или при вводе нового элемента в поле ввода. Обработчик событий
        // выполняет два дела. Во-первых, он выполняет поиск текстовой строки на листе.
        // Затем выясняет, не содержится ли строка в раскрывающемся списке. Если это 
        // не так, строка добавляется к списку.
        private void cbMRUFind_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var xlcell = Globals.ThisWorkbook.Application.Cells.Find(cbMRUFind.Text, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, Type.Missing, Type.Missing, Type.Missing);
            if (xlcell == null)
            {
                System.Windows.Forms.MessageBox.Show("Text Not Found");
            }
            else
            {
                xlcell.Select();
                System.Windows.Forms.MessageBox.Show("Search text: " + cbMRUFind.Text + " found in cell " 
                    + xlcell.get_Address(Type.Missing,Type.Missing,Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,Type.Missing,Type.Missing).ToString());
            }

            bool newSearchText = true;
            foreach (RibbonDropDownItem ddI in cbMRUFind.Items)
            {
                if (cbMRUFind.Text == ddI.Label)
                {
                    newSearchText = false;
                }
            }
            if (newSearchText)  
            {
                var item = new RibbonDropDownItem { Label = cbMRUFind.Text };
                cbMRUFind.Items.Add(item);
                System.Windows.Forms.MessageBox.Show("New item: " + item.Label + " added to ComboBox");
            }
        }

        //  Событие загрузки элементов коллекции создается каждый раз, когда пользователь щелкает 
        //  раскрывающийся список коллекции. Это позволяет загружать элементы коллекции
        //  динамически во время выполнения.
        private void galShapes_ItemsLoading(object sender, RibbonControlEventArgs e)
        {
            if (galShapes.Items.Count == 0)
            {
                var galleryItems = galShapes.Items;
                galleryItems.Add(new RibbonDropDownItem { Label = "Avocado", Image = Properties.Resources.AvocadoGreen });
                galleryItems.Add(new RibbonDropDownItem { Label = "Berry", Image = Properties.Resources.Berry });
                galleryItems.Add(new RibbonDropDownItem { Label = "BurntRed", Image = Properties.Resources.BurntRed });
                galleryItems.Add(new RibbonDropDownItem { Label = "Gold", Image = Properties.Resources.Gold });
                galleryItems.Add(new RibbonDropDownItem { Label = "Gray", Image = Properties.Resources.Gray });
                galleryItems.Add(new RibbonDropDownItem { Label = "Green", Image = Properties.Resources.Green });
                galleryItems.Add(new RibbonDropDownItem { Label = "Orange", Image = Properties.Resources.Orange });
                galleryItems.Add(new RibbonDropDownItem { Label = "Purple", Image = Properties.Resources.Purple });
                galleryItems.Add(new RibbonDropDownItem { Label = "Red", Image = Properties.Resources.Red });
                galleryItems.Add(new RibbonDropDownItem { Label = "Sapphire", Image = Properties.Resources.Sapphire });
                galleryItems.Add(new RibbonDropDownItem { Label = "SkyBlue", Image = Properties.Resources.SkyBlue });
                galleryItems.Add(new RibbonDropDownItem { Label = "Teal", Image = Properties.Resources.Teal });
                galleryItems.Add(new RibbonDropDownItem { Label = "Turquiose", Image = Properties.Resources.Turquoise });
                galleryItems.Add(new RibbonDropDownItem { Label = "Violet", Image = Properties.Resources.Violet });
            }
        }
    
        // Событие щелчка коллекции создается при выборе пользователем элемента коллекции
        private void galShapes_Click(object sender, RibbonControlEventArgs e)
        {
            galShapes.Image = galShapes.SelectedItem.Image;
            
            var oldShape = Globals.Sheet1.Shapes.Item("Picture 1");

            string tempImageName = System.IO.Path.GetTempFileName();
            galShapes.SelectedItem.Image.Save(tempImageName);

            var newShape = Globals.Sheet1.Shapes.AddPicture(tempImageName,
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoTrue,
                oldShape.Left, oldShape.Top, oldShape.Width, oldShape.Height);

            oldShape.Delete();
            newShape.Name = "Picture 1";
        }

        // Событие загрузки пунктов меню создается, если у меню есть динамическое свойство, 
        // установленное в значение True,  а пользователь щелкает раскрывающееся меню. Это позволяет
        // динамически добавлять элементы меню во время выполнения.
        private void mDynamicMenu_ItemsLoading(object sender, RibbonControlEventArgs e)
        {
            mDynamicMenu.Items.Clear();

            if (cbButton.Checked)
            {
                mDynamicMenu.Items.Add(new RibbonButton { Label = "Button" });
            }

            if (cbSeparator.Checked)
            {
                mDynamicMenu.Items.Add(new RibbonSeparator { Title = "Menu Separator" });
            }

            if (cbSubMenu.Checked)
            {
                RibbonButton subButton = new RibbonButton {
                    Label = "SubMenu Button",
                    OfficeImageId = "_3DMaterialMetal",
                    Description = "Large Button in a Sub Menu" 
                };
                RibbonMenu mSubMenu = new RibbonMenu {
                    ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge,
                    Label = "Sub Menu"
                };
                mSubMenu.Items.Add(subButton);
                mDynamicMenu.Items.Add(mSubMenu);
            }
        }
    }
}
