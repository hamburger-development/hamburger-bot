using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using Hamburger.Core.Services;
using Hamburger.Logger;

namespace Hamburger.Discord
{
    public class JoinHandler
    {
        private readonly IDiscord _discord;
        private readonly ILogger _logger;
        private readonly IDbStorage _db;
        private readonly UserService _userService;

        public JoinHandler(IDiscord discord, ILogger logger, IDbStorage db, UserService userService)
        {
            _discord = discord;
            _logger = logger;
            _db = db;
            _userService = userService;
        }

        public void InstallJoinHandler()
        {
            _discord.Client.JoinedGuild += HandleNewGuildAsync;
            _discord.Client.LeftGuild += HandleLeaveGuildAsync;
        }

        private async Task HandleNewGuildAsync(SocketGuild arg)
        {
            if (await _db.CollectionExistsAsync(arg.Id.ToString()))
            {
                _logger.Log("Guild already exists, doing nothing", LogSeverity.SEVERITY_ALERT);
                return;
            }

            HamburgerGuildConfiguration guildConfig = new HamburgerGuildConfiguration
            {
                DiscordGuildId = arg.Id,
                CommandPrefix = "!"
            };

            _logger.Log($"Creating new collection for server: {arg.Name}", LogSeverity.SEVERITY_ALERT);
#pragma warning disable 4014
            _db.StoreOne(guildConfig, arg.Id.ToString());
#pragma warning restore 4014

            foreach (var user in arg.Users)
            {
                HamburgerUser hamburgerUser = new HamburgerUser
                {
                    DiscordUserId = user.Id,
                };

                await _userService.CreateUserAsync(hamburgerUser, guildConfig);
                _logger.Log($"Created user {hamburgerUser.DiscordUserId}", LogSeverity.SEVERITY_MESSAGE);
            }
        }

        private async Task HandleLeaveGuildAsync(SocketGuild arg)
        {
            _logger.Log("Waiting 1 minute before deleting collection...", LogSeverity.SEVERITY_ALERT);
            var _cts = new CancellationTokenSource();
            _ = Task.Delay(TimeSpan.FromMinutes(1).Milliseconds, _cts.Token).ContinueWith(x => DeleteDataAsync(arg.Id.ToString()), TaskContinuationOptions.NotOnCanceled);
        }

        private async Task DeleteDataAsync(string path)
        {
            await _db.DeleteCollectionAsync(path);
            _logger.Log($"Deleting collection for {path}", LogSeverity.SEVERITY_ALERT);
        }
    }
}
