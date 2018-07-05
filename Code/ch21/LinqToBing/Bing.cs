using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToBing
{
    class SearchResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }

    class Bing
    {
        public string AppId { get; set; }
        const string BingNameSpace = "http://schemas.microsoft.com/LiveSearch/2008/04/XML/web";

        public Bing(string appId)
        {
            this.AppId = appId;
        }

        public IEnumerable<SearchResult> QueryWeb(string query)
        {
            string escaped = Uri.EscapeUriString(query);
            string url = BuildUrl(escaped);
            XDocument doc = XDocument.Load(url);
            XNamespace ns = BingNameSpace;
            IEnumerable<SearchResult> results =
                from sr in doc.Descendants(ns + "WebResult")
                select new SearchResult
                {
                    Title = sr.Element(ns + "Title").Value,
                    Url = sr.Element(ns + "Url").Value,
                    Description = sr.Element(ns + "Description").Value
                };
            return results;
        }

        string BuildUrl(string query)
        {
            return "http://api.search.live.net/xml.aspx?"
                + "AppId=" + AppId
                + "&Query=" + query
                + "&Sources=Web"
                + "&Version=2.0"
                + "&Web.Count=10"
                + "&Web.Offset=0"
                ;
        }
    }
}
