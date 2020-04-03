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
            board[0] = new char[3];
            board[1] = new char[3];
            board[2] = new char[3];
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
                char row = input[0];
                int rowPos = Int32.Parse(row.ToString());
                char col = input[1];
                int colPos = Int32.Parse(col.ToString());

                if(itIsXsTurn)
                    board[rowPos][colPos] = 'x';
                else
                    board[rowPos][colPos] = 'o';

            }



        }

        public void Run()
        {
            while(true) {
                AcceptPlayerInput();
                PrintTheBoard();
                itIsXsTurn = !itIsXsTurn;
            }
        }

    }
}
