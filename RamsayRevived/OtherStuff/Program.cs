using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;

namespace RamsayRevived
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBot().GetAwaiter().GetResult();

        // Creating the necessary variables
        public static DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public static DailyReport report;

        // Runbot task
        public async Task RunBot()
        {
            _client = new DiscordSocketClient(); // Define _client
            _commands = new CommandService(); // Define _commands

            _services = new ServiceCollection() // Define _services
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "TOKEN"; // Make a string for the token

            _client.Log += Log; // Logging
            _client.MessageReceived += MessageReceived;
            _client.Ready += Ready;
            _client.JoinedGuild += JoinGuild;
            _client.LeftGuild += LeftGuild;
            _client.UserJoined += userjoin;
            _client.UserLeft += userleave;
            await RegisterCommandsAsync(); // Call registercommands

            await _client.LoginAsync(TokenType.Bot, botToken); // Log into the bot user

            Timer t = new Timer(86400000); // 1 sec = 1000, 60 sec = 60000

            t.AutoReset = true;

            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);

            t.Start();

            await _client.StartAsync(); // Start the bot user

            report = new DailyReport()
            {
                dayOfReport = DateTime.Now,
                insultsSent = 0,
                commandsRan = 0,
                guildsJoined = 0,
                guildsLeft = 0,
                messagesSent = 0,
                usersJoined = 0,
                usersLeft = 0
            };

            await _client.SetGameAsync("gr!help"); // Set the game the bot is playing

            await TakeCommand();

            await Task.Delay(-1); // Delay for -1 to keep the console window opened
        }

        private async void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            sendReport(true);
            int users = 0;
            foreach (SocketGuild guild in _client.Guilds)
            {
                foreach (SocketGuildUser user in guild.Users)
                {
                    users++;
                }
            }
            await _client.SetGameAsync($"{users} users", null, ActivityType.Watching);
        }

        private async void sendReport(bool isDaily)
        {
            SocketGuild supportServer = _client.GetGuild(543324164191289354);
            SocketTextChannel logChannel = (SocketTextChannel)supportServer.GetChannel(543537906854264842);
            string json = JsonConvert.SerializeObject(report, Formatting.Indented);
            File.WriteAllText("DailyReports\\" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + " Report.json", json);
            await logChannel.SendFileAsync("DailyReports\\" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + " Report.json", "Daily report for " + DateTime.Now);
            if (isDaily)
            {
                report = new DailyReport()
                {
                    dayOfReport = DateTime.Now,
                    insultsSent = 0,
                    commandsRan = 0,
                    guildsJoined = 0,
                    guildsLeft = 0,
                    messagesSent = 0,
                    usersJoined = 0,
                    usersLeft = 0
                };
            }
        }

        private async Task userjoin(SocketGuildUser arg)
        {
            report.usersJoined++;
        }

        private async Task userleave(SocketGuildUser arg)
        {
            report.usersLeft++;
        }

        private async Task JoinGuild(SocketGuild arg)
        {
            report.guildsJoined++;
        }

        private async Task LeftGuild(SocketGuild arg)
        {
            report.guildsLeft++;
        }

        private async Task TakeCommand()
        {
            string command = Console.ReadLine();
            if(command == "announce")
            {
                int amountsent = 0;
                foreach (SocketGuild guild in _client.Guilds)
                {
                    foreach (SocketTextChannel channel in guild.TextChannels)
                    {
                        try
                        {
                            if (channel.Name.ToLower() == "lobby")
                            {
                                await channel.SendMessageAsync($@"Hello, as you may have noticed this bot has been offline and I would like to let you all know about the current situation with the bot. To start, the server it is hosted on completely died last night taking all of my bots (Announcement Bot and Gordon Ramsay) down with it, now you may be wondering how I am sending this message if the bots are offline. Well I am hosting the bot off of my PC right now but that will not work as a permanent solution because all of the data on your servers is on the server that died so hosting it on my PC would be using data from the 31st of December which could cause some issues and break some things on your servers. If you have any further questions join the support server listed on the bots page. (Sorry if you already got this message just making sure everyone got it :) )");
                                amountsent++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Fail: " + e.Message);
                        }
                    }
                    Console.WriteLine("Sent " + amountsent + " messages");
                }
            }
        }

        private async Task Ready()
        {
            Bot.CheckDirs();
        }

        private async Task MessageReceived(SocketMessage message)
        {
            report.messagesSent++;
            SocketTextChannel chnl = message.Channel as SocketTextChannel;
            if(chnl.Guild.Id != 264445053596991498 && !message.Author.IsBot)
            {
                string msg = message.Content.ToLower();
                if (msg.Contains("gordon ramsay"))
                {
                    if (msg.Contains("sucks"))
                        await message.Channel.SendMessageAsync(message.Author.Mention + " Wrong, what sucks is your cooking. You tried microwaving a grilled cheese you fucking idiot");
                }
                if (msg.Contains("ramsey"))
                {
                    await chnl.SendMessageAsync(message.Author.Mention + " You fucking idiot");
                    await chnl.SendMessageAsync("YOU HAD ONE. FUCKING. JOB.");
                    await Task.Delay(1000);
                    await chnl.SendMessageAsync("AND THAT JOB WAS AS SIMPLE AS NOT CALLING ME RAMSEY YOU LITTLE FUCK");
                }
            }
        }

        private async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync; // Messagerecieved

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null); // Add module to _commands
        }

        private Task Log(LogMessage arg) // Logging
        {
            Console.WriteLine(arg); // Print the log to Console
            return Task.CompletedTask; // Return with completedtask
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            string messageLower = arg.Content.ToLower(); // Convert the message to a Lower
            var message = arg as SocketUserMessage; // Create a variable with the message as SocketUserMessage
            if (message is null || message.Author.IsBot) return; // Checks if the message is empty or sent by a bot
            int argumentPos = 0; // Sets the argpos to 0 (the start of the message)
            if (message.HasStringPrefix("gr!", ref argumentPos)) // If the message has the prefix at the start or starts with someone mentioning the bot
            {
                if (!File.ReadAllText("Resources\\Data\\blacklist.txt").Contains(message.Author.Id.ToString()))
                {
                    var context = new SocketCommandContext(_client, message); // Create a variable called context
                    var result = await _commands.ExecuteAsync(context, argumentPos, _services); // Create a veriable called result
                    if (!result.IsSuccess) // If the result is unsuccessful
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
                    else report.commandsRan++;
                }
                else
                {
                    await message.Channel.SendMessageAsync("You must seriously be stupid, you got yourself blacklisted why would you think you can use commands?");
                }
            }
        }
    }
}
