using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RamsayRevived
{
    public class MainCmds : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        private async Task Help()
        {
            var eb = new EmbedBuilder()
            {
                Color = Color.Red,
                Title = "Gordon Ramsay Bot V0.2 Help"
            };
            eb.AddField("Main Commands", @"
gr!help == Show this help message
gr!insult == Insult someone
gr!dice == Roll a random dice number
gr!google == Search something on google
gr!botinfo == Get information on this bot
gr!ping == Check the bot's latency
gr!suggest == Send a suggestion to Starman (the bots creator)
gr!donate == Get the link to send a donation (minimum $1 USD) to help keep this and my other bots online");
            eb.AddField("Staff Commands", @"
gr!kick == Kick a user (takes no reason)
gr!ban == Ban a user (takes no reason)
gr!purge == Purge selected amount of messages
");
            var built = eb.Build();
            await ReplyAsync("I finally have the lamb sauce, so here are your commands", false, built);
        }
        [Command("ping")]
        private async Task Ping()
        {
            await ReplyAsync($"Pong! :ping_pong: **{Program._client.Latency}ms**");
        }
        [Command("botinfo")]
        private async Task BotInfo()
        {
            var eb = new EmbedBuilder();
            eb.WithTitle("Gordon Ramsay Bot by Starman#4981");
            eb.WithCurrentTimestamp();
            eb.WithColor(Color.Red);
            eb.WithFooter("Gordon Ramsay Bot V0.2");
            eb.AddField("Author", @"Starman#4981", true);
            eb.AddField("Contributors", @"
dead_inside#7377 - Not being an asshole when I was testing in his server", true);
            await ReplyAsync("", false, eb.Build());
        }
        [Command("lambsauce")]
        private async Task Lamb(IGuildUser user)
        {
            await Context.Message.DeleteAsync();
            await ReplyAsync(user.Mention + " can I ask you a question?");
            await Task.Delay(5000);
            await ReplyAsync("WHERES THE LAMB SAUUUUCCCEEE?!??!?!?!?!??!!");
        }
        [Command("dice")]
        private async Task Dice()
        {
            await ReplyAsync("You rolled a " + new Random().Next(1, 6) + "!");
        }
        [Command("insult")]
        private async Task insult(IGuildUser user)
        {
            try
            {
               await Context.Message.DeleteAsync();
            }
            catch
            {
                Console.WriteLine("Failed to delete " + Context.Message.Author + "'s insult command context on " + user);
            }
            List<string> insults = new List<string>();
            int counter = 0;
            string line;
            System.IO.StreamReader file =
    new System.IO.StreamReader(@"Resources\\insults");
            while ((line = file.ReadLine()) != null)
            {
                if(line != "" && line != null)
                {
                    insults.Add(line);
                    counter++;
                }
            }
            file.Close();
            string finalinsult = insults[new Random().Next(0, insults.Count)];
            if (user.Id == Context.Message.Author.Id)
                await ReplyAsync("You fucking idiot " + Context.Message.Author.Mention + " you can't insult yourself!");
            else if (user.Id == 363850072309497876)
                await ReplyAsync("You fucking idiot you can't insult my creator!");
            else
                await ReplyAsync(user.Mention + " " + finalinsult);
            Program.report.insultsSent++;
        }
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        private async Task KickIns(IGuildUser user, IGuildUser user2 = null, IGuildUser user3 = null, IGuildUser user4 = null, IGuildUser user5 = null)
        {
            if(user != null && user2 == null) // ONE USER
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await user.KickAsync();
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} was kicked by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 == null) // TWO USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await user.KickAsync();
                await user2.KickAsync();
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} were kicked by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 == null) // THREE USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await user.KickAsync();
                await user2.KickAsync();
                await user3.KickAsync();
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} were kicked by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 != null && user5 == null) // FOUR USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user4.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await user.KickAsync();
                await user2.KickAsync();
                await user3.KickAsync();
                await user4.KickAsync();
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} & {user4} were kicked by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 != null && user5 != null) // FIVE USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user4.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user5.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await user.KickAsync();
                await user2.KickAsync();
                await user3.KickAsync();
                await user4.KickAsync();
                await user5.KickAsync();
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} & {user4} & {user5} were kicked by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
        }
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        private async Task BanIns(IGuildUser user, IGuildUser user2 = null, IGuildUser user3 = null, IGuildUser user4 = null, IGuildUser user5 = null)
        {
            if (user != null && user2 == null) // ONE USER
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Context.Guild.AddBanAsync(user);
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} was banned by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 == null) // TWO USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Context.Guild.AddBanAsync(user);
                await Context.Guild.AddBanAsync(user2);
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} were banned by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 == null) // THREE USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Context.Guild.AddBanAsync(user);
                await Context.Guild.AddBanAsync(user2);
                await Context.Guild.AddBanAsync(user3);
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} were banned by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 != null && user5 == null) // FOUR USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user4.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Context.Guild.AddBanAsync(user);
                await Context.Guild.AddBanAsync(user2);
                await Context.Guild.AddBanAsync(user3);
                await Context.Guild.AddBanAsync(user4);
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} & {user4} were banned by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
            else if (user != null && user2 != null && user3 != null && user4 != null && user5 != null) // FIVE USERS
            {
                await ReplyAsync($@"LISTEN, {user.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user2.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user3.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user4.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"LISTEN, {user5.Mention}");
                await Task.Delay(500);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Task.Delay(1000);
                await ReplyAsync($@"FUCK OFF!");
                await Context.Guild.AddBanAsync(user);
                await Context.Guild.AddBanAsync(user2);
                await Context.Guild.AddBanAsync(user3);
                await Context.Guild.AddBanAsync(user4);
                await Context.Guild.AddBanAsync(user5);
                await ReplyAsync("GET OUT!");
                await ReplyAsync($@"({user} & {user2} & {user3} & {user4} & {user5} were banned by {Context.Message.Author} at {DateTime.UtcNow} (UTC))");
            }
        }
        [Command("shame")]
        private async Task Museum()
        {
            EmbedBuilder firstkick = new EmbedBuilder()
            {
                Color = Color.Orange,
                Title = "First Kick With This Bot"
            };
            EmbedBuilder firstban = new EmbedBuilder()
            {
                Color = Color.Red,
                Title = "First Ban With This Bot"
            };
            EmbedBuilder idiot = new EmbedBuilder()
            {
                Color = Color.Red,
                Title = "Ex-Owner of a server I Co-Own advertising then getting banned for it",
            };
            firstban.WithImageUrl("https://cdn.discordapp.com/attachments/318621297057988608/514928116557414400/unknown.png");
            firstkick.WithImageUrl("https://cdn.discordapp.com/attachments/318621297057988608/514927623567179786/unknown.png");
            idiot.WithImageUrl("https://cdn.discordapp.com/attachments/544634065387651083/545067324802203668/screensaver.png");
            await ReplyAsync("", false, firstkick.Build());
            await ReplyAsync("", false, firstban.Build());
            await ReplyAsync("", false, idiot.Build());
        }
        [Command("purge")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeChat(int amount = 0)
        {
            if (amount != 0)
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                const int delay = 3000;
                IUserMessage m = await ReplyAsync($"Messages purged. This message will be deleted in 3 seconds");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
        }


        [Command("suggest")]
        private async Task Suggest([Remainder]string suggestion)
        {
            if (suggestion.ToLower() != "hey" || suggestion.ToLower() != "hello" || suggestion.ToLower() != "hi")
            {
                SocketGuild guild = Program._client.GetGuild(543324164191289354);
                SocketTextChannel suggestions = guild.TextChannels.FirstOrDefault(x => x.Id == 544633242729447434);
                var eb = new EmbedBuilder();
                eb.WithTitle("Suggestion From " + Context.Message.Author);
                eb.WithCurrentTimestamp();
                eb.WithColor(Color.Blue);
                eb.WithDescription(suggestion);
                await suggestions.SendMessageAsync("", false, eb.Build());
                await ReplyAsync("Your suggestion has been sent!");
            }
        }
    }
}
