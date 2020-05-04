using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Hamburger.Core;
using Hamburger.Discord.Logging;
using Hamburger.Logger;
using LogSeverity = Hamburger.Logger.LogSeverity;

namespace Hamburger.Discord
{
    public class HamburgerDiscord : IDiscordConnection
    {
        private readonly IDiscord _discord;
        private readonly DiscordLogger _discordLogger;
        private readonly ILogger _logger;
        private readonly CommandHandler _commandHandler;
        private readonly JoinHandler _joinHandler;


        public HamburgerDiscord(IDiscord hamburgerDiscord, DiscordLogger dislogger, ILogger logger, CommandHandler commandHandler, JoinHandler joinHandler)
        {
            _discord = hamburgerDiscord;
            _discordLogger = dislogger;
            _logger = logger;
            _commandHandler = commandHandler;
            _joinHandler = joinHandler;
        }
        public async Task RunAsync(CancellationToken tokenSource)
        {
            try
            {
                await _discord.InitializeAsync();
                _discord.Client.Log += _discordLogger.Log;
                _discord.Client.Ready += OnClientReady;
                await _commandHandler.InstallCommandsAsync();
                _joinHandler.InstallJoinHandler();
                await _discord.Client.StartAsync();
                await Task.Delay(-1, tokenSource);
            }
            catch (Exception ex)
            {
                if (_discord.Client != null)
                {
                    await _discord.Client.LogoutAsync();
                    _discord.DisposeOfClient();
                }
                _logger.Log(ex.Message, LogSeverity.SEVERITY_ERROR);
            }

            await Task.Delay(-1);
        }

        private Task OnClientReady()
        {
            _logger.Log("Client is ready", LogSeverity.SEVERITY_MESSAGE);
            _discord.Client.SetGameAsync("you", null, ActivityType.Watching);

            return Task.CompletedTask;
        }

    }
}
