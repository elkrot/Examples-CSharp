using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandUndo
{
    class CreateWidgetCommand : ICommand
    {
        private ICollection<IWidget> _collection;
        private IWidget _newWidget;

        public CreateWidgetCommand(ICollection<IWidget> collection, IWidget newWidget)
        {
            _collection = collection;
            _newWidget = newWidget;
        }

        #region ICommand Members

        public void Execute()
        {
            _collection.Add(_newWidget);
        }

        public void Undo()
        {
            _collection.Remove(_newWidget);
        }

        public string Name
        {
            get { return "Create new widget"; }
        }

        #endregion
    }
}
