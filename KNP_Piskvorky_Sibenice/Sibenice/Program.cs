using System;
using System.Linq;


class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string[] words = {
"program", "stůl", "čokoláda", "klíč", "kamarád",
"počítač", "město", "hory", "zahrada", "kolo"
};


        Random rand = new Random();
        string word = words[rand.Next(words.Length)].ToLower();
        char[] guessed = new string('_', word.Length).ToCharArray();


        int lives = 7;
        bool won = false;


        while (lives > 0 && !won)
        {
            Console.Clear();
            Console.WriteLine("Šibenice — uhodni slovo!");
            Console.WriteLine($"Životy: {lives}");
            Console.WriteLine($"Slovo: {new string(guessed)}");


            Console.Write("Hádej písmeno: ");
            string input = Console.ReadLine()?.ToLower() ?? "";


            if (input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("Zadej prosím jedno písmeno.");
                Console.ReadKey();
                continue;
            }


            char guess = input[0];


            if (guessed.Contains(guess))
            {
                Console.WriteLine("Toto písmeno už jsi hádal.");
                Console.ReadKey();
                continue;
            }


            bool correct = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == guess)
                {
                    guessed[i] = guess;
                    correct = true;
                }
            }


            if (!correct)
            {
                lives--;
            }


            won = !guessed.Contains('_');
        }


        Console.Clear();
        if (won)
        {
            Console.WriteLine($"Výborně! Uhodl jsi slovo: {word} 🎉");
        }
        else
        {
            Console.WriteLine($"Prohrál jsi. Slovo bylo: {word}");
        }
    }
}