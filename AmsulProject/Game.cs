using System;
using System.Collections.Generic;
using System.Text;

namespace AmsulProject
{
    public class Game
    {

        private char[][] board = new char[3][];
        private bool itIsXsTurn = true;

        public Game()//constructor
        {
            for (int y = 0; y <= 2; y = y + 1)
            {
                board[y] = new char[3];
                for (int x = 0; x <= 2; x = x + 1)
                {
                    board[y][x] = '.';

                }
                /*board[1] = new char[3];
                board[2] = new char[3];*/
            }
        }


        public void PrintTheBoard()
        {

            for(int y= 0; y <= 2; y = y+1)
            {
                foreach(char boardPosition in board[y])
                {
                    Console.Write(" " + boardPosition + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("new screen new turn");
        }

        public void AcceptPlayerInput()
        {
           
            Console.WriteLine("Type Board Coords Like 11");
            string input = Console.ReadLine();
            if (input.Length == 2)
            {
                try
                {
                    char col = input[0];
                    int colPos = Int32.Parse(col.ToString());
                    char row = input[1];
                    int rowPos = Int32.Parse(row.ToString());
                

                if (colPos <0 || colPos > board.Length-1 || rowPos < 0 || rowPos > board.Length-1)
                {
                    /*Console.WriteLine(colPos);
                    Console.WriteLine(rowPos);*/
                    Console.WriteLine("A tic tac toe board is 3 by 3 you idiot.");
                }
                else
                {

                    if (board[rowPos][colPos] != '.')
                    {
                        Console.WriteLine("There is already someone that is there.");
                    }
                    else
                    {
                        if (itIsXsTurn)

                            board[rowPos][colPos] = 'x';
                        else
                            board[rowPos][colPos] = 'o';

                        itIsXsTurn = !itIsXsTurn;
                    }
                }
                }
                catch
                {
                    Console.WriteLine("Yea Buddy, learn how to type a 2 digit number, idiot.");
                }
            }
            else
            {

                Console.WriteLine("What the fuck are you even doing?");
            }



        }//acceptPlayerInput end

        public void Run()
        {
            while(true) {
                PrintTheBoard();
                AcceptPlayerInput();
                
                //itIsXsTurn = !itIsXsTurn;
            }
        }

    }
}
