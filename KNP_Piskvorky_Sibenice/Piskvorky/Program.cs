using System;


class Program
{
    static char[,] board = {
{ '1', '2', '3' },
{ '4', '5', '6' },
{ '7', '8', '9' }
};


    static char currentPlayer = 'X';


    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Piškvorky 3x3 — dva hráči");


        int moves = 0;
        bool gameOver = false;


        while (!gameOver)
        {
            DrawBoard();
            Console.Write($"Hráč {currentPlayer}, zadej číslo pole: ");
            string input = Console.ReadLine();


            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 9)
            {
                Console.WriteLine("Neplatný vstup. Zadej číslo 1–9.");
                continue;
            }


            int row = (choice - 1) / 3;
            int col = (choice - 1) % 3;


            if (board[row, col] == 'X' || board[row, col] == 'O')
            {
                Console.WriteLine("Toto pole je již obsazeno. Zkus jiné.");
                continue;
            }


            board[row, col] = currentPlayer;
            moves++;


            if (CheckWin())
            {
                DrawBoard();
                Console.WriteLine($"Hráč {currentPlayer} vyhrál! 🎉");
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