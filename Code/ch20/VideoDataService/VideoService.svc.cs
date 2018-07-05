using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VideoDataService
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class VideoService : IVideoService
    {
        private List<VideoItem> items = new List<VideoItem>();
        public VideoService()
        {
            items.Add(new VideoItem("Writeable Bitmaps","http://mschnlnine.vo.llnwd.net/d1/ch9/8/3/3/7/6/4/MTSL3WriteableBitmaps_2MB_ch9.wmv"));
            items.Add(new VideoItem("Running Out of Browser Apps on the Macintosh", "http://mschnlnine.vo.llnwd.net/d1/ch9/8/7/1/6/6/4/MTSloobOnMac_2MB_ch9.wmv"));
        }

        public IList<VideoItem> GetVideoItems()
        {
            return items;
        }
    }
}
