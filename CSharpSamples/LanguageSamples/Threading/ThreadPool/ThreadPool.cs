// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Threading;

// Класс Fibonacci предоставляет интерфейс дополнительного
// потока для выполнения длительных вычислений Fibonacci(N).
// Число N передается в конструктор класса Fibonacci вместе с
// событием, при  котором объект сообщает о завершении операции.
// После этого результат можно получить при помощи свойства FibOfN.
public class Fibonacci
{
    public Fibonacci(int n, ManualResetEvent doneEvent)
    {
        _n = n;
        _doneEvent = doneEvent;
    }

    // Метод оболочки для использования с пулом потоков.
    public void ThreadPoolCallback(Object threadContext)
    {
        int threadIndex = (int)threadContext;
        Console.WriteLine("thread {0} started...", threadIndex);
        _fibOfN = Calculate(_n);
        Console.WriteLine("thread {0} result calculated...", threadIndex);
        _doneEvent.Set();
    }

    // Рекурсивный метод, вычисляющий N-ое число Фибоначчи.
    public int Calculate(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            return Calculate(n - 1) + Calculate(n - 2);
        }
    }

    public int N { get { return _n; } }
    private int _n;

    public int FibOfN { get { return _fibOfN; } }
    private int _fibOfN;

    ManualResetEvent _doneEvent;
}

public class ThreadPoolExample
{
    static void Main()
    {
        const int FibonacciCalculations = 10;

        // Каждый объект Fibonacci использует одно событие
        ManualResetEvent[] doneEvents = new ManualResetEvent[FibonacciCalculations];
        Fibonacci[] fibArray = new Fibonacci[FibonacciCalculations];
        Random r = new Random();

        // Настройка и запуск потоков при помощи ThreadPool:
        Console.WriteLine("launching {0} tasks...", FibonacciCalculations);
        for (int i = 0; i < FibonacciCalculations; i++)
        {
            doneEvents[i] = new ManualResetEvent(false);
            Fibonacci f = new Fibonacci(r.Next(20,40), doneEvents[i]);
            fibArray[i] = f;
            ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
        }

        // Ожидание завершения расчета всех потоков пула...
        WaitHandle.WaitAll(doneEvents);
        Console.WriteLine("Calculations complete.");

        // Отображение результатов...
        for (int i= 0; i<FibonacciCalculations; i++)
        {
            Fibonacci f = fibArray[i];
            Console.WriteLine("Fibonacci({0}) = {1}", f.N, f.FibOfN);
        }
    }
}