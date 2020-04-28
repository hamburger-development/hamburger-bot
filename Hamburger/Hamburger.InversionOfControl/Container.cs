using Ninject;
using Ninject.Modules;
using Hamburger.Logger;

namespace Hamburger.InversionOfControl
{
    public class Container
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
            => _kernel = new StandardKernel(new BaseInjectionModule());
    }
}
