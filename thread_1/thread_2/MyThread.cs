using System.Diagnostics;

public class MyThread
{
    public int Count;
    public Thread Thrd;
    public ThreadPriority Priority;
    public Stopwatch Stopwatch;

    public MyThread(string name, ThreadPriority priority)
    {
        Count = 0;
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Priority = priority;
        Priority = priority;
        Stopwatch = new Stopwatch();
        Thrd.Start();
    }

    void Run()
    {
        Stopwatch.Start();

        while (Count < 1000000000)
        {
            Count++;
        }

        Stopwatch.Stop();
    }
}
