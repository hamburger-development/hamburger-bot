using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.Core.Configuration
{
    public interface IConfiguration
    {
        string DiscordBotToken { get; set; }
    }
}
