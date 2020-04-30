using System.Threading.Tasks;
using Discord.WebSocket;

namespace Hamburger.Discord
{
    public interface IDiscord
    {
        DiscordSocketClient Client { get; }
        Task InitializeAsync();
        void DisposeOfClient();
    }
}