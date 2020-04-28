using System;
using Hamburger.Logger;
using Ninject;

namespace Hamburger.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = InversionOfControl.Kernel.Get<ILogger>();

            logger.Log("This is an error", LogSeverity.SEVERITY_ERROR);
            logger.Log("This should be an alert", LogSeverity.SEVERITY_ALERT);
        }
    }
}
