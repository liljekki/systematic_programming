using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static double[] data;

    static void Main()
    {
        int size = 1000000;
        data = new double[size];

        Stopwatch sw = new Stopwatch();
        sw.Start();
        Parallel.For(0, size, i => { data[i] = Math.PI * Math.Exp(-data[i]); });
        Parallel.For(0, size, i => { data[i] = -data[i]/10; });
        sw.Stop();

        Console.WriteLine("Parallel with lambda execution time: " + sw.Elapsed.TotalSeconds + " sec");
        Console.ReadLine();
    }
}
