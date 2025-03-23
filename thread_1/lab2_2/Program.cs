using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static double[] dataDouble;
    static int[] dataInt;

    // Обчислення для типу int
    static void ComputeFirstInt(int i) => dataInt[i] = dataInt[i] / 10;
    static void ComputeSecondInt(int i) => dataInt[i] = dataInt[i] / (int)Math.PI;
    static void ComputeThirdInt(int i) => dataInt[i] = (int)(Math.Exp(dataInt[i]) / Math.Pow(dataInt[i], Math.PI));
    static void ComputeFourthInt(int i) => dataInt[i] = (int)(Math.Exp(Math.PI * dataInt[i]) / Math.Pow(dataInt[i], Math.PI));

    // Обчислення для типу double
    static void ComputeFirstDouble(int i) => dataDouble[i] = dataDouble[i] / 10;
    static void ComputeSecondDouble(int i) => dataDouble[i] = dataDouble[i] / Math.PI;
    static void ComputeThirdDouble(int i) => dataDouble[i] = Math.Exp(dataDouble[i]) / Math.Pow(dataDouble[i], Math.PI);
    static void ComputeFourthDouble(int i) => dataDouble[i] = Math.Exp(Math.PI * dataDouble[i]) / Math.Pow(dataDouble[i], Math.PI);

    static void Main()
    {
        // Задаємо різні розміри масивів для експериментів
        int[] sizes = { 100000, 1000000, 10000000 };

        foreach (var size in sizes)
        {
            // Створення масивів для типу double
            dataDouble = new double[size];
            for (int i = 0; i < size; i++)
                dataDouble[i] = i + 1;

            Console.WriteLine($"Results for array size: {size}");

            // Послідовне обчислення для типу double
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < size; i++)
                ComputeFirstDouble(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeFirst (double): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для типу double
            sw.Restart();
            Parallel.For(0, size, ComputeFirstDouble);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeFirst (double): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для другого виразу (double)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeSecondDouble(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeSecond (double): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для другого виразу (double)
            sw.Restart();
            Parallel.For(0, size, ComputeSecondDouble);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeSecond (double): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для третього виразу (double)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeThirdDouble(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeThird (double): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для третього виразу (double)
            sw.Restart();
            Parallel.For(0, size, ComputeThirdDouble);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeThird (double): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для четвертого виразу (double)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeFourthDouble(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeFourth (double): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для четвертого виразу (double)
            sw.Restart();
            Parallel.For(0, size, ComputeFourthDouble);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeFourth (double): {sw.Elapsed.TotalSeconds} sec");

            // Створення масивів для типу int
            dataInt = new int[size];
            for (int i = 0; i < size; i++)
                dataInt[i] = i + 1;

            // Послідовне обчислення для типу int
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeFirstInt(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeFirst (int): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для типу int
            sw.Restart();
            Parallel.For(0, size, ComputeFirstInt);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeFirst (int): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для другого виразу (int)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeSecondInt(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeSecond (int): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для другого виразу (int)
            sw.Restart();
            Parallel.For(0, size, ComputeSecondInt);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeSecond (int): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для третього виразу (int)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeThirdInt(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeThird (int): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для третього виразу (int)
            sw.Restart();
            Parallel.For(0, size, ComputeThirdInt);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeThird (int): {sw.Elapsed.TotalSeconds} sec");

            // Послідовне обчислення для четвертого виразу (int)
            sw.Restart();
            for (int i = 0; i < size; i++)
                ComputeFourthInt(i);
            sw.Stop();
            Console.WriteLine($"Serial ComputeFourth (int): {sw.Elapsed.TotalSeconds} sec");

            // Паралельне обчислення для четвертого виразу (int)
            sw.Restart();
            Parallel.For(0, size, ComputeFourthInt);
            sw.Stop();
            Console.WriteLine($"Parallel ComputeFourth (int): {sw.Elapsed.TotalSeconds} sec");

            Console.WriteLine();  // Пустий рядок між різними розмірами масивів
        }
    }
}
