using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);
    private const int VK_SPACE = 0x20;

    static int record = 0; // uchovává nejlepší skóre

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool playAgain = true;
        while (playAgain)
        {
            PlayRound(5); // délka kola v sekundách

            Console.Write("\nChceš hrát znovu? (a/n): ");
            string ans = Console.ReadLine()?.Trim().ToLower();

            if (ans == "a" || ans == "ano")
                playAgain = true;
            else
                playAgain = false;
        }

        Console.WriteLine("\nPapaaaaa!");
    }

    static void PlayRound(int seconds)
    {
        Console.Clear();
        Console.WriteLine("=== Spacebar Challenge ===");
        Console.WriteLine($"Stiskni MEZERNÍK co nejvíc za {seconds} sekund.");
        Console.WriteLine($"Aktuální rekord: {record}");
        Console.WriteLine("Začni stisknutím MEZERNÍKU...");

        // čekání na první stisk mezerníku
        while (!IsSpaceDown()) { Thread.Sleep(1); }
        // počká, až ho hráč pustí (aby se první stisk nepočítal rovnou do výsledku)
        while (IsSpaceDown()) { Thread.Sleep(1); }

        int count = 0;
        Stopwatch sw = Stopwatch.StartNew();
        bool prevDown = false;

        while (sw.Elapsed < TimeSpan.FromSeconds(seconds))
        {
            bool isDown = IsSpaceDown();
            if (isDown && !prevDown)
            {
                count++;
            }

            prevDown = isDown;
            Thread.Sleep(1);
        }

        sw.Stop();
        Console.WriteLine($"\nČas vypršel! Počet stisknutí: {count}");

        if (count > record)
        {
            record = count;
            Console.WriteLine("🏆 Nový rekord!");
        }
        else
        {
            Console.WriteLine($"Rekord zůstává: {record}");
        }

        if (count < 20) Console.WriteLine("💤 Trochu pomalejší tempo.");
        else if (count < 50) Console.WriteLine("🙂 Dobrá rychlost!");
        else if (count < 80) Console.WriteLine("🔥 Skvělý výkon!");
        else Console.WriteLine("🚀 Tyran rychlosti!");
    }

    static bool IsSpaceDown()
    {
        return (GetAsyncKeyState(VK_SPACE) & 0x8000) != 0;
    }
}