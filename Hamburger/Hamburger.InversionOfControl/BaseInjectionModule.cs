using Hamburger.Core;
using Hamburger.Core.Configuration;
using Hamburger.Core.PersistentStorage;
using Hamburger.Core.Providers;
using Hamburger.Core.Services;
using Hamburger.Discord;
using Hamburger.Discord.Logging;
using Hamburger.MongoStorage;
using MongoDB.Driver;
using Ninject.Modules;

namespace Hamburger.InversionOfControl
{
    public class BaseInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<Configuration>().InSingletonScope();
            Bind<IDiscord>().To<HamburgerDiscordClient>().InSingletonScope();
            Bind<IDiscordConnection>().To<HamburgerDiscord>().InSingletonScope();
            Bind<DiscordLogger>().ToSelf().InSingletonScope();
            Bind<CommandHandler>().ToSelf().InSingletonScope();
            Bind<IDbStorage>().To<MongoDbStorage>().InSingletonScope();
            Bind<MongoClient>().ToSelf().InSingletonScope();
            Bind<UserService>().ToSelf().InSingletonScope();
            Bind<HamburgerUserProvider>().ToSelf().InSingletonScope();
            Bind<PermissionService>().ToSelf().InSingletonScope();
        }
    }
}