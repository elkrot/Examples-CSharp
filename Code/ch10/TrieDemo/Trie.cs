using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TrieDemo
{
    class Trie<T> 
    {
        //an inner class to actually contain values and the next nodes
        class TrieNode<T>
        {
            //public so that Trie can access it
            public Dictionary<char, TrieNode<T>> _next = new Dictionary<char, TrieNode<T>>();
            public ICollection<T> Values { get; private set; }

            public TrieNode()
            {
                this.Values = new List<T>();
                _next = new Dictionary<char, TrieNode<T>>();
            }

            public void AddValue(string key, int depth, T item)
            {
                if (depth < key.Length)
                {
                    TrieNode<T> subNode;
                    if (!_next.TryGetValue(key[depth], out subNode))
                    {
                        subNode = new TrieNode<T>();
                        _next[key[depth]] = subNode;
                    }
                    subNode.AddValue(key, depth + 1, item);
                }
                else
                {
                    Values.Add(item);
                }
            }

            //get the sub-node for the specific character
            public TrieNode<T> GetNext(char c)
            {
                TrieNode<T> node;
                if (_next.TryGetValue(c, out node))
                    return node;
                return null;
            }

            //get all values in this node, and possibly all nodes under this one
            public ICollection<T> GetValues(bool recursive)
            {
                List<T> values = new List<T>();
                values.AddRange(this.Values);
                if (recursive)
                {
                    foreach (TrieNode<T> node in _next.Values)
                    {
                        values.AddRange(node.GetValues(recursive));
                    }
                }
                return values;
            }
        }

        TrieNode<T> _root = new TrieNode<T>();
        
        public void AddValue(string key, T item)
        {
            _root.AddValue(key, 0, item);
        }

        public ICollection<T> FindValues(string key, bool recursive)
        {
            TrieNode<T> next = _root;
            int index = 0;
            //follow the key to the last node
            while (index < key.Length && 
                next.GetNext(key[index]) != null)
            {
                next = next.GetNext(key[index++]);
            }
            //only get values if we found the entire key
            if (index == key.Length)
            {
                return next.GetValues(recursive);
            }
            else
            {
                return new T[0];
            }
        }
    }
}
