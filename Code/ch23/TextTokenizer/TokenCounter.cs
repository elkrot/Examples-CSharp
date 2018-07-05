using System;
using System.Collections.Generic;

namespace TextTokenizer
{
    struct WordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }

    class TokenCounter
    {
        delegate void CountDelegate();

        private string _data;
        private Dictionary<string, int> _tokens = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
        private List<WordCount> _wordCounts = new List<WordCount>();
        public IList<WordCount> WordCounts
        {
            get
            {
                return _wordCounts;
            }
        }
        
        public TokenCounter(string data)
        {
            _data = data;
        }

        public void Count()
        {
            //just split by standard word separators to keep things simple
            char[] splitters = new char[] {' ', '.', ',', ';',':','-','?','!','\t','\n','\r','(',')','[',']','{','}' };
            string[] words = _data.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                int count;
                if (!_tokens.TryGetValue(word, out count))
                {
                    _tokens[word] = 1;
                }
                else
                {
                    _tokens[word] = count + 1;
                }
            }
            
            foreach (KeyValuePair<string, int> pair in _tokens)
            {
                _wordCounts.Add(new WordCount() { Word = pair.Key, Count = pair.Value });
            }
            _wordCounts.Sort((Comparison<WordCount>)delegate(WordCount a, WordCount b)
            {
                return -a.Count.CompareTo(b.Count);
            });
        }

        public IAsyncResult BeginCount(AsyncCallback callback, object state)
        {
            CountDelegate countDelegate = Count;
            return countDelegate.BeginInvoke(callback, state);
        }

        public void EndCount(IAsyncResult result)
        {
            //wait until operation finishes
            result.AsyncWaitHandle.WaitOne();
        }
    }
}
