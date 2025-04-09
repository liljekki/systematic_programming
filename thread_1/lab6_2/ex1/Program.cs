using System;
using System.IO;

class DirectoryStructure
{
    static void ShowDirectory(string path, string indent = "")
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            Console.WriteLine(indent + "[DIR] " + dir.Name);

            foreach (FileInfo file in dir.GetFiles())
                Console.WriteLine(indent + "  " + file.Name);

            foreach (DirectoryInfo subDir in dir.GetDirectories())
                ShowDirectory(subDir.FullName, indent + "  ");
        }
        catch (Exception ex)
        {
            Console.WriteLine(indent + "Помилка: " + ex.Message);
        }
    }

    static void Main()
    {
        Console.WriteLine("Завдання 1");
        Console.Write("Введiть шлях до папки: ");
        string path = Console.ReadLine();
        ShowDirectory(path);

        Console.WriteLine("\nЗавдання 2");
        Console.Write("Введiть шлях до папки для пошуку: ");
        string searchPath = Console.ReadLine();
        Console.Write("Введiть назву файлу для пошуку: ");
        string searchTerm = Console.ReadLine();
        FileSearcher.Search(searchPath, searchTerm);
    }
}
class FileSearcher
{
    public static void Search(string directory, string filename)
    {
        try
        {
            foreach (string file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(file).Contains(filename, StringComparison.OrdinalIgnoreCase))
                {
                    FileInfo info = new FileInfo(file);
                    Console.WriteLine("\nЗнайдено файл:");
                    Console.WriteLine($"Шлях: {info.FullName}");
                    Console.WriteLine($"Розмiр: {info.Length} байт");
                    Console.WriteLine($"Дата створення: {info.CreationTime}");
                    Console.WriteLine($"Атрибути: {info.Attributes}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}
