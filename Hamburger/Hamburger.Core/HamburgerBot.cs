using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hamburger.Core
{
    public class HamburgerBot
    {
        private CancellationTokenSource _tokenSource;
        public HamburgerBot(IDiscordConnection hamburgerDiscordConnection)
        {
            HamburgerDiscord = hamburgerDiscordConnection;
        }

        public IDiscordConnection HamburgerDiscord { get; }

        public async Task StartAsync()
        {
            _tokenSource = new CancellationTokenSource();
            await HamburgerDiscord.RunAsync(_tokenSource.Token);
        }
    }
}
