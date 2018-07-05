using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;

namespace GenericService
{
    public partial class GenericService : ServiceBase
    {
        Thread _programThread;

        bool _continueRunning = false;
        public GenericService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _continueRunning = true;
            LogString("Service starting");
            _programThread = new Thread(new ThreadStart(ThreadProc));
            _programThread.Start();
        }

        protected override void OnStop()
        {
            _continueRunning = false;

            LogString("Service stopping");
            
        }

        private void LogString(string line)
        {
            using (FileStream fs = new FileStream(@"C:\GenericService_Output.log", FileMode.Append))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(line);
            }
        }

        private void ThreadProc()
        {
            while (_continueRunning)
            {
                Thread.Sleep(5000);
                LogString(string.Format("{0} - Service running.", DateTime.Now));
            }
        }

        
    }
}
