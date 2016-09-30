// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FormRegionOutlookAddIn
{
    partial class RssRegion
    {
        #region Фабрика областей формы

        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass(Microsoft.Office.Tools.Outlook.FormRegionMessageClassAttribute.PostRss)]
        [Microsoft.Office.Tools.Outlook.FormRegionName("FormRegionOutlookAddIn.RssRegion")]
        public partial class RssRegionFactory
        {
            // Возникает перед инициализацией области формы.
            // Чтобы исключить появление области формы, задайте для параметра e.Cancel значение true.
            // Используйте e.OutlookItem для получения ссылки на текущий элемент Outlook.
            private void RssRegionFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // Возникает перед отображением области формы.
        // Используйте this.OutlookItem для получения ссылки на текущий элемент Outlook.
        // Используйте this.OutlookFormRegion для получения ссылки на область формы.
        private void RssRegion_FormRegionShowing(object sender, System.EventArgs e)
        {
            this.RssRegionSplitContainer.Panel2Collapsed = true;

            Outlook.PostItem rssItem = (Outlook.PostItem)this.OutlookItem;

            this.webBrowserRss.Navigate(Helper.ParseUrl(rssItem));
        }

        // Возникает перед закрытием области формы.
        // Используйте this.OutlookItem для получения ссылки на текущий элемент Outlook.
        // Используйте this.OutlookFormRegion для получения ссылки на область формы.
        private void RssRegion_FormRegionClosed(object sender, System.EventArgs e)
        {
        }

        // По щелчку "SearchSimilarTopicsbutton" открывается "webBrowserSearch" в отдельной области.
        private void searchSimilarTopicsButton_Click(object sender, EventArgs e)
        {
            Outlook.PostItem rssItem = (Outlook.PostItem)this.OutlookItem;

            this.searchSimilarTopicsButton.Visible = false;

            this.RssRegionSplitContainer.Panel2Collapsed = false;

            // Ищите соответствующие заголовки, поместив заголовок в кавычки ("").
            this.webBrowserSearch.Navigate(string.Concat("http://www.bing.com/search?q=\"", rssItem.Subject, "\""));

            this.RssRegionSplitContainer.SplitterDistance = (this.OutlookFormRegion.Inspector.Width / 2);

            this.searchWindowProgressBar.Visible = true;
        }

        // Возвращение к Rss-статье в области "Обзор статьи”.
        private void viewRssBackButton_Click(object sender, EventArgs e)
        {
            this.webBrowserRss.GoBack();
        }

        // Событие, определяющее свойства "ViewRssProgressBar".
        private void webBrowserRss_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.viewRssProgressBar.Visible = true;

            this.viewRssProgressBar.Value = 0;
        }

        private void webBrowserRss_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // Свойство "viewRssProgressBar.Visible" установлено в событиях Navigating и DocumentCompleted.
            // Чтобы его обновление выполнялось правильно, координируйте эти три события.

            if (this.viewRssProgressBar.Visible)
            {
                // -1 указывает, что загрузка выполнена.
                if (e.CurrentProgress == -1)
                {
                    this.viewRssProgressBar.Value = 100;
                }
                else
                {
                    this.viewRssProgressBar.Value = (int)((100 * e.CurrentProgress) / e.MaximumProgress);
                }
            }

        }

        // Событие, определяющее свойства "ViewRssProgressBar".
        private void webBrowserRss_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.viewRssProgressBar.Visible = false;

            this.viewRssProgressBar.Value = 0;
        }

        // Скройте область "Результаты поиска" и разверните область "Обзор статьи".
        private void hideSearchResultsButton_Click(object sender, EventArgs e)
        {
            this.RssRegionSplitContainer.Panel2Collapsed = true;

            this.searchSimilarTopicsButton.Visible = true;
        }

        // Возвращение к результатам поиска.
        private void searchResultsBackButton_Click(object sender, EventArgs e)
        {
            this.webBrowserSearch.GoBack();
        }

        // Событие для задания свойств "ViewRssProgressBar"
        private void webBrowserSearch_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.searchWindowProgressBar.Visible = true;

            this.searchWindowProgressBar.Value = 0;
        }

        private void webBrowserSearch_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // Свойство "searchWindowProgressBar.Visible" установлено в событиях Navigating и DocumentCompleted.
            // Чтобы его обновление выполнялось правильно, эти три события координируются.

            if (this.searchWindowProgressBar.Visible)
            {
                // -1 указывает, что загрузка выполнена.
                if (e.CurrentProgress == -1)
                {
                    this.searchWindowProgressBar.Value = 100;
                }
                else
                {
                    this.searchWindowProgressBar.Value = (int)((100 * e.CurrentProgress) / e.MaximumProgress);
                }
            }
        }

        // Событие, определяющее свойства ProgressBar
        private void webBrowserSearch_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.searchWindowProgressBar.Visible = false;

            this.searchWindowProgressBar.Value = 0;
        }
    }
}
