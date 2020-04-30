using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Hamburger.Core.Configuration;

namespace Hamburger.Discord
{
    public class HamburgerDiscordClient : IDiscord
    {
        private readonly IConfiguration _config;
        public HamburgerDiscordClient(IConfiguration config)
        {
            _config = config;
            _config.DiscordBotToken = "NjE0NzkyMTYwNDUzMDY2NzUy.XqggNg.EmALnDr_H2h35qqj9cFNgA7IaPU";
        }

        public DiscordSocketClient Client { get; private set; }
        public async Task InitializeAsync()
        {
            if (string.IsNullOrWhiteSpace(_config.DiscordBotToken))
            {
                throw new ArgumentNullException($"{nameof(_config.DiscordBotToken)} is null");
            }

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            await Client.LoginAsync(TokenType.Bot, _config.DiscordBotToken);
        }

        public void DisposeOfClient()
        {
            Client = null;
        }
    }
}
