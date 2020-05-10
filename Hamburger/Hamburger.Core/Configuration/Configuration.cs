using Newtonsoft.Json;
using System.IO;

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

        public void LoadConfiguration(string path)
        {
            var json = JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(path));
            DiscordBotToken = json.DiscordToken;
            MongoConnectionString = json.MongoConnectionString;
        }
    }

    class ConfigObject
    {
        [JsonProperty("discord_token")]
        public string DiscordToken { get; set; }
        [JsonProperty("mongo_connection_string")]
        public string MongoConnectionString { get; set; }

    }
}
