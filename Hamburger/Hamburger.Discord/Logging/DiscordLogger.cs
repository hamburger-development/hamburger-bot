using Discord;
using Hamburger.Logger;
using System.Threading.Tasks;
using LogSeverity = Discord.LogSeverity;

namespace Hamburger.Discord.Logging
{
    public class DiscordLogger
    {
        private readonly ILogger _logger;

        public DiscordLogger(ILogger logger)
        {
            _logger = logger;
        }

        internal Task Log(LogMessage msg)
        {
            if (msg.Severity == LogSeverity.Info)
            {
                _logger.Log(msg.ToString(), Logger.LogSeverity.SEVERITY_MESSAGE);
            }
            else if (msg.Severity == LogSeverity.Warning)
            {
                _logger.Log(msg.ToString(), Logger.LogSeverity.SEVERITY_ALERT);
            }
            else if (msg.Severity == LogSeverity.Error)
            {
                _logger.Log(msg.ToString(), Logger.LogSeverity.SEVERITY_ERROR);
            }

            return Task.CompletedTask;
        }
    }
}
