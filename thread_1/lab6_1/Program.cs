using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введіть директорію для пошуку: ");
        string directory = Console.ReadLine();

        Console.Write("Введіть ім’я файлу для пошуку: ");
        string fileName = Console.ReadLine();

        try
        {
            string[] files = Directory.GetFiles(directory, fileName, SearchOption.AllDirectories);

            if (files.Length == 0)
            {
                Console.WriteLine("Файл не знайдено.");
            }
            else
            {
                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);
                    Console.WriteLine($"Знайдено: {info.FullName}");
                    Console.WriteLine($"Розмір: {info.Length} байт");
                    Console.WriteLine($"Час створення: {info.CreationTime}");
                    Console.WriteLine($"Атрибути: {info.Attributes}");
                    Console.WriteLine();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
    }
}
