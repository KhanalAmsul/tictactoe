using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AmsulProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic Tac Toe NOW WITH MULTIPLAYER");
            var netTest = new NetworkManager();

            bool chatting = netTest.isConnected();
            while (chatting)
            {
                Console.WriteLine("Type EXIT to break out of test chat loop and go to the game");
                if (Console.ReadLine() == "EXIT") break;
                netTest.Chat();
            }

            //todo integrate chat and game, really need a screenmanager so that chat and board can print next to each other
            Game tictac = new Game();
            tictac.Run();
        }
    }

}
