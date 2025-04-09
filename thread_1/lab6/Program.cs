using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть шлях до директорії: ");
        string path = Console.ReadLine();

        if (Directory.Exists(path))
        {
            PrintDirectoryTree(path, "");
        }
        else
        {
            Console.WriteLine("Директорія не існує.");
        }
    }

    static void PrintDirectoryTree(string path, string indent)
    {
        Console.WriteLine(indent + Path.GetFileName(path));

        foreach (var dir in Directory.GetDirectories(path))
        {
            PrintDirectoryTree(dir, indent + "  ");
        }

        foreach (var file in Directory.GetFiles(path))
        {
            Console.WriteLine(indent + "  " + Path.GetFileName(file));
        }
    }
}
