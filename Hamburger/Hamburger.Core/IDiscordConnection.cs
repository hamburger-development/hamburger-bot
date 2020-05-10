using System.Threading;
using System.Threading.Tasks;

namespace Hamburger.Core
{
    public interface IDiscordConnection
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
