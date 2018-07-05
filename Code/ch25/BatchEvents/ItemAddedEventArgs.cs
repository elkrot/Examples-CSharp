using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchEvents
{
    class ItemAddedEventArgs<T> : EventArgs
    {
        private IList<T> _items = new List<T>();
        public IList<T> Items { get { return _items; } }

        public ItemAddedEventArgs()
        {
        }
    
        public ItemAddedEventArgs(T item)
        {
            _items.Add(item);
        }

        public void Add(T item)
        {
            _items.Add(item);
        }
    }
}
