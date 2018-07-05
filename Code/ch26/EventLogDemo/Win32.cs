using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EventLogDemo
{
    class Win32
    {
        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        //defined in CommCtrl.h
        public const UInt32 BCM_SETSHIELD = 0x0000160C;
    }
}
