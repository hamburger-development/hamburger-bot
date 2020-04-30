using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public string DiscordBotToken { get; set; }
    }
}
