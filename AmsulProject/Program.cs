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
            var networkManager = new NetworkManager();
            //todo integrate chat and game, really need a screenmanager so that chat and board can print next to each other
            //Host or local player => X
            Game tictac = new Game(networkManager.gameMode != GameMode.NetworkedPlayer, /*Localplay*/networkManager.gameMode == GameMode.Local, networkManager.playerConnection);
            tictac.Run();
        }//main


    }

}
