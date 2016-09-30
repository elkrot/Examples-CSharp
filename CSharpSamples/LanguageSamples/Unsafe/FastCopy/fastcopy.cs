// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// fastcopy.cs
// компилировать с /unsafe
using System;

class Test
{
    // Ключевое слово "unsafe" позволяет использовать указатели внутри
    // следующего метода:
    static unsafe void Copy(byte[] src, int srcIndex,
        byte[] dst, int dstIndex, int count)
    {
        if (src == null || srcIndex < 0 ||
            dst == null || dstIndex < 0 || count < 0)
        {
            throw new ArgumentException();
        }
        int srcLen = src.Length;
        int dstLen = dst.Length;
        if (srcLen - srcIndex < count ||
            dstLen - dstIndex < count)
        {
            throw new ArgumentException();
        }


        // Следующий фиксированный оператор закрепляет размещение
        // в памяти объектов src и dst, благодаря чему они не будут
        // удалены при сборке мусора.		
        fixed (byte* pSrc = src, pDst = dst)
        {
            byte* ps = pSrc;
            byte* pd = pDst;

            // Цикл по счетчику блоками в 4 байта и копирование
            // целого числа (4 байта) при каждой итерации:
            for (int n = 0; n < count / 4; n++)
            {
                *((int*)pd) = *((int*)ps);
                pd += 4;
                ps += 4;
            }

            // Завершение копирования после перемещения всех байтов, которые не были
            // перемещены в блоках по 4 байта:
            for (int n = 0; n < count % 4; n++)
            {
                *pd = *ps;
                pd++;
                ps++;
            }
        }
    }


    static void Main(string[] args)
    {
        byte[] a = new byte[100];
        byte[] b = new byte[100];
        for (int i = 0; i < 100; ++i)
            a[i] = (byte)i;
        Copy(a, 0, b, 0, 100);
        Console.WriteLine("The first 10 elements are:");
        for (int i = 0; i < 10; ++i)
            Console.Write(b[i] + " ");
        Console.WriteLine("\n");
    }
}

