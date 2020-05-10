using Hamburger.InversionOfControl;
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
