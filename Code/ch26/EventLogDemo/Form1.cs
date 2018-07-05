using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace EventLogDemo
{
    public partial class Form1 : Form
    {
        private EventLogEntryType _selectedLogEntryType = EventLogEntryType.Information;
        public Form1()
        {
            InitializeComponent();

            radioButtonInfo.Tag = EventLogEntryType.Information;
            radioButtonWarn.Tag = EventLogEntryType.Warning;
            radioButtonError.Tag = EventLogEntryType.Error;
        }

        private void buttonLogIt_Click(object sender, EventArgs e)
        {
            using (EventLog log = new EventLog(Program.LogName, ".", Program.LogSource))
            {
                log.WriteEntry(textBoxMessage.Text, _selectedLogEntryType, (int)numericUpDown1.Value);
            }
        }

        private void buttonCreateSource_Click(object sender, EventArgs e)
        {
            //you can't elevate the current process--you have to start a new one
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Arguments = "-createEventSource";
            startInfo.Verb = "runas";
            try
            {
                Process proc = Process.Start(startInfo);
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error launching the elevated process: " + ex.Message);
            }
        }

        private void OnEntryTypeChanged(object sender, EventArgs e)
        {
            _selectedLogEntryType = (EventLogEntryType)(((RadioButton)sender).Tag);
        }

        private void buttonViewEvents_Click(object sender, EventArgs e)
        {
            using (EventLog log = new EventLog(Program.LogName, ".", Program.LogSource))
            {
                StringBuilder sb = new StringBuilder();
                foreach (EventLogEntry entry in log.Entries)
                {
                    sb.AppendFormat("({0}, {1} {2}) {3}", entry.TimeGenerated, entry.InstanceId, entry.EntryType, entry.Message);
                    sb.AppendLine();
                }
                MessageBox.Show(sb.ToString(),"Existing events");
            }
        }
    }
}
