using Discord;
using Discord.WebSocket;
using Hamburger.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace Hamburger.Discord
{
    public class HamburgerDiscordClient : IDiscord
    {
        private readonly IConfiguration _config;
        public HamburgerDiscordClient(IConfiguration config)
        {
            _config = config;
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
