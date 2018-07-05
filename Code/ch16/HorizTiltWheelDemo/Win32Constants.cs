using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorizTiltWheelDemo
{
    class Win32Constants
    {
        //taken from winuser.h in the Platform SDK
        public const int MK_LBUTTON = 0x0001;
        public const int MK_RBUTTON = 0x0002;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int MK_MBUTTON = 0x0010;
        public const int MK_XBUTTON1 = 0x0020;
        public const int MK_XBUTTON2 = 0x0040;

        public const int WHEEL_DELTA = 120;

        //Windows Vista or Server 2008 or higher required for this one
        public const int SPI_GETWHEELSCROLLCHARS = 0x006C;
    }
}
