using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBrokerDemo
{
    //a very simple event broker implementation
    public class EventBroker
    {
        Dictionary<string, List<Delegate>> _subscriptions = new Dictionary<string, List<Delegate>>();

        public void Register(string eventId, Delegate method)
        {
            //associate an event handler for an eventId
            List<Delegate> delegates = null;
            if (!_subscriptions.TryGetValue(eventId, out delegates))
            {
                delegates = new List<Delegate>();
                _subscriptions[eventId] = delegates;
            }
            delegates.Add(method);
        }

        public void Unregister(string eventId, Delegate method)
        {
            //unassociate a specific event handler method for the eventId
            List<Delegate> delegates = null;
            if (_subscriptions.TryGetValue(eventId, out delegates))
            {
                delegates.Remove(method);
                if (delegates.Count == 0)
                {
                    _subscriptions.Remove(eventId);
                }
            }
        }

        public void OnEvent(string eventId, params object[] args)
        {
            //call all event handlers for the given eventId
            List<Delegate> delegates = null;
            if (_subscriptions.TryGetValue(eventId, out delegates))
            {
                foreach (Delegate del in delegates)
                {
                    if (del.Method != null)
                    {
                        if (del.Target != null)
                        {
                            del.DynamicInvoke(args);
                        }
                    }
                }
            }
        }
    }
}
