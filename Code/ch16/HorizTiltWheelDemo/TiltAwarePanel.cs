using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace HorizTiltWheelDemo
{
    class TiltAwarePanel : Panel
    {
        //FYI: There is a .Net SystemParameters class in WPF, but not in Winforms
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

        private int _wheelHPos = 0;
        private readonly uint HScrollChars = 1;
        //scrolling is in terms of lines and characters, which is app-defined
        private const int CharacterWidth = 8;

        public event EventHandler<MouseEventArgs> MouseHWheel;
        
        public TiltAwarePanel()
        {
            //get the system's values for horizontal scrolling
            if (!SystemParametersInfo(Win32Constants.SPI_GETWHEELSCROLLCHARS, 0, ref HScrollChars, 0))
            {
                throw new InvalidOperationException("Unsupported on this platform");
            }

        }

        protected void OnMouseHWheel(MouseEventArgs e)
        {
            //we have to accumulate the value
            _wheelHPos += e.Delta;
            //this method
            while (_wheelHPos >= Win32Constants.WHEEL_DELTA)
            {
                ScrollHorizontal((int)HScrollChars * CharacterWidth);
                _wheelHPos -= Win32Constants.WHEEL_DELTA;
            }

            while (_wheelHPos <= -Win32Constants.WHEEL_DELTA)
            {
                ScrollHorizontal(-(int)HScrollChars * CharacterWidth);
                _wheelHPos += Win32Constants.WHEEL_DELTA;
            }

            if (MouseHWheel != null)
            {
                MouseHWheel(this, e);
            }

            Refresh();
        }

        private void ScrollHorizontal(int delta)
        {
            AutoScrollPosition = 
                new Point(
                    -AutoScrollPosition.X + delta, 
                    -AutoScrollPosition.Y);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.HWnd == this.Handle)
            {
                switch (m.Msg)
                {
                    case Win32Messages.WM_MOUSEHWHEEL:
                        OnMouseHWheel(CreateMouseEventArgs(m.WParam, m.LParam));
                        //0 to indicate we handled it
                        m.Result = (IntPtr)0;
                        return;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            return;
        }

        private MouseEventArgs CreateMouseEventArgs(IntPtr wParam, IntPtr lParam)
        {
            int buttonFlags = LOWORD(wParam);
            MouseButtons buttons = MouseButtons.None;
            buttons |= ((buttonFlags & Win32Constants.MK_LBUTTON) != 0)?MouseButtons.Left:0;
            buttons |= ((buttonFlags & Win32Constants.MK_RBUTTON) != 0) ? MouseButtons.Right : 0;
            buttons |= ((buttonFlags & Win32Constants.MK_MBUTTON) != 0) ? MouseButtons.Middle : 0;
            buttons |= ((buttonFlags & Win32Constants.MK_XBUTTON1) != 0) ? MouseButtons.XButton1 : 0;
            buttons |= ((buttonFlags & Win32Constants.MK_XBUTTON2) != 0) ? MouseButtons.XButton2 : 0;

            int delta = (Int16)HIWORD(wParam);
            int x = LOWORD(lParam);
            int y = HIWORD(lParam);

            return new MouseEventArgs(buttons, 0, x, y, delta);
        }

        private static Int32 HIWORD(IntPtr ptr)
        {
            Int32 val32 = ptr.ToInt32();
            return ((val32 >> 16) & 0xFFFF);
        }

        private static Int32 LOWORD(IntPtr ptr)
        {
            Int32 val32 = ptr.ToInt32();
            return (val32 & 0xFFFF);
        }
    }
}
