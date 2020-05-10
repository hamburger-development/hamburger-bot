using Discord.WebSocket;
using System.Threading.Tasks;

namespace Hamburger.Discord
{
    public interface IDiscord
    {
        DiscordShardedClient Client { get; }
        Task InitializeAsync();
        void DisposeOfClient();
    }
}