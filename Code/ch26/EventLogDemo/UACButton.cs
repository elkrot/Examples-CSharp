using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace EventLogDemo
{
    public class UACButton : Button
    {
        private bool _showShield = false;
        public bool ShowShield
        {
            get
            {
                return _showShield;
            }
            set
            {
                bool needToShow = value && !IsElevated();
                //pass in 1 for true in lParam
                if (this.Handle != IntPtr.Zero)
                {
                    Win32.SendMessage(new HandleRef(this, this.Handle), Win32.BCM_SETSHIELD,
                        new IntPtr(0), new IntPtr(needToShow ? 1 : 0));
                    _showShield = needToShow;
                }
            }
        }

        private bool IsElevated()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public UACButton()
        {
            this.FlatStyle = FlatStyle.System;
        }
    }
}
