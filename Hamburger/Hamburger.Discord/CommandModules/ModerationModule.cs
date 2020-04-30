using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Hamburger.Discord.CommandModules
{
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user)
        {
            var embed = new EmbedBuilder()
                .WithTitle($"User info for {user.Username}")
                .AddField("Username", user.Username, true)
                .AddField("Is bot", user.IsBot, true)
                .AddField("Created at", user.CreatedAt.ToString())
                .AddField("User id", user.Id, true)
                .WithFooter($"{DateTime.Now.ToShortDateString()} | Hamburger v0.0")
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
