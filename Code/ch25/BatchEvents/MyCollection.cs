using System;
using System.Collections.Generic;

namespace BatchEvents
{
    class MyCollection<T>
    {
        List<T> _data = new List<T>();
        int _updateCount = 0;

        public event EventHandler<ItemAddedEventArgs<T>> ItemsAdded;
        List<T> _updatedItems = new List<T>();

        protected void OnItemsAdded(T item)
        {
            if (!IsUpdating)
            {
                if (ItemsAdded != null)
                {
                    ItemsAdded(this, new ItemAddedEventArgs<T>(item));
                }
            }
            else
            {
                _updatedItems.Add(item);
            }
        }

        protected void FireQueuedEvents()
        {
            if (!IsUpdating && _updatedItems.Count > 0)
            {
                //the event args have the ability to contain multiple items
                ItemAddedEventArgs<T> args = new ItemAddedEventArgs<T>();
                foreach (T item in _updatedItems)
                {
                    args.Add(item);
                }
                _updatedItems.Clear();
                if (ItemsAdded != null)
                {
                    ItemsAdded(this, args);
                }
            }
        }

        public bool IsUpdating
        {
            get
            {
                return _updateCount > 0;
            }
        }

        public void BeginUpdate()
        {
            //keep a count in case multiple clients call BeginUpdate, 
            //or it's called recursively, though note that this
            //class is NOT thread safe.
            ++_updateCount;
        }

        public void EndUpdate()
        {
            --_updateCount;
            if (_updateCount == 0)
            {
                //only fire when we're done with all updates
                FireQueuedEvents();
            }
        }

        public void Add(T item)
        {
            _data.Add(item);
            OnItemsAdded(item);
        }
    }
}
