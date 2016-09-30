// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FormRegionOutlookAddIn
{
    static class Helper
    {
        // Возврат URL-адреса ссылки "Обзор статьи", представленной в заголовке элемента RSS. 
        public static string ParseUrl(Outlook.PostItem item)
        {
            const string lookUpText = "HYPERLINK";
            const string articleStr = "View article";
            string body = item.Body;

            int index = body.IndexOf(lookUpText, 0, body.Length);
            int end = 0;
            // Просмотрите в тексте вхождения "HYPERLINKS" и выберите ссылку на "Обзор статьи...".
            while (true)
            {
                end = body.IndexOf(articleStr, index, body.Length - index);
                int nextIndex = body.IndexOf(lookUpText, index + 1, body.Length - (index + 1));

                if (nextIndex > index && nextIndex < end)
                {
                    index = nextIndex;
                }
                else
                    break;
            }
            // Получить ссылку на статью.
            string url = body.Substring(index + lookUpText.Length + 1, end - index - (lookUpText.Length + 1));

            url = url.Trim('"');

            return url;
        }
    }
}
