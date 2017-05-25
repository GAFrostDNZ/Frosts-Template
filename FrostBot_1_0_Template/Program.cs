using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;


namespace MyBot
{
    public class Program : ModuleBase
    {
        public static void Main(string[] args) =>
            new Program().Start().GetAwaiter().GetResult();

        private IServiceProvider _map;
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task Start()
        {

            client = new DiscordSocketClient();
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
            });
            await client.LoginAsync(TokenType.Bot, "ADD YOUR TOKEN HERE! ~Frost");

            await client.StartAsync();




            Console.WriteLine($"{DateTime.UtcNow}: Your Bot Has Started! Have fun kid ~Frost");



            var ServiceProvider = ConfigureServices();
            handler = new CommandHandler(ServiceProvider);
            await handler.Install(_map, client);


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
                .AddSingleton(new CommandService(new CommandServiceConfig { CaseSensitiveCommands = false, ThrowOnError = true, LogLevel = LogSeverity.Verbose }))

                

            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(services);




            return provider;
        }


    }
}