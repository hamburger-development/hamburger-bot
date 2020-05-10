using Hamburger.Core;
using Hamburger.Core.PersistentStorage;
using Ninject;
using System.Threading.Tasks;

namespace Hamburger.ConsoleApp
{
    class Program
    {
        private static HamburgerBot _hamburger;

        private static async Task Main(string[] args)
        {
            _hamburger = new HamburgerBot(InversionOfControl.Kernel.Get<IDiscordConnection>());

            var mongodb = InversionOfControl.Kernel.Get<IDbStorage>();
            await _hamburger.StartAsync();
        }
    }
}
