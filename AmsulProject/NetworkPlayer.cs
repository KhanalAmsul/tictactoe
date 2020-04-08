using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmsulProject
{
    /// <summary>
    /// Class to handle a player connected to a server, provides methods for the server to callback to players
    /// </summary>
    public class NetworkPlayer
    {

        HubConnection connection;

        /// <summary>
        /// Should be handled by the NetworkManager, not done manually.
        /// Constructor that configures a connection to a host server, but does not open a connection.
        /// See Connect() to begin network action!
        /// </summary>
        /// <param name="hostUrl">The url of the server to connect to</param>
        public NetworkPlayer(string hostUrl)
        {
            this.connection = new HubConnectionBuilder().WithUrl(rectifyUrl(hostUrl)).WithAutomaticReconnect().Build();
            IDisposable disposable = connection.On<string>("ReceiveMessage", ReceiveMessage);
        }

        /// <summary>
        /// Opens a configured connection to the server with the given player name
        /// </summary>
        /// <param name="playerName">Human readable screenname</param>
        /// <returns>Awaitable task</returns>
        public async Task Connect(string playerName)
        {
            await connection.StartAsync();
            await connection.InvokeAsync("AddPlayer", playerName);
        }

        /// <summary>
        /// Invokes Server method to send a chat message from this player
        /// Should be handled by the NetworkManager, not done manually.
        /// </summary>
        /// <param name="message">The message to send over the wire</param>
        /// <returns>Awaitable task</returns>
        public async Task SendMessage(string message)
        {
            await connection.InvokeAsync("SendChat", message);
        }

        /// <summary>
        /// Prints a chat from the server
        /// Invoked by the server, do not invoke manually, that would be dumb.
        /// </summary>
        /// <param name="message">Message to print out, sent by the server</param>
        public void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }

        private string rectifyUrl(string url)
        {
            url = url.ToLower();
            if (!url.StartsWith("http://"))
            {
                url = "http://" + url;
            }
           
            if (!url.EndsWith("/") && !url.EndsWith("connect"))
            {
                url = url + "/";
            }

            if (!url.EndsWith("connect"))
            {
                url = url + "connect";
            }
            return url;

        }

    }
}
