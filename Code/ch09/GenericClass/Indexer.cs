using System;
using System.Collections.Generic;

namespace GenericClass
{
    class Indexer<T> where T:class
    {
        struct ItemStruct
        {
            public string key;
            public T value;
            public ItemStruct(string key, T value)
            {
                this.key = key;
                this.value = value;
            }
        };

        List<ItemStruct> _items = new List<ItemStruct>();

        public T Find(string key)
        {
            foreach (ItemStruct itemStruct in _items)
            {
                if (itemStruct.key == key)
                {
                    return itemStruct.value;
                }
            }
            return null;
        }

        public void Add(string key, T value)
        {
            _items.Add(new ItemStruct(key, value));
        }
    }
}