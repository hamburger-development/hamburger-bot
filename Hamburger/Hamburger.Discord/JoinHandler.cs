using Discord.WebSocket;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using Hamburger.Core.Services;
using Hamburger.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hamburger.Core.Providers;

namespace Hamburger.Discord
{
    public class JoinHandler
    {
        private readonly IDiscord _discord;
        private readonly ILogger _logger;
        private readonly IDbStorage _db;
        private readonly HamburgerUserProvider _userProvider;

        public JoinHandler(IDiscord discord, ILogger logger, IDbStorage db, HamburgerUserProvider userProvider)
        {
            _discord = discord;
            _logger = logger;
            _db = db;
            _userProvider = userProvider;
        }

        public void InstallJoinHandler()
        {
            _discord.Client.JoinedGuild += HandleNewGuildAsync;
            _discord.Client.UserJoined += HandleNewUserJoinAsync;
            _discord.Client.LeftGuild += HandleLeaveGuildAsync;
        }

        private async Task HandleNewUserJoinAsync(SocketGuildUser arg)
        {
            var user = await _userProvider.GetOrCreateUserAsync(arg.Id, arg.Guild.Id);
            _logger.Log($"Created new user {user.DiscordUserId} on join", LogSeverity.SEVERITY_ALERT);
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
                var hamburgerUser = await _userProvider.GetOrCreateUserAsync(user.Id, arg.Id);
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
