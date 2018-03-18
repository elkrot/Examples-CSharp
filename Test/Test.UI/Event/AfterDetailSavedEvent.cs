﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UI.Event
{
    public class AfterDetailSavedEvent : PubSubEvent<AfterDtailSavedEventArgs>
    {
    }

    public class AfterDtailSavedEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
