namespace Hamburger.Logger
{
    public interface ILogger
    {
        void Log(string message, LogSeverity severity);
    }
}
