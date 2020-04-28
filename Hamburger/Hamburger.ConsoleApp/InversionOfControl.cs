using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburger.InversionOfControl;
using Hamburger.Logger;
using Ninject;


namespace Hamburger.ConsoleApp
{
    public static class InversionOfControl
    {
        private static IKernel _kernel;
        internal static IKernel Kernel
        {
            get
            {
                if (_kernel is null) { InitializeKernel(); }
                return _kernel;
            }
        }

        private static void InitializeKernel()
            => _kernel = new StandardKernel(new ConsoleInjectionModule(), new BaseInjectionModule());
    }
}
