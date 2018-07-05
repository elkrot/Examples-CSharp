using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBroker
{
    [AttributeUsage(AttributeTargets.Event)]
    class GlobalEventAttribute : Attribute
    {
        public string EventId { get; set; }
        GlobalEventAttribute(string eventId)
        {
            this.EventId = eventId;
        }
    }
}
