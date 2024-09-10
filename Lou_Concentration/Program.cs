//Louai Al Jabi
//CMP220
//Application Assignment #2


using System;
using System.Drawing;
using System.Text.RegularExpressions;


Concentration.Hi();
Console.ReadKey(true);
Console.Clear();
Concentration.Run();


public class Board
{
    // A method that creates an empty 8x8 2d array. The '?' are place holders
    static public char[,] BlankBoard()
    {
        char[,] blankboard = new char[8, 8]
        {
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'},
        { '?', '?', '?', '?', '?', '?', '?', '?'}
        };
        return blankboard;
    }

    //The method creates an array consisting of random pairs of a chosen set of symbols. The board is randomised every time.
    static public char[,] RandomBoard()
    {
        char[] symbols = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '@','#','$','%', '&','='};
        Shuffle.ShuffleArray(symbols);
        char[,] randomboard = new char[8, 8];
        int index = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Creating the array such that it's pairs of symbols
                randomboard[i, j] = symbols[index];
                randomboard[i, j + 1] = symbols[index];
                index++;
                if (index >= symbols.Length)
                {
                    index = 0;
                    Shuffle.ShuffleArray(symbols); // Shuffle the array again if all elements have been used
                }
                j++; // increment j twice to avoid adding the same element to the same column twice
            }
        }
        Shuffle.ShuffleMDArray(randomboard);
        return randomboard;
    }

    //The method displays the board with bars between them and numbers showcasing their position
    static public void Display(char[,] array)
    {
        Console.WriteLine("0 1 2 3 4 5 6 7 8");
        for (int i = 0; i < array.GetLength(0); i++)
        {

            Console.Write(i + 1);
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write("|{0}", array[i, j]);
            }
            Console.Write("|");
            Console.WriteLine();
        }
    }

    //The method resets the board to the empty board with '?'s
    static public char[,] Reset(char[,] array, char symbol)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = symbol;
            }
        }
        return array;
    }
}

public class Shuffle
{
    //The method uses the Fisher-Yates algorithm to shuffle the array around
    static public void ShuffleArray(char[] Array)
    {
        Random rng = new Random();
        for (int i = Array.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            char holder = Array[i];
            Array[i] = Array[j];
            Array[j] = holder;
        }
    }

    //The method uses the Fisher-Yates algorithm shufle the multidimensional array around
    static public void ShuffleMDArray(char[,] MultiD)
    {
        Random rng = new Random();
        for (int i = 0; i < MultiD.GetLength(0); i++)
        {
            for (int j = 0; j < MultiD.GetLength(1); j++)
            {
                // Choose a random element to swap with
                int change_i = rng.Next(MultiD.GetLength(0));
                int change_j = rng.Next(MultiD.GetLength(1));

                // Swap the elements
                char holder = MultiD[i, j];
                MultiD[i, j] = MultiD[change_i, change_j];
                MultiD[change_i, change_j] = holder;
            }
        }
    }
}



public class Player
{
    // The method takes the player's input and checks if it's a number and only one element and returns the input as an int
    static public int PlayerInput()
    {
        string firstinput = Console.ReadLine();
        while (firstinput.Count() != 1 || !Regex.IsMatch(firstinput, @"^[0-9]+$"))
        {
            Console.WriteLine("Please enter one number only");
            firstinput = Console.ReadLine();
        }
        int X = Int32.Parse(firstinput);     
        return X;
    }
}

public struct Score
{
    //Didn't get a chance to develope a score system
}

public class Concentration
{
    //The method runs the game. imports the two boards and swaps the player's inputs with the board
    static public void Run()
    {
        char[,] gamestate = Board.BlankBoard();
        char[,] randomboard = Board.RandomBoard();
        bool quit = false;
        bool game = true;
        Board.Display(randomboard);
        Console.WriteLine(randomboard[3, 3]); //debug
        //Thread.Sleep(1);
        //Concentration.Wait(10);
        //Board.Display(gamestate);

        while (game)
        {
            quit = false;
            Console.WriteLine("Enter the X coordinate");
            int X = Player.PlayerInput();
            Console.WriteLine("Enter the Y coordinate");
            int Y = Player.PlayerInput();
            if (X == 0 || Y == 0) break;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (randomboard[i, j] == randomboard[Y - 1, X - 1])
                    {
                        gamestate[i, j] = randomboard[Y - 1, X - 1];
                        randomboard[Y - 1, X - 1] = '(';
                        quit = true;
                        break;
                    }
                }
                if (quit) break;
            }
            Board.Display(gamestate);

            quit = false;
            Console.WriteLine("Enter the X coordinate");
            int A = Player.PlayerInput();
            Console.WriteLine("Enter the Y coordinate");
            int B = Player.PlayerInput();
            if (A == 0 || B == 0) break;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (randomboard[i, j] == randomboard[B - 1, A - 1])
                    {
                        Console.WriteLine("boob1"); //debug
                        if (gamestate[Y - 1, X - 1] == randomboard[B - 1, A - 1])
                        {
                            gamestate[i, j] = randomboard[B - 1, A - 1];
                            Console.WriteLine("boob2"); //debug
                            randomboard[B - 1, A - 1] = '(';
                            quit = true;
                            break;
                        }
                    }
                }
                if (quit) break;
            }
            Board.Display(gamestate);
            Thread.Sleep(1000);
            Console.Clear();

            foreach (char c in gamestate)
            {
                int count = 64;
                if (!(c == '?'))
                {
                    count--;
                }
                if (count == 0) game = false;
            }

            Board.Display(randomboard);
            Concentration.Wait(5);
        }
    }

    //The method waits the gives amount and showcases the number 
    static public void Wait(int num)
    {

        for (int i = 0; i < num; i++)
        {
            Console.Write("{0}s ",i + 1);
            Thread.Sleep(1000);
        }
        Console.WriteLine();
        Console.Clear();
    }

    //The method gives a greeting
    static public void Hi()
    {
        Console.WriteLine("""
            Welcome to the Concentration memory game!

            The game is based on matching every symbol with its pair. You will be presented with a radnomely shuffled board and asked to
            enter the X coordinates first, and the Y coordinates second that correspond to the card you want to flip. While playing,
            if you want to stop the game, enter your answer as the number 0.

            Press any key to start the game.
            """);
    }
}