using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hamburger.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            LoadConfiguration("config.json");
        }
        public string DiscordBotToken { get; set; }

        public void LoadConfiguration(string path)
        {
            if (path is null) return;
            var json = JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(path));
            DiscordBotToken = json.DiscordToken;
        }
    }

    class ConfigObject
    {
        [JsonProperty("discord_token")]
        public string DiscordToken { get; set; }
    }
}
