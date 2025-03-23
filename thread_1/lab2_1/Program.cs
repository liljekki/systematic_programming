using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void ExecuteTask()
    {
        int taskIdentifier = Task.CurrentId ?? -1;
        Console.WriteLine($"Задача {taskIdentifier} починається.");

        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(taskIdentifier * 200); 
            Console.WriteLine($"Задача {taskIdentifier}, лічильник = {i}");
        }

        Console.WriteLine($"Задача {taskIdentifier} завершена.");
    }

    static void Main()
    {
        Task task1 = new Task(ExecuteTask);
        Task task2 = new Task(ExecuteTask);

        task1.Start();
        task2.Start();

        Task.WaitAll(task1, task2);
        Console.WriteLine("Всі задачі завершені.");

        Task task3 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Задача через лямбда-вираз починається.");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Лямбда-вираз, лічильник = {i}");
            }
            Console.WriteLine("Задача через лямбда-вираз завершена.");
        });

        task3.Wait(); 

        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Паралельна задача 1 виконується.");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Паралельна задача 1, лічильник = {i}");
                }
                Console.WriteLine("Паралельна задача 1 завершена.");
            },
            () =>
            {
                Console.WriteLine("Паралельна задача 2 виконується.");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Паралельна задача 2, лічильник = {i}");
                }
                Console.WriteLine("Паралельна задача 2 завершена.");
            }
        );

    }
}
