using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using Hamburger.Logger;

namespace Hamburger.Discord
{
    public class JoinHandler
    {
        private readonly IDiscord _discord;
        private readonly ILogger _logger;
        private readonly IDbStorage _db;

        public JoinHandler(IDiscord discord, ILogger logger, IDbStorage db)
        {
            _discord = discord;
            _logger = logger;
            _db = db;
        }

        public void InstallJoinHandler()
        {
            _discord.Client.JoinedGuild += HandleNewGuildAsync;
            _discord.Client.LeftGuild += HandleLeaveGuildAsync;
        }

        private async Task HandleNewGuildAsync(SocketGuild arg)
        {
            HamburgerGuildConfiguration guildConfig = new HamburgerGuildConfiguration
            {
                DiscordGuildId = arg.Id,
                DiscordGuildName = arg.Name,
                CommandPrefix = "!"
            };

            await _db.StoreOne(guildConfig, arg.Id.ToString());
        }

        private async Task HandleLeaveGuildAsync(SocketGuild arg)
        {
            return;
        }
    }
}
