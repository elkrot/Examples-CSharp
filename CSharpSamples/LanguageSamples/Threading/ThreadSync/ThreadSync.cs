// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

// События синхронизации потоков инкапсулированы в этом 
// классе для быстрой передачи классам Consumer 
// и Producer. 
public class SyncEvents
{
    public SyncEvents()
    {
        // Для события "новый элемент" используется AutoResetEvent, поскольку
        // необходимо, чтобы каждый раз выполнялся автоматический сброс этого события,
        // когда на него отвечает поток-потребитель.
        _newItemEvent = new AutoResetEvent(false);

        // Для события "выход" используется ManualResetEvent, поскольку
        // необходимо, чтобы при возникновении данного события на него отвечало множество
        // потоков. Если для этого использовать AutoResetEvent, объект
        // события вернется в состояние отсутствия сигнала после 
        // ответа одного потока, и другим потокам не удастся 
        // завершиться.
        _exitThreadEvent = new ManualResetEvent(false);

        // Оба события помещаются также в массив WaitHandle, благодаря чему
        // поток-потребитель может блокировать оба события при помощи
        // метода WaitAny.
        _eventArray = new WaitHandle[2];
        _eventArray[0] = _newItemEvent;
        _eventArray[1] = _exitThreadEvent;
    }

    // Открытые свойства обеспечивают безопасный доступ к событиям.
    public EventWaitHandle ExitThreadEvent
    {
        get { return _exitThreadEvent; }
    }
    public EventWaitHandle NewItemEvent
    {
        get { return _newItemEvent; }
    }
    public WaitHandle[] EventArray
    {
        get { return _eventArray; }
    }

    private EventWaitHandle _newItemEvent;
    private EventWaitHandle _exitThreadEvent;
    private WaitHandle[] _eventArray;
}

// Класс Producer асинхронно (при помощи рабочего потока)
// добавляет элементы в очередь, пока их число не достигнет 20.
public class Producer 
{
    public Producer(Queue<int> q, SyncEvents e)
    {
        _queue = q;
        _syncEvents = e;
    }
    public void ThreadRun()
    {
        int count = 0;
        Random r = new Random();
        while (!_syncEvents.ExitThreadEvent.WaitOne(0, false))
        {
            lock (((ICollection)_queue).SyncRoot)
            {
                while (_queue.Count < 20)
                {
                    _queue.Enqueue(r.Next(0, 100));
                    _syncEvents.NewItemEvent.Set();
                    count++;
                }
            }
        }
        Console.WriteLine("Producer thread: produced {0} items", count);
    }
    private Queue<int> _queue;
    private SyncEvents _syncEvents;
}

// Класс Consumer использует свой собственный рабочий поток для обработки элементов
// очереди. Класс Producer уведомляет класс Consumer
// о появлении новых элементов при помощи события NewItemEvent.
public class Consumer
{
    public Consumer(Queue<int> q, SyncEvents e)
    {
        _queue = q;
        _syncEvents = e;
    }
    public void ThreadRun()
    {
        int count = 0;
        while (WaitHandle.WaitAny(_syncEvents.EventArray) != 1)
        {
            lock (((ICollection)_queue).SyncRoot)
            {
                int item = _queue.Dequeue();
            }
            count++;
        }
        Console.WriteLine("Consumer Thread: consumed {0} items", count);
    }
    private Queue<int> _queue;
    private SyncEvents _syncEvents;
}

public class ThreadSyncSample
{
    private static void ShowQueueContents(Queue<int> q)
    {
        // Операция перечисления коллекции по определению не является потокобезопасной,
        // поэтому необходимо обязательно блокировать коллекцию во время выполнения
        // перечисления, чтобы потоки потребителя и источника
        // не могли изменить содержимое коллекции. (Этот метод вызывается
        // только главным потоком.)
        lock (((ICollection)q).SyncRoot)
        {
            foreach (int i in q)
            {
                Console.Write("{0} ", i);
            }
        }
        Console.WriteLine();
    }

    static void Main()
    {
        // Настройка структуры, содержащей сведения о событии, необходимые
        // для синхронизации потоков. 
        SyncEvents syncEvents = new SyncEvents();

        // Коллекция универсальной очереди применяется для хранения элементов, 
        // предназначенных для создания и использования. В этом случае используется тип 'int'.
        Queue<int> queue = new Queue<int>();

        // Формирование объектов: одного -- для создания элементов, а другого -- для 
        // их использования. Очередь и события синхронизации
        // потоков передаются обоим объектам.
        Console.WriteLine("Configuring worker threads...");
        Producer producer = new Producer(queue, syncEvents);
        Consumer consumer = new Consumer(queue, syncEvents);

        // Создание потоковых объектов для объектов источника
        // и приемника. Это не приводит к созданию или запуску
        // реальных потоков.
        Thread producerThread = new Thread(producer.ThreadRun);
        Thread consumerThread = new Thread(consumer.ThreadRun);

        // Создание и запуск обоих потоков.     
        Console.WriteLine("Launching producer and consumer threads...");        
        producerThread.Start();
        consumerThread.Start();

        // Настройка потоков источника и приемника на работу в течение 10 секунд.
        // Использование главного потока (потока, выполняющего данный метод)
        // для отображения содержимого очереди каждые 2,5 секунды.
        for (int i = 0; i < 4; i++)
        {
            Thread.Sleep(2500);
            ShowQueueContents(queue);
        }

        // Передача обоим потокам -- и источника, и приемника -- сигнала о необходимости завершить работу.
        // Ответят оба потока, поскольку ExitThreadEvent -- это 
        // событие, сбрасываемое вручную, поэтому оно остается в состоянии 'set', пока не будет сброшено явным образом.
        Console.WriteLine("Signaling threads to terminate...");
        syncEvents.ExitThreadEvent.Set();

        // Применение метода Join для блокирования главного потока; сначала -- до момента завершения работы
        // потока источника, а затем -- до завершения работы потока приемника.
        Console.WriteLine("main thread waiting for threads to finish...");
        producerThread.Join();
        consumerThread.Join();
    }
}
