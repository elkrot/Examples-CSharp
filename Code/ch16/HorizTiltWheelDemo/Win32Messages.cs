using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorizTiltWheelDemo
{
    abstract class Win32Messages
    {
        //taken from winuser.h
        public const int WM_MOUSEWHEEL = 0x020a;

        //taken from winuser.h (Vista or Server 2008 and up required!)
        public const int WM_MOUSEHWHEEL = 0x020e;
    }
}
