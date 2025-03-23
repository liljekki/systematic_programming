using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadPriorityExample
{
    class Program
    {
        public static Stopwatch TotalStopwatch = new Stopwatch();
        public static MyThread[] threads;

        static void Main()
        {
            Console.Write("Введіть кількість потоків: ");
            int threadCount = int.Parse(Console.ReadLine());

            threads = new MyThread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                Console.WriteLine($"Введіть пріоритет для потоку {i + 1} (0: Lowest, 1: BelowNormal, 2: Normal, 3: AboveNormal, 4: Highest): ");
                int priorityInput = int.Parse(Console.ReadLine());

                ThreadPriority priority = priorityInput switch
                {
                    0 => ThreadPriority.Lowest,
                    1 => ThreadPriority.BelowNormal,
                    2 => ThreadPriority.Normal,
                    3 => ThreadPriority.AboveNormal,
                    4 => ThreadPriority.Highest,
                    _ => ThreadPriority.Normal,
                };

                Console.WriteLine($"Потік {i + 1} буде мати пріоритет {priority}");

                threads[i] = new MyThread($"Потік {i + 1}", priority);
            }

            TotalStopwatch.Start();

            foreach (var thread in threads)
            {
                thread.Thrd.Join();
            }

            TotalStopwatch.Stop();

            Console.WriteLine("\n--- Результати ---");
            double totalElapsedTime = TotalStopwatch.ElapsedMilliseconds;

            for (int i = 0; i < threadCount; i++)
            {
                double threadElapsedTime = threads[i].Stopwatch.ElapsedMilliseconds;
                double percentage = (threadElapsedTime / totalElapsedTime) * 100;
                Console.WriteLine($"{threads[i].Thrd.Name} (пріоритет: {threads[i].Priority}) - {threads[i].Count} ітерацій, використано {percentage:F2}% часу.");
            }
        }
    }
}