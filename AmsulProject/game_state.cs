

using System;
using System.Drawing.Printing;

namespace AmsulProject
{
    public class Gamestate
    {
        public bool current_x = true;
        public char[][] board_2nd = new char[3][];

        public Gamestate()//constructor
        {            
            for (int y = 0; y <= 2; y = y + 1)
            {
                board_2nd[y] = new char[3];

                for (int x = 0; x <= 2; x = x + 1)
                {
                    board_2nd[y][x] = '.';

                }
                
            }
        }

        public bool valid_move(int row_pos, int col_pos)
        {

            if (col_pos < 0 || col_pos > board_2nd.Length - 1 || row_pos < 0 || row_pos > board_2nd.Length - 1)
            {
                return false;

            }
            else
            {
                if (board_2nd[row_pos][col_pos] != '.')
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        public bool updateBoard(int row_pos, int col_pos)
        {

            char player_name;
            if (current_x)
            {
                player_name = 'x';
            }
            else
            {
                player_name = 'o';
            }

            if (!valid_move(row_pos, col_pos))
            {
                throw new Exception("cheater!");
            }
            
            board_2nd[row_pos][col_pos] = player_name;
            current_x = !current_x;

            return current_x;

        }

    }




}
