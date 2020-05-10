using Hamburger.Logger;
using Ninject.Modules;

namespace Hamburger.InversionOfControl
{
    public class ConsoleInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<ConsoleLogger>().InSingletonScope();
        }
    }
}
