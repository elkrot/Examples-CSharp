using System;
using System.Runtime.InteropServices; 

namespace TransparentForm
{
    class Win32
    {
         public const int WM_NCLBUTTONDOWN = 0xA1; 
         public const int HTCAPTION = 0x2; 
 
         [DllImportAttribute ("user32.dll")] 
         public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam); 
    }
}
