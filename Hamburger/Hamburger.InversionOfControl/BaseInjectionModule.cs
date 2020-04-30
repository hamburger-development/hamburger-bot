using System;
using Hamburger.Core;
using Hamburger.Core.Configuration;
using Hamburger.Discord;
using Hamburger.Discord.Logging;
using Hamburger.Logger;
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
        }
    }
}