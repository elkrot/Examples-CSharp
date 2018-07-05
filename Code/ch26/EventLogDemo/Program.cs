using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace EventLogDemo
{
    static class Program
    {
        public const string LogName = "CSharpHowToLog";
        //if you just want to put your messages in the system-side Application Log, do this:
        //public const string LogName = "Application";
        public const string LogSource = "EventLogDemo";
       
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if (string.Compare("-createEventSource", arg)==0)
                {
                    CreateEventSource();
                    //don't need to show UI--we're already running it, so just exit
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void CreateEventSource()
        {
            //this functionality requires admin privileges--consider doing this during installation
            //of your app, rather than runtime
            if (!EventLog.SourceExists(LogSource))
            {
                //to log to the general application log, pass in null for the application name
                EventSourceCreationData data = new EventSourceCreationData(LogSource, LogName);
                EventLog.CreateEventSource(data);
            }
        }
    }
}
