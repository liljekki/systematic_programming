using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static double[] data;
    const double Target = 1000;
    const double Epsilon = 5;

    static void Compute(int i, ParallelLoopState state)
    {
        data[i] = i / 10;
        data[i] = Math.Sin(i) * 100;
        if (Math.Abs(data[i] - Target) < Epsilon) state.Break();
    }

    static void Main()
    {
        int size = 1000000;
        data = new double[size];

        Stopwatch sw = new Stopwatch();
        sw.Start();
        ParallelLoopResult result = Parallel.For(0, size, Compute);
        sw.Stop();

        Console.WriteLine("Parallel loop stopped at iteration: " + result.LowestBreakIteration);
        Console.WriteLine("Execution time: " + sw.Elapsed.TotalSeconds + " sec");
    }
}
