
using System.Threading.Tasks;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace MyBot
{
    public class CommandHandler : ModuleBase
    {
        private CommandService commands;
        private DiscordSocketClient bot;
        private IServiceProvider map;

        public CommandHandler(IServiceProvider provider)
        {
            map = provider;
            bot = map.GetService<DiscordSocketClient>();
            commands = map.GetService<CommandService>();
        }

        public async Task Install(IServiceProvider _map, DiscordSocketClient c)



        {
            //Create Command Service, Inject it into Dependency Map
            bot = c;


            commands = new CommandService();

            map = _map;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            bot.UserJoined += AnnounceUserJoined;
            bot.UserLeft += AnnounceLeftUser;


            //Send user message to get handled
            bot.MessageReceived += HandleCommand;
        }



        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            //Don't handle the command if it is a system message
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            //Mark where the prefix ends and the command begins
            int argPos = 0;
            //Determine if the message has a valid prefix, adjust argPos
            if (!(message.HasMentionPrefix(bot.CurrentUser, ref argPos) || message.HasCharPrefix('!', ref argPos))) return;

            //Create a Command Context
            var context = new CommandContext(bot, message);
            //Execute the command, store the result
            var result = await commands.ExecuteAsync(context, argPos, map);

            //If the command failed, notify the user
            if (!result.IsSuccess && result.ErrorReason != "Unknown command.")

                await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
        }


        public async Task AnnounceLeftUser(SocketGuildUser user)
        {
            var thumbnailurl = user.GetAvatarUrl();
            var embed = new EmbedBuilder();
            embed.WithColor(new Color(0, 71, 171));

            {
                var channel = bot.GetChannel(000000000000) as SocketTextChannel;
                {
                    embed.ThumbnailUrl = user.GetAvatarUrl();
                    embed.Title = $"**{user.Username} Left The Server:**";
                    embed.Description = $"**User:** {user.Mention}\n **Time**: {DateTime.UtcNow}: \n **Server:** {user.Guild.Name}";
                    await channel.SendMessageAsync("", false, embed);
                }
            }

        }
       
        public async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var channel = bot.GetChannel(000000000000) as SocketTextChannel;
            var embed = new EmbedBuilder();
            embed.ThumbnailUrl = user.GetAvatarUrl();
            embed.WithColor(new Color(0x13ef42));
            embed.Title = $"**{user.Username} Joined The Server:**";
            embed.Description = ($" **User:** {user.Mention} \n **Time**: {DateTime.UtcNow}: \n **Server:** {user.Guild.Name}");
            await channel.SendMessageAsync("", false, embed: embed);
            
        }

    }
}