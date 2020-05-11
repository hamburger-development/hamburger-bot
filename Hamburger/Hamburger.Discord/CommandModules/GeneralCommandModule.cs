using System.Linq;
using Discord;
using Discord.Commands;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Hamburger.Discord.CommandModules
{
    public class GeneralCommandModule : ModuleBase<ShardedCommandContext>
    {
        private readonly IDbStorage _dbStorage;
        private readonly IDiscord _client;

        public GeneralCommandModule(IDbStorage dbStorage, IDiscord client)
        {
            _dbStorage = dbStorage;
            _client = client;
        }

        [Command("prefix")]
        public async Task PrefixAsync(string newPrefix)
        {
            var guildConfiguration = await _dbStorage.FindOne<HamburgerGuildConfiguration>(x => x.DiscordGuildId == Context.Guild.Id,
                Context.Guild.Id.ToString());

            var oldPrefix = guildConfiguration.CommandPrefix;
            guildConfiguration.CommandPrefix = newPrefix;

            await _dbStorage.UpdateOne(guildConfiguration, x => x.DiscordGuildId == Context.Guild.Id,
                Context.Guild.Id.ToString());

            var embed = new EmbedBuilder()
                .WithTitle("Command prefix changed")
                .WithDescription($"The command prefix was changed from `{oldPrefix}` to `{newPrefix}`")
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("botinfo")]
        public async Task BotinfoAsync()
        {
            DiscordSocketClient shard = _client.Client.GetShard(_client.Client.GetShardIdFor(Context.Guild));

            int totalGuilds = _client.Client.Guilds.Count;
            int totalUsers = _client.Client.Guilds.Sum(x => x.MemberCount);

            var embedBuilder = new EmbedBuilder()
                .WithTitle("Bot info")
                .AddField("Bot owner", "Duke#1118", true)
                .AddField("Latency", shard.Latency, true)
                .AddField("Shard ID", shard.ShardId, true)
                .AddField("Shard amount", _client.Client.Shards.Count, true)
                .AddField("Total guild amount", totalGuilds, true)
                .AddField("Total user amount", totalUsers, true)
                .Build();

            await Context.Channel.SendMessageAsync("", false, embedBuilder);
        }
    }
}
