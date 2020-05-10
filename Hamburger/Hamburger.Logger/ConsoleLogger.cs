using System;

namespace Hamburger.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogSeverity severity)
        {
            if (severity == LogSeverity.SEVERITY_MESSAGE)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{DateTime.Now:g} | [MESSAGE] : {message}");
                Console.ResetColor();
            }
            else if (severity == LogSeverity.SEVERITY_ALERT)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{DateTime.Now:g} | [ALERT] : {message}");
                Console.ResetColor();
            }
            else if (severity == LogSeverity.SEVERITY_ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{DateTime.Now:g} | [ERROR] : {message}");
                Console.ResetColor();
            }
        }
    }


}
