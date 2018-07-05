using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CommandUndo
{
    class MoveCommand : ICommand
    {
        private Point _originalLocation;
        private Point _newLocation;
        private IWidget _widget;

        public MoveCommand(IWidget widget, Point originalLocation, Point newLocation)
        {
            _widget = widget;
            _originalLocation = originalLocation;
            _newLocation = newLocation;
        }

        #region ICommand Members

        public void Execute()
        {
            _widget.Location = _newLocation;
        }

        public void Undo()
        {
            _widget.Location = _originalLocation;
        }

        public string Name
        {
            get { return "Move widget"; }
        }

        #endregion
    }
}
