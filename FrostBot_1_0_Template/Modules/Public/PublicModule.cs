using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FrostBot_1_0_Template.Modules.Public
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        
        [Command("test")] //Command Name
        [Remarks("Tests your bot to see if it worked ;)")] //Summary for your command. it will not add anything.
        public async Task Test()
        {
            await ReplyAsync("Successfully Conducted test. Also , you should thank our god ; Frost, for giving you this project."); //makes the bot reply back!
        }
        [Command("credits")]
        [Remarks("Credits for this project")]
        public async Task Credits()
            {
                 var embed = new EmbedBuilder()
                    {
                        Color = new Color(245 , 232 , 31)
                    };
                  embed.Title = $"Credits:";
                  embed.Description = $"~Frost \n ~Morde";
                  embed.WithFooter(new EmbedFooterBuilder().WithIconUrl("https://cdn.discordapp.com/attachments/275377268728135680/318819504467476483/FrostBot_LOGO_png.png").WithText($"As Requested By {Context.User.Username}."));
                    await Context.Channel.SendMessageAsync("" , embed : embed);
            }
    }
        

}


/*
    Hello Everyone! This is a bot template i made B/c the one lunarlite made in CWS forum is shit and outdated!

    This one is updated to the latest version of 1.0: RC3! 
        

    use the Pre-Condition ===> [Command("Name")] To add a name to a command! Remember that this Pre-Condition will not trigger unless you have your prefix before the said command! 
    (Prefix in CommandHandler.cs , Line 58)

    Additional Pre-Attributes to Consider:

    [Remarks("Summary")] ===> Adds a summary to your designated command. will not trigger anycode. just for human speak!
    [RequireOwner] ===> Requires the owner of the bot (YOU) to do the said command!
    [RequireUserPermissions(GuildPermission.Your Wanted Permission)] ===> Requires the user doing the command to have a specified guild permission!!!
    [RequireBotPermissions(GuildPermission.Your Wanted Permission)] ===> Requires the BOT to have a specified permission before operating the said command!
    

    Following a Pre-Condition , you must add a task! (Look at PublicModule.cs , Line 14 For example!)


    All Said operations will require the await Operator! (Look at PublicModule.cs , Line 16 for example!)


    That is all i have for you in this template! now its your turn to shine!

    If you need help , @ me in the CWS Discord or at my personal discord: https://discord.gg/jYrQd5d 



    ~Frost

*/
