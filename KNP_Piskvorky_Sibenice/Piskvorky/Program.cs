using System;

class Program
{
    static char[,] board;
    static char currentPlayer;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Piškvorky 3x3 — dva hráči");

        bool playAgain = true;

        while (playAgain)
        {
            ResetBoard();
            int moves = 0;
            bool gameOver = false;

            while (!gameOver)
            {
                DrawBoard();
                Console.Write($"Hráč {currentPlayer}, zadej číslo pole: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > 9)
                {
                    Console.WriteLine("Zadej číslo 1–9.");
                    continue;
                }

                int row = (choice - 1) / 3;
                int col = (choice - 1) % 3;

                if (board[row, col] == 'X' || board[row, col] == 'O')
                {
                    Console.WriteLine("Tady už něco je. Zkus jiné pole.");
                    continue;
                }

                board[row, col] = currentPlayer;
                moves++;

                if (CheckWin())
                {
                    DrawBoard();
                    Console.WriteLine($"Hráč {currentPlayer} je frajer!");
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                    Console.WriteLine($"Hráč {currentPlayer} je looooser!");
                    gameOver = true;
                }
                else if (moves == 9)
                {
                    DrawBoard();
                    Console.WriteLine("Remíza!");
                    gameOver = true;
                }
                else
                {
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                }
            }

            Console.Write("\nChceš hrát znovu? (a/n): ");
            string again = Console.ReadLine()?.ToLower();
            playAgain = (again == "a" || again == "ano");
        }

        Console.WriteLine("Papaaaaa!");
    }

    static void ResetBoard()
    {
        board = new char[3, 3];
        char pos = '1';
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = pos++;
            }
        }
        currentPlayer = 'X';
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("-------------");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j] + " | ");
            }
            Console.WriteLine("\n-------------");
        }
    }

    static bool CheckWin()
    {
        // řádky a sloupce
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                return true;
            if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                return true;
        }
        // diagonály
        if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            return true;
        if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
            return true;

        return false;
    }
}
