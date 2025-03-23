using System;
using System.Diagnostics;
using System.Threading;
using Thread_1;

class Program
{
    public static Stopwatch TotalStopwatch = new Stopwatch();

    static void Main()
    {
        Console.WriteLine("Запуск потоків...");

        TotalStopwatch.Start();

        MyThread mt1 = new MyThread("Потік 1", ThreadPriority.Highest);
        MyThread mt2 = new MyThread("Потік 2", ThreadPriority.Lowest);
        MyThread mt3 = new MyThread("Потік 3", ThreadPriority.AboveNormal);
        MyThread mt4 = new MyThread("Потік 4", ThreadPriority.Normal);
        MyThread mt5 = new MyThread("Потік 5", ThreadPriority.BelowNormal);







        mt1.Thrd.Join();
        mt2.Thrd.Join();
        mt3.Thrd.Join();
        mt4.Thrd.Join();
        mt5.Thrd.Join();

        TotalStopwatch.Stop();

        Console.WriteLine("\n--- Результати ---");


        double totalElapsedTime = TotalStopwatch.ElapsedMilliseconds;

        PrintThreadInfo(mt1, totalElapsedTime);
        PrintThreadInfo(mt2, totalElapsedTime);
        PrintThreadInfo(mt3, totalElapsedTime);
        PrintThreadInfo(mt4, totalElapsedTime);
        PrintThreadInfo(mt5, totalElapsedTime);

        Console.WriteLine("\nГоловний потік завершено.");
    }

    static void PrintThreadInfo(MyThread thread, double totalElapsedTime)
    {
        double threadElapsedTime = thread.Stopwatch.ElapsedMilliseconds;
        double percentage = (threadElapsedTime / totalElapsedTime) * 100;

        Console.WriteLine($"{thread.Thrd.Name} (пріоритет: {thread.Priority}) - {thread.Count} ітерацій, використано {percentage:F2}% часу.");
    }
}
