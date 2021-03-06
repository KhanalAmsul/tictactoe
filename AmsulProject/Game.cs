﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace AmsulProject
{
    public class Game
    {

        public char whoamI = 'x';
        public bool forever_lonely = true;
        public NetworkPlayer multiplayerConnectionClient;
        public Gamestate my_board = new Gamestate();

        public Game(bool iAmX, bool localPlay, NetworkPlayer multiplayerConnectionClient) //constructor
        {

            if (iAmX)
            {
                whoamI = 'x';
            }
            else
            {
                whoamI = 'o';
            }

            if (localPlay)
            {
                forever_lonely = true;

            }
            else
            {
                forever_lonely = false;
            }
            this.multiplayerConnectionClient = multiplayerConnectionClient;
        }


        public void PrintTheBoard()
        {

            for (int y = 0; y <= 2; y = y + 1)
            {
                foreach (char boardPosition in my_board.board_2nd[y])
                {
                    Console.Write(" " + boardPosition + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

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


                    if (my_board.valid_move(rowPos, colPos))
                    {
                        good_input = true;

                        my_board.updateBoard(rowPos, colPos);
                        if (!forever_lonely)
                        {
                            multiplayerConnectionClient.PerformMove(rowPos, colPos);

                        }
                    }
                    else
                    {
                        Console.WriteLine("You Dun Fucked Up.");
                        good_input = false;
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

        }//acceptPlayerInput


        public void AcceptNetworkInput()
        {
            int row_val = multiplayerConnectionClient.LastMoveOtherPlayer_row;
            int col_val = multiplayerConnectionClient.LastMoveOtherPlayer_col;

            my_board.updateBoard(row_val, col_val);
        }//acceptNetworkInput

        public bool checkwinstate(char whoamI)
        {
            bool win_state = false;
            for (int y = 0; y <= 2; y++)
            {
                bool bool_col = true;
                bool bool_row = true;
                for (int x = 0; x <= 2; x++)
                {
                    bool_col &= my_board.board_2nd[y][x] == whoamI;
                    bool_row &= my_board.board_2nd[x][y] == whoamI;

                }
                if (bool_col || bool_row)
                {
                    win_state = true;
                    break;
                }
            }

            bool bool_diag1 = my_board.board_2nd[0][0] == whoamI && my_board.board_2nd[1][1] == whoamI && my_board.board_2nd[2][2] == whoamI;

            bool bool_diag2 = my_board.board_2nd[0][2] == whoamI &&
                        my_board.board_2nd[1][1] == whoamI &&
                        my_board.board_2nd[2][0] == whoamI;

            if (bool_diag1 || bool_diag2)
            {
                win_state = true;
                /* Console.WriteLine("The winner is " + checkChar + ".");*/
            }

            if (win_state)
            {
                PrintTheBoard();
                Console.WriteLine("The winner is " + whoamI + ".");
            }
        

            return win_state;

        }

        public void Run()
        {
            bool gameIsOver = false;

            while (!gameIsOver)
            {
                PrintTheBoard();
                Console.WriteLine("new screen new turn");


                if ((whoamI == 'x' && my_board.current_x) || (whoamI == 'o' && !my_board.current_x))
                {
                    bool gotGoodInput = AcceptPlayerInput();
                    while (!gotGoodInput)
                    {
                        gotGoodInput = AcceptPlayerInput();
                    }
                    gameIsOver = checkwinstate(whoamI);

                    if (forever_lonely)
                    {
                        gameIsOver = checkwinstate(whoamI);

                        

                        if (my_board.current_x)
                        {
                            whoamI = 'x';
                        }
                        else
                        {
                            whoamI = 'o';
                        }
                    }

                }
                else
                {
                    if (!forever_lonely)
                    {
                        AcceptNetworkInput();
                        
                        gameIsOver = checkwinstate(whoamI == 'x' ? 'o' : 'x');
                    }
                }
               

                
                

                Console.WriteLine("\n\n\n"); 
                Console.WriteLine("I am showing you some json that will make your life easier");
                string jsonGameState = Newtonsoft.Json.JsonConvert.SerializeObject(my_board, Formatting.Indented);
                Console.WriteLine(jsonGameState);
                Console.WriteLine("\n\n\n");

            }//main game loop

            Console.ReadLine();//don't exit immediately after game ends
        }

    }
}
