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

        public bool AcceptPlayerInput()
        {
            
            Console.WriteLine("Type Board Coords Like 11");
            string input = Console.ReadLine();
            bool good_input = false;
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
                            char checkChar;
                            if (itIsXsTurn)
                            {
                                board[rowPos][colPos] = 'x';
                                checkChar = 'x';
                            }
                            else
                            {
                                board[rowPos][colPos] = 'o';
                                checkChar = 'o';
                            }
                            good_input = true;
                            /*itIsXsTurn = !itIsXsTurn;*/
                            
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

            return good_input;

        }//acceptPlayerInput end


        

        public bool checkwinstate(char checkChar)
        {
            bool win_state = false;
            for (int y = 0; y <= 2; y++)
            {
                bool bool_col = true;
                bool bool_row = true;
                for (int x = 0; x <= 2; x++)
                {
                    bool_col &= board[y][x] == checkChar;
                    bool_row &= board[x][y] == checkChar;

                }
                if (bool_col || bool_row)
                {
                    win_state = true;                    
                    break;
                }
            }

            bool bool_diag1 = board[0][0] == checkChar && board[1][1] == checkChar && board[2][2] == checkChar;

            bool bool_diag2 = board[0][2] == checkChar &&
                        board[1][1] == checkChar &&
                        board[2][0] == checkChar;

            if (bool_diag1 || bool_diag2)
            {
                win_state = true;
               /* Console.WriteLine("The winner is " + checkChar + ".");*/
            }

            return win_state;

        }

        public void Run()
        {
            bool gameIsOver = false;
            char current_player = '.';
            while(!gameIsOver) {
                PrintTheBoard();
                bool gotGoodInput = AcceptPlayerInput();
                while (!gotGoodInput)
                {
                    gotGoodInput = AcceptPlayerInput();

                }
                if (itIsXsTurn)
                {
                    current_player = 'x';
                }
                else
                {
                    current_player = 'o';
                }
                gameIsOver = checkwinstate(current_player) ;
                
                itIsXsTurn = !itIsXsTurn;
            }

            if (gameIsOver)
            {
                Console.WriteLine("The winner is " + current_player + ".");
            }
            

            Console.ReadLine();//don't exit now


        }

    }
}
