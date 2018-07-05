using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandUndo
{
    class DeleteAllWidgetsCommand : ICommand
    {
        private ICollection<IWidget> _collection;
        
        private ICollection<IWidget> _savedCollection;

        public DeleteAllWidgetsCommand(ICollection<IWidget> collection)
        {
            _collection = collection;
        }
        #region ICommand Members

        public void Execute()
        {
            _savedCollection = new List<IWidget>(_collection);
            _collection.Clear();
        }

        public void Undo()
        {
            foreach (IWidget widget in _savedCollection)
            {
                _collection.Add(widget);
            }
            _savedCollection.Clear();
        }

        public string Name
        {
            get { return "Delete all widgets"; }
        }

        #endregion
    }
}
