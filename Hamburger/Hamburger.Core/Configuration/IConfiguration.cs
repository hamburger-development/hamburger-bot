namespace Hamburger.Core.Configuration
{
    public interface IConfiguration
    {
        string DiscordBotToken { get; set; }
        string MongoConnectionString { get; set; }
        void LoadConfiguration(string path);
    }
}
