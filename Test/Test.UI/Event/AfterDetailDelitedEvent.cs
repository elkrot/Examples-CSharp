using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UI.Event
{
    class AfterDetailDelitedEvent : PubSubEvent<AfterDtailDelitedEventArgs>
    {
    }

    internal class AfterDtailDelitedEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
