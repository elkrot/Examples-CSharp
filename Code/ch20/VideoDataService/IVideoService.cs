using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VideoDataService
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
    [ServiceContract]
    public interface IVideoService
    {
        [OperationContract]
        IList<VideoItem> GetVideoItems();

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class VideoItem
    {
        [DataMember]
        public string Title { get; set; }        

        [DataMember]
        public string Url {get;set;}

        public VideoItem(string title, string url)
        {
            this.Title = title;
            this.Url = url;
        }
    }
}
