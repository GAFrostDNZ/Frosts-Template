using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;


namespace MyBot
{
    public class Program
    {
        public static void Main(string[] args) =>
            new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task Start()
        {

            client = new DiscordSocketClient();
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
            });

            client.Log += Log;
            await client.LoginAsync(TokenType.Bot, "YOUR TOKEN HERE PLS");
            await client.StartAsync();

            Console.WriteLine($"{DateTime.UtcNow}: Your Bot Has Started! Have fun kid ~Frost");

            var serviceProvider = ConfigureServices();
            handler = new CommandHandler(serviceProvider);
            await handler.ConfigureAsync();


            //Block this program untill it is closed
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public IServiceProvider ConfigureServices()
        {

            var services = new ServiceCollection()
                .AddSingleton(client)
                 .AddSingleton(new CommandService(new CommandServiceConfig { CaseSensitiveCommands = false }));
            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);




            return provider;
        }


    }
}