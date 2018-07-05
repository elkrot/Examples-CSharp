using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandUndo
{
    public partial class Form1 : Form
    {
        private CommandHistory _history = new CommandHistory();
        private List<IWidget> _widgets = new List<IWidget>();
        private bool _isDragging = false;
        private IWidget _dragWidget = null;
        private Point _prevMousePt;
        private Point _originalLocation;
        private Point _newLocation;
        
        public Form1()
        {
            InitializeComponent();

            panelSurface.MouseDoubleClick += new MouseEventHandler(panelSurface_MouseDoubleClick);
            panelSurface.Paint += new PaintEventHandler(panelSurface_Paint);
            panelSurface.MouseMove += new MouseEventHandler(panelSurface_MouseMove);
            panelSurface.MouseDown += new MouseEventHandler(panelSurface_MouseDown);
            panelSurface.MouseUp += new MouseEventHandler(panelSurface_MouseUp);

            editToolStripMenuItem.DropDownOpening += new EventHandler(editToolStripMenuItem_DropDownOpening);
            undoToolStripMenuItem.Click += new EventHandler(undoToolStripMenuItem_Click);
        }

        void panelSurface_MouseDown(object sender, MouseEventArgs e)
        {
            IWidget widget = GetWidgetUnderPoint(e.Location);
            if (widget != null)
            {
                _dragWidget = widget;
                _isDragging = true;
                _prevMousePt = e.Location;
                _newLocation = _originalLocation = _dragWidget.Location;
            }
        }

        void panelSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
            {
                IWidget widget = GetWidgetUnderPoint(e.Location);
                if (widget != null)
                {
                    panelSurface.Cursor = Cursors.SizeAll;
                }
                else
                {
                    panelSurface.Cursor = Cursors.Default;
                }
            }
            else if (_dragWidget != null)
            {
                Point offset = new Point(e.Location.X - _prevMousePt.X,
                    e.Location.Y - _prevMousePt.Y);
                
                _prevMousePt = e.Location;
                
                _newLocation.Offset(offset);
                //update the widget temporarily as we move -- not a command in this case
                _dragWidget.Location = _newLocation;
             
                Refresh();
            }
        }

        void panelSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                //now perform the command so that Undo restores to location
                //before we started dragging
                RunCommand(new MoveCommand(_dragWidget, _originalLocation, _newLocation));
            }
            _isDragging = false;
            _dragWidget = null;
        }

        void panelSurface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CreateNewWidget(e.Location);
        }

        private IWidget GetWidgetUnderPoint(Point point)
        {
            foreach (IWidget widget in _widgets)
            {
                if (widget.BoundingBox.Contains(point))
                {
                    return widget;
                }
            }
            return null;
        }

        void panelSurface_Paint(object sender, PaintEventArgs e)
        {
            foreach (IWidget widget in _widgets)
            {
                widget.Draw(e.Graphics);
            }
        }

        //menu handling
        void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = _history.CanUndo;
            if (_history.CanUndo)
            {
                undoToolStripMenuItem.Text = "&Undo " + _history.MostRecentCommandName;
            }
            else
            {
                undoToolStripMenuItem.Text = "&Undo";
            }
        }

        void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoMostRecentCommand();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewWidget(new Point(0, 0));
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCommand(new DeleteAllWidgetsCommand(_widgets));
            Refresh();
        }

        private void CreateNewWidget(Point point)
        {
            RunCommand(new CreateWidgetCommand(_widgets, new Widget(point)));
            Refresh();
        }

        private void RunCommand(ICommand command)
        {
            _history.PushCommand(command);
            command.Execute();
        }

        private void UndoMostRecentCommand()
        {
            ICommand command = _history.PopCommand();
            command.Undo();
            Refresh();
        }
    }
}
