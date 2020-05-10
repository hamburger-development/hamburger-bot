using Discord;
using Discord.Commands;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using System.Threading.Tasks;

namespace Hamburger.Discord.CommandModules
{
    public class GeneralCommandModule : ModuleBase<SocketCommandContext>
    {
        private readonly IDbStorage _dbStorage;

        public GeneralCommandModule(IDbStorage dbStorage)
        {
            _dbStorage = dbStorage;
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
    }
}
