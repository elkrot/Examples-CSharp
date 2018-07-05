using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;
using System.Globalization;

namespace RssLib
{
    public class Feed
    {
        public Channel Read(string url)
        {
            WebRequest request = WebRequest.Create(url);
            
            WebResponse response = request.GetResponse();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(response.GetResponseStream());
                Channel channel = new Channel();
                XmlElement rssElem = doc["rss"];
                if (rssElem == null) return null;
                XmlElement chanElem = rssElem["channel"];

                if (chanElem != null)
                {
                    channel.Title = chanElem["title"].InnerText;
                    channel.Link = chanElem["link"].InnerText;
                    channel.Description = chanElem["description"].InnerText;
                    channel.Culture = CultureInfo.CreateSpecificCulture(chanElem["language"].InnerText);
                    channel.Items = new List<Item>();
                    XmlNodeList itemElems = chanElem.GetElementsByTagName("item");
                    foreach (XmlElement itemElem in itemElems)
                    {
                        Item item = new Item();
                        item.Title = itemElem["title"].InnerText;
                        item.Link = itemElem["link"].InnerText;
                        item.Description = itemElem["description"].InnerText;
                        item.PubDate = itemElem["pubDate"].InnerText;
                        channel.Items.Add(item);
                    }
                }
                return channel;
            }
            catch (XmlException)
            {
                return null;
            }
        }

        public void Write(Stream stream, Channel channel)
        {
            XmlWriter writer = XmlTextWriter.Create(stream);
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteStartElement("channel");
            writer.WriteElementString("title", channel.Title);
            writer.WriteElementString("link", channel.Link);
            writer.WriteElementString("description", channel.Description);
            writer.WriteElementString("language", channel.Culture.ToString());
            foreach (RssLib.Item item in channel.Items)
            {
                writer.WriteStartElement("item");
                writer.WriteElementString("title", item.Title);
                writer.WriteElementString("link", item.Link);
                writer.WriteElementString("description", item.Description);
                writer.WriteElementString("pubDate", item.PubDate);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.Flush();       
        }
    }
}
