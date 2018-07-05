using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoWaitCursor
{
    class AutoWaitCursor : IDisposable
    {
        private Control _target;
        private Cursor _prevCursor = Cursors.Default;

        public AutoWaitCursor(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            _target = control;
            _prevCursor = _target.Cursor;
            _target.Cursor = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            _target.Cursor = _prevCursor;
        }
    }
}
