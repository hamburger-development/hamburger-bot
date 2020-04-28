using System;
using Hamburger.Logger;
using Ninject.Modules;

namespace Hamburger.InversionOfControl
{
    public class BaseInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWriteSomething>().To<WriteSomething>().InSingletonScope();
        }
    }
}