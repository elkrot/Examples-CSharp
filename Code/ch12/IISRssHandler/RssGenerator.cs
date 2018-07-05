using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;

namespace IISRssHandler
{
    class RssGenerator : System.Web.IHttpHandler
    {
        RssLib.Feed feed = new RssLib.Feed();

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            CreateFeedContent(context.Response.OutputStream);    
        }

        #endregion

        private void CreateFeedContent(Stream outStream)
        {
            RssLib.Channel channel = GetFeedFromDB();

            feed.Write(outStream, channel);
            
        }

        private RssLib.Channel GetFeedFromDB()
        {
            using (IDataReader reader = CreateDataSet().CreateDataReader())
            {
                RssLib.Channel channel = new RssLib.Channel();
                channel.Title = "Test Feed";
                channel.Link = "http://localhost";
                channel.Description = "A sample RSS generator";
                channel.Culture = CultureInfo.CurrentCulture;
                channel.Items = new List<RssLib.Item>();
                while (reader.Read())
                {
                    RssLib.Item item = new RssLib.Item();
                    item.Title = reader["title"] as string;
                    item.Link = reader["link"] as string;
                    item.PubDate = reader["pubDate"] as string;
                    item.Description = reader["description"] as string;
                    channel.Items.Add(item);
                }
                return channel;
            }
        }

        private static DataSet CreateDataSet()
        {
            //simulate database with a dataset
            DataSet dataSet = new DataSet();
            DataTable contentTable = new DataTable("ContentItems");
            dataSet.Tables.Add(contentTable);
            contentTable.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("title", typeof(string)),
                    new DataColumn("link", typeof(string)),
                    new DataColumn("pubDate", typeof(string)),
                    new DataColumn("description", typeof(string)),
                });
            contentTable.Rows.Add("Title 1", "http://example.com/link1", DateTime.UtcNow.ToString(), "Some sample content");
            contentTable.Rows.Add("Title 2", "http://example.com/link2", DateTime.UtcNow.ToString(), "Some more sample content");
            return dataSet;
        }

    }
}
