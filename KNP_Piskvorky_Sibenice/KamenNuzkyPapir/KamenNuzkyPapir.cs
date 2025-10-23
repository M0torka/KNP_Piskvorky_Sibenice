using System;


class KamenNuzkyPapir
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var rand = new Random();
        int scoreUser = 0, scoreComp = 0, round = 0;


        Console.WriteLine("Kámen, nůžky, papír — hra proti počítači");
        Console.WriteLine("Zadej 'k' pro kámen, 'n' pro nůžky, 'p' pro papír. 'q' pro konec.");


        while (true)
        {
            round++;
            Console.Write($"Kolo {round} — tvůj tah: ");
            string input = Console.ReadLine()?.Trim().ToLower() ?? "";


            if (input == "q") break;


            string userChoice = ParseChoice(input);
            if (userChoice == null)
            {
                Console.WriteLine("Neplatný vstup. Použij k/n/p nebo q.");
                round--;
                continue;
            }


            string[] choices = { "kámen", "nůžky", "papír" };
            string compChoice = choices[rand.Next(choices.Length)];


            Console.WriteLine($"Ty: {userChoice} — Počítač: {compChoice}");


            int result = DecideWinner(userChoice, compChoice);
            // result: 0 = remíza, 1 = user vyhrál, -1 = počítač vyhrál
            if (result == 0)
            {
                Console.WriteLine("Remíza.");
            }
            else if (result == 1)
            {
                Console.WriteLine("Vyhrál jsi!");
                scoreUser++;
            }
            else
            {
                Console.WriteLine("Prohrál jsi.");
                scoreComp++;
            }


            Console.WriteLine($"Skóre — Ty: {scoreUser} | Počítač: {scoreComp}\n");
        }


        Console.WriteLine("Konec hry.");
        Console.WriteLine($"Celkové skóre — Ty: {scoreUser} | Počítač: {scoreComp}");
    }


    static string ParseChoice(string s)
    {
        if (s == "k" || s == "kámen" || s == "kamen") return "kámen";
        if (s == "n" || s == "nůžky" || s == "nuzky") return "nůžky";
        if (s == "p" || s == "papír" || s == "papir") return "papír";
        return null;
    }


    static int DecideWinner(string user, string comp)
    {
        if (user == comp) return 0;
        // kámen poráží nůžky, nůžky poráží papír, papír poráží kámen
        if (user == "kámen" && comp == "nůžky") return 1;
        if (user == "nůžky" && comp == "papír") return 1;
        if (user == "papír" && comp == "kámen") return 1;
        return -1;
    }
}