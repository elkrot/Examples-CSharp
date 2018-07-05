using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueueDemo
{
    //usually, priority queues are implemented as heaps, but this turns out
    //to be problematic when we want to enumerate over the structure
    //as a heap does not maintain a strict order
    //here's an alternate implementation using just existing data structures
    class PriorityQueue<TPriority, TObject> : ICollection, IEnumerable<TObject>
    {
        private SortedDictionary<TPriority, Queue<TObject>> _elements;
        
        //same types of constructors as Queue<T> class

        public PriorityQueue()
        {
            _elements = new SortedDictionary<TPriority, Queue<TObject>>();
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            _elements = new SortedDictionary<TPriority, Queue<TObject>>(comparer);
        }

        public PriorityQueue(PriorityQueue<TPriority, TObject> queue)
            :this()
        {
            foreach (var pair in queue._elements)
            {
                _elements.Add(pair.Key, new Queue<TObject>(pair.Value));
            }
        }

        public PriorityQueue(PriorityQueue<TPriority, TObject> queue, IComparer<TPriority> comparer)
            :this(comparer)
        {
            foreach (var pair in queue._elements)
            {
                _elements.Add(pair.Key, new Queue<TObject>(pair.Value));
            }
        }

        public void Enqueue(TPriority priority, TObject item)
        {
            Queue<TObject> queue = null;
            if (!_elements.TryGetValue(priority, out queue))
            {
                queue = new Queue<TObject>();
                _elements[priority] = queue;
            }
            queue.Enqueue(item);
        }

        public TObject Dequeue()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty");
            }
            SortedDictionary<TPriority, Queue<TObject>>.Enumerator enumerator = _elements.GetEnumerator();
            enumerator.MoveNext();//must succeed since we've already established that there is at least one element
            
            Queue<TObject> queue = enumerator.Current.Value;
            TObject obj = queue.Dequeue();
            //always make sure to remove empty queues
            if (queue.Count == 0)
            {
                _elements.Remove(enumerator.Current.Key);
            }
            return obj;
        }
    
        public IEnumerator<TObject> GetEnumerator()
        {
            foreach (var pair in _elements)
            {
                foreach (TObject obj in pair.Value)
                {
                    yield return obj;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<TObject>).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("Array needs to be of rank 1", "array");
            }
            if (this.Count + index > array.Length)
            {
                throw new ArgumentException("There is not enough space in the array","array");
            }
            int currentIndex = index;
            foreach (var pair in _elements)
            {
                foreach (TObject obj in pair.Value)
                {
                    array.SetValue(obj, currentIndex++);
                }
            }
        }

        public int Count
        {
            get 
            {
                int count = 0;
                foreach (var queue in _elements.Values)
                {
                    count += queue.Count;
                }
                return count;
            }
        }

        public bool IsSynchronized
        {
            get { return (_elements as ICollection).IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return (_elements as ICollection).SyncRoot; }
        }
    }
}
