using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefineEvent
{
    //your own EventArgs classes must be derived from EventArgs proper
    class ProgramDataEventArgs : EventArgs
    {
        public TimeSpan LoadTime { get; private set; }
        public ProgramDataEventArgs(TimeSpan loadTime)
        {
            this.LoadTime = loadTime;
        }
    }
}
