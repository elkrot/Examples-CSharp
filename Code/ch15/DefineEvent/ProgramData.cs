using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DefineEvent
{
    class ProgramData
    {
        private DateTime _startTime;

        /* Follow this pattern when defining your own events:
         * -public event
         * -protected virtual On... function that triggers the event
         */
        public event EventHandler<EventArgs> LoadStarted;
        protected virtual void OnLoadStarted()
        {
            if (LoadStarted != null)
            {
                //no custom data, so just use empty EventArgs
                LoadStarted(this, EventArgs.Empty);
            }
        }

        public event EventHandler<ProgramDataEventArgs> LoadEnded;
        protected virtual void OnLoadEnded(TimeSpan loadTime)
        {
            if (LoadEnded != null)
            {
                LoadEnded(this, new ProgramDataEventArgs(loadTime));
            }
        }
        
        public void BeginLoad()
        {
            _startTime = DateTime.Now;
            Thread thread = new Thread(new ThreadStart(LoadThreadFunc));
            thread.Start();
            //raise LoadStarted event
            OnLoadStarted();
        }

        private void LoadThreadFunc()
        {
            //simulate work
            Thread.Sleep(5000);
            //raise LoadEnded event
            OnLoadEnded(DateTime.Now - _startTime);
        }
    }
}
