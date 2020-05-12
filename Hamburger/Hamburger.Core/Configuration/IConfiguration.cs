using System.Collections.Generic;
using Hamburger.Core.Permissions;

namespace Hamburger.Core.Configuration
{
    public interface IConfiguration
    {
        string DiscordBotToken { get; set; }
        string MongoConnectionString { get; set; }
        List<Permission> ValidPermissions { get; set; }
        void LoadConfiguration(string path);
    }
}
