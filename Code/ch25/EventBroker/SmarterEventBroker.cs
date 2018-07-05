using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EventBroker
{
    class SmarterEventBroker
    {
        public void Register(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj != null)
            {
                //look for our attribute on all events
                Type type = obj.GetType();
                foreach (EventInfo info in type.GetEvents(BindingFlags.Public | BindingFlags.Instance))
                {
                    object[] attributes = info.GetCustomAttributes(typeof(GlobalEventAttribute), true);
                    foreach (GlobalEventAttribute attr in attributes)
                    {

                    }
                }
            }

        }
    }
}
