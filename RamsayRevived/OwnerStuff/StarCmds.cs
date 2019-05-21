using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Z.Expressions;
using Google.Apis.Customsearch.v1.Data;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace RamsayRevived
{
    public class StarCmds : ModuleBase<SocketCommandContext>
    {
        Process currentProcess = Process.GetCurrentProcess();

        
        [Command("mem")]
        private async Task Mem()
        {
            await ReplyAsync(currentProcess.PrivateMemorySize64 / 1024 / 1024 + "MB");
        }
        [Command("embedimage")]
        private async Task Embed()
        {
            var eb = new EmbedBuilder();
            eb.WithImageUrl("https://i.ytimg.com/vi/KCYR9418I98/maxresdefault.jpg");
            await ReplyAsync("", false, eb.Build());
        }
        [Command("ussr")]
        private async Task USSR(SocketGuildUser user)
        {
            try
            {
                await Context.Message.DeleteAsync();
            }
            catch
            {
                Console.WriteLine("del err");
            }
            check:
            bool exists = false;
            if (!File.Exists("ussr.mp3"))
            {
                WebClient client = new WebClient();
                client.DownloadFile("https://www.marxists.org/history/ussr/sounds/mp3/soviet-anthem1944.mp3", "ussr.mp3");
                goto check;
            }
            else
                exists = true;
            await Context.Channel.SendFileAsync("ussr.mp3", $"{user.Mention}");
        }
        [Command("google")]
        private async Task Google([Remainder]string search)
        {
            // GOOGLESEARCH

            var results = SearchCMD.Search(search);
            List<Result> reslist = results.ToList();
            Result num1res = results.FirstOrDefault();
            Result num2res = results.FirstOrDefault(x => x != num1res);
            Result num3res = results.FirstOrDefault(x => x != num1res && x != num2res);
            var eb = new EmbedBuilder();

            // OTHER

            

            // EMBED

            eb.WithTitle("Google Search Results For " + search);
            eb.WithColor(Color.Blue);
            eb.AddField($"Result 1: {num1res.Title} ({num1res.Link})", num1res.Snippet);
            eb.AddField($"Result 2: {num2res.Title} ({num2res.Link})", num2res.Snippet);
            eb.AddField($"Result 3: {num3res.Title} ({num3res.Link})", num3res.Snippet);
            eb.WithCurrentTimestamp();

            // MESSAGE

            var build = eb.Build();
            await ReplyAsync("",false, build);
        }

        [Command("botreport")]
        private async Task BotReport()
        {
            // Guild info
            int guildCount = 0;
            int roleCount = 0;
            // Channel info
            int textChannels = 0;
            int voiceChannels = 0;
            int totalChannels = 0;
            // User info
            int userCount = 0;
            int dndCount = 0;
            int invCount = 0;
            int idlCount = 0;
            int onlCount = 0;
            int afkCount = 0;
            int offCount = 0;
            foreach(SocketGuild guild in Program._client.Guilds)
            {
                guildCount++;
                foreach(IRole role in guild.Roles)
                {
                    roleCount++;
                }
                foreach(SocketTextChannel channel in guild.TextChannels)
                {
                    totalChannels++;
                    textChannels++;
                }
                foreach(SocketVoiceChannel channel in guild.VoiceChannels)
                {
                    totalChannels++;
                    voiceChannels++;
                }
                foreach(SocketGuildUser user in guild.Users)
                {
                    if (user.Status == UserStatus.AFK)
                        afkCount++;
                    else if (user.Status == UserStatus.Idle)
                        idlCount++;
                    else if (user.Status == UserStatus.Invisible)
                        invCount++;
                    else if (user.Status == UserStatus.Offline)
                        offCount++;
                    else if (user.Status == UserStatus.Online)
                        onlCount++;
                    else if (user.Status == UserStatus.DoNotDisturb)
                        dndCount++;
                    userCount++;
                }
            }


            var eb = new EmbedBuilder();
            eb.WithTitle("Gordon Ramsay Bot Report");
            eb.WithCurrentTimestamp();
            eb.AddField($@"Guild Info", $@"Total Guilds: {guildCount}
Total Roles: {roleCount}", true);
            eb.AddField($@"Channel Info", $@"Total Channels: {totalChannels}
Text Channels: {textChannels}
Voice Channels: {voiceChannels}", true);
            eb.AddField($@"User Info", $@"Total Users: {userCount}
Online Users: {onlCount}
Offline Users: {offCount}
DnD Users: {dndCount}
Idle Users: {idlCount}
", true);
            await ReplyAsync($"Here's your report, {Context.Message.Author.Mention}", false, eb.Build());
        }

        [Command("nick")]
        private async Task Nick(SocketGuildUser user, [Remainder]string nickname)
        {
            try
            {
                await user.ModifyAsync(x => x.Nickname = nickname);
                await ReplyAsync(user + "'s nickname has been changed");
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }

        }

        [Command("blacklist")]
        private async Task BlackList(string username, int discriminator)
        {
            if(Context.Message.Author.Id == 363850072309497876)
            {
                SocketGuildUser user = null;
                foreach (SocketGuild guild in Program._client.Guilds)
                {
                    foreach (SocketGuildUser user2 in guild.Users)
                    {
                        string usernameanddiscrim = user2.Username + "#" + user2.Discriminator;
                        if (usernameanddiscrim == username + "#" + discriminator)
                        {
                            user = user2;
                            await ReplyAsync("Found user! blacklisting..");
                        }
                    }
                }

                // Loading routine
                StringBuilder sb = new StringBuilder();
                if (File.Exists("Resources\\Data\\blacklist.txt"))
                    sb.AppendLine(File.ReadAllText("Resources\\Data\\blacklist.txt"));
                else
                    File.WriteAllText("Resources\\Data\\blacklist.txt", "");

                if (!File.ReadAllText("Resources\\Data\\blacklist.txt").Contains(user.Id.ToString()))
                {
                    // blacklist the user
                    sb.AppendLine(user.Id.ToString());
                    await ReplyAsync("Blacklisted " + user);
                    await user.SendMessageAsync("You have been blacklisted from using commands on this bot. If you are the owner of a server and need to be unblacklisted contact Starman via the support server");
                }
                else
                {
                    // unblacklist the user
                    sb.Replace(user.Id.ToString(), "");
                    await ReplyAsync("Unblacklisted " + user);
                    await user.SendMessageAsync("You have been unblacklisted!");
                }

                // Save the blacklist
                File.WriteAllText("Resources\\Data\\blacklist.txt", sb.ToString());
            }
            
        }

        [Command("donate")]
        private async Task Donate()
        {
            await ReplyAsync("https://donatebot.io/checkout/543324164191289354");
        }

        [Command("reply")]
        private async Task Reply(string username, int discriminator, [Remainder]string message)
        {
            SocketGuild guild = Program._client.GetGuild(543324164191289354);
            SocketTextChannel suggestions = guild.TextChannels.FirstOrDefault(x => x.Id == 544633242729447434);
            if (Context.Message.Author.Id == 363850072309497876)
            {
                SocketGuildUser user = null;
                IUserMessage message2 = await Context.Channel.SendMessageAsync("finding user");
                foreach (SocketGuild guild2 in Program._client.Guilds)
                {
                    foreach (SocketGuildUser user2 in guild2.Users)
                    {
                        string usernameanddiscrim = user2.Username + "#" + user2.Discriminator;
                        if (usernameanddiscrim == username + "#" + discriminator)
                        {
                            user = user2;
                            await ReplyAsync("User found. Messaging...");
                        }
                        else
                        {
                            await message2.ModifyAsync(x => x.Content = user2.Username + " == wrong user...");
                        }
                    }
                }
                var eb = new EmbedBuilder();
                eb.WithTitle("Response to your suggestion");
                eb.WithColor(Color.Blue);
                eb.WithDescription(message);
                eb.WithCurrentTimestamp();
                await user.SendMessageAsync("", false, eb.Build());
                var eb2 = new EmbedBuilder();
                eb2.WithTitle("Suggestion Response");
                eb2.WithColor(Color.Blue);
                eb2.WithCurrentTimestamp();
                eb2.WithDescription(message);
                await suggestions.SendMessageAsync("", false, eb2.Build());
            }
        }
    }
}
