using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static double[] data;

    static void Compute(int i)
    {
        data[i] = i / 10.0;
    }

    static void Main()
    {
        int size = 1000000;
        data = new double[size];

        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Використовуємо Parallel.ForEach для обробки елементів масиву
        Parallel.ForEach(data, (value, state, index) => Compute((int)index));

        sw.Stop();

        Console.WriteLine("Execution time: " + sw.Elapsed.TotalSeconds + " sec");
    }
}
