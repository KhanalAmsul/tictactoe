using System;
using System.Collections.Generic;

namespace AmsulProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to tictactoe");
            Console.ReadLine();
            Game tictac = new Game();
            tictac.Run();
        }
    }

}
