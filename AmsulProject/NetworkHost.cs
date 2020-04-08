using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmsulProject
{
    /// <summary>
    /// Class that the server background process will use to configure connections, send messages, manage player connections.
    /// Not a game state manager.
    /// </summary>
    public class NetworkHost : Hub
    {
        string hostedGroupName = "TICTACTOE"+Guid.NewGuid().ToString().Substring(0,5);
        static Dictionary<string,string> connectionIdPlayerName = new Dictionary<string, string>();

        /// <summary>
        /// Don't call this constructor yourself, it is only for ASP.NET Core to call
        /// </summary>
        public NetworkHost()
        {
        }

        /// <summary>
        /// Attach a human readable name to the connection ID
        /// Called from the NetworkPlayer class on a remote machine
        /// </summary>
        /// <param name="playerName">Screen name</param>
        public void AddPlayer(string playerName)
        {
            connectionIdPlayerName.Add(this.Context.ConnectionId, playerName);
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, "ThereIsOnlyOneGroup");
        }

        /// <summary>
        /// Sends a timestamped, signed (screenname) message to everyone attached to this server.
        /// Called from the NetworkPlayer class on a remote machine
        /// </summary>
        /// <param name="message">Text to send to everyone</param>
        /// <returns>Awaitable task</returns>
        public async Task SendChat(string message)
        {
            string formattedChat = "["+DateTime.Now.TimeOfDay+"] " + connectionIdPlayerName[this.Context.ConnectionId] + " says: " + message;
            await this.Clients.Group("ThereIsOnlyOneGroup").SendAsync("ReceiveMessage", formattedChat);
        }

        /// <summary>
        /// Called by the ASP.NET Core runtime to configure an application server
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(configureSignalR);//pass in a method that the run time can use to call us back later
        }

        private void configureSignalR(IEndpointRouteBuilder erb) //callback method
        {
            erb.MapHub<NetworkHost>("/connect");
        }

        /// <summary>
        /// Called by the ASP.NET Core runtime to configure an application server
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        /// <summary>
        /// Static method that initializes a web server to host connections
        /// </summary>
        /// <returns>A webhost builder you can build() and run()</returns>
        public static IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<NetworkHost>();
        }

    }
}
