using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmsulProject
{
    /// <summary>
    /// Handles initialization and manages host and server networking.
    /// Call Chat() in a loop to engage!
    /// </summary>
    public class NetworkManager
    {
        private NetworkPlayer iAmPlayer;

        /// <summary>
        /// Initializes and connects to a server
        /// </summary>
        public NetworkManager()
        {
            Console.WriteLine("Enter what type of multiplayer you want:\n1 to be a server\n2 to be a player\n3 to play locally");
            var option = Int32.Parse(Console.ReadLine());//todo fix this in case the player types 4 or Q or hello, keep prompting for good input
            if (option == 3)
            {
                Console.WriteLine("Local play it is.");
                return;
            }
            else if (option == 1)
            {
                Task.Run(NetworkHost.CreateWebHostBuilder().Build().Run);
                Thread.Sleep(1000);
                Console.WriteLine("You should point run NGrok (ngrok.io) and point it at the URL this server is listening on (likely localhost:5000) ex:\n\n > ngrok.exe http 5000\n\nEnter your ngrok url:");
            }
            else if (option == 2)
            {
                Console.WriteLine("Enter the URL of the host you'd like to connect to. Localhost won't work!");
            }
            iAmPlayer = new NetworkPlayer(Console.ReadLine());
            Console.WriteLine("Connecting. Enter your name: ");
            iAmPlayer.Connect(Console.ReadLine()).Wait(); //todo error handling - what if the url isn't responding??


            Console.WriteLine("Successfully connected! Type to chat.");

        }

        /// <summary>
        /// Prompts for user input and sends messages to the server for broadcast
        /// </summary>
        public void Chat()
        {
            if (isConnected())
            {
                Console.Write("Chat >");
                iAmPlayer.SendMessage(Console.ReadLine());
            }
            
        }

        /// <summary>
        /// Returns whether or not this game is connected to a server
        /// </summary>
        /// <returns></returns>
        public bool isConnected() { 
            return iAmPlayer != null;  
        }


    }
}
