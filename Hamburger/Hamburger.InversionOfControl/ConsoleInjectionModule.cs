using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
