using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPattern
{
    class DataObserver : IObserver<int>
    {
        //give it a name so we can distinguish it in the output
        private string _name = "Observer";
        #region IObserver<int> Members

        public void OnCompleted()
        {
            Console.WriteLine(_name + ":Completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(_name + ": Error");
        }

        public void OnNext(int value)
        {
            Console.WriteLine(_name + ":Generated data {0}", value);
        }

        #endregion

        public DataObserver(string observerName)
        {
            _name = observerName;
        }
    }
}
