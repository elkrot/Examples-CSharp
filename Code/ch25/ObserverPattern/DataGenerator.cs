using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPattern
{
    class DataGenerator : IObservable<int>
    {
        private List<IObserver<int>> _observers = new List<IObserver<int>>();
        private int _lastPrime = -1;
        
        //inherited from IObservable<T>
        public IDisposable Subscribe(IObserver<int> observer)
        {
            _observers.Add(observer);
            observer.OnNext(_lastPrime);
            return observer as IDisposable;
        }

        //notifies all subscribers of the new data
        private void NotifyData(int n)
        {
            foreach (IObserver<int> observer in _observers)
            {
                observer.OnNext(n);
            }
        }

        //notifies all subscribers that no more data is coming
        private void NotifyComplete()
        {
            foreach (IObserver<int> observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        private static Random rand = new Random();

        //let's just generate some aribtrary data
        public void Run()
        {
            for (int i=0;i<100;++i)
            {
                int n = rand.Next(1, Int32.MaxValue);
                if (IsPrime(n))
                {
                    _lastPrime = n;
                    NotifyData(n);
                }
            }
            NotifyComplete();
        }

        private static bool IsPrime(Int32 number)
        {
            //check for evenness
            if (number % 2 == 0)
            {
                if (number == 2)
                    return true;
                return false;
            }
            //don't need to check past the square root
            Int32 max = (Int32)Math.Sqrt(number);
            for (Int64 i = 3; i <= max; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
