using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Hamburger.Core.Permissions;

namespace Hamburger.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            LoadConfiguration("config.json");
        }

        public string DiscordBotToken { get; set; }
        public string MongoConnectionString { get; set; }
        public List<Permission> ValidPermissions { get; set; }

        public void LoadConfiguration(string path)
        {
            var json = JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(path));
            DiscordBotToken = json.DiscordToken;
            MongoConnectionString = json.MongoConnectionString;
            ValidPermissions = json.ValidPermissions;
        }
    }

    class ConfigObject
    {
        [JsonProperty("discord_token", NullValueHandling = NullValueHandling.Ignore)]
        public string DiscordToken { get; set; }

        [JsonProperty("mongo_connection_string", NullValueHandling = NullValueHandling.Ignore)]
        public string MongoConnectionString { get; set; }

        [JsonProperty("valid_permissions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Permission> ValidPermissions { get; set; }
    }
}
