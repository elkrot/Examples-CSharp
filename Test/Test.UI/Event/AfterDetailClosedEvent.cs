using Prism.Events;

namespace Test.UI.Event
{
    public class AfterDetailClosedEvent : PubSubEvent<AfterDtailClosedEventArgs>
    {
    }

    public class AfterDtailClosedEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
