using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Spacebar Challenge ===");
        Console.WriteLine("Úkol: Stiskni co nejvícekrát MEZERNÍK za 5 vteřin!");
        Console.WriteLine("Připrav se a stiskni ENTER pro start...");
        Console.ReadLine();

        int count = 0;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine("Začni mačkat MEZERNÍK!");

        // časový limit 5 sekund
        while (stopwatch.Elapsed < TimeSpan.FromSeconds(5))
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    count++;
                }
            }
            Thread.Sleep(1); // malý oddech pro CPU
        }

        stopwatch.Stop();
        Console.WriteLine($"\nČas vypršel! Počet stisknutí: {count}");

        // Hodnocení podle výkonu
        if (count < 20) Console.WriteLine("💤 Trochu pomalejší tempo.");
        else if (count < 50) Console.WriteLine("🙂 Dobrá rychlost!");
        else if (count < 80) Console.WriteLine("🔥 Skvělý výkon!");
        else Console.WriteLine("🚀 Neuvěřitelná rychlost!");
    }
}
