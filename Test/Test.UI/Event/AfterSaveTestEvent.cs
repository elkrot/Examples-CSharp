using Prism.Events;

namespace Test.UI.Event
{
    public class AfterTestSaveEvent:PubSubEvent<AfterTestSavedEventArgs>
    {
    }

    public class AfterTestSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
