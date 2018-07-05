using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SplashScreenWinForms
{
    class Win32
    {
        [DllImport("winmm.dll", EntryPoint = "PlaySound", CharSet = CharSet.Auto)]
        public static extern int PlaySound(String pszSound, int hmod, int flags);

        [Flags]
        public enum Soundflags
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_MEMORY = 0x0004, 
            SND_LOOP = 0x0008, 
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_ALIAS = 0x00010000,
            SND_ALIAS_ID = 0x00110000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004,
            SND_PURGE = 0x0040,
            SND_APPLICATION = 0x0080
        }
    }
}
