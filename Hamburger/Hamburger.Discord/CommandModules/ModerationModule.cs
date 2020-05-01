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
        public ModerationModule()
        {
            
        }
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

        [Command("ban")]
        public async Task BanAsync(IUser user, string reason = "No reason provided")
        {
            var embed = new EmbedBuilder()
                .WithTitle("User banned")
                .WithDescription($"{user.Mention} was banned by {Context.User.Mention}")
                .WithFooter($"{DateTime.Now.ToShortDateString()} | Hamburger v0.0")
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);

            var dmchannel = user.GetOrCreateDMChannelAsync().Result;

            var dmEmbed = new EmbedBuilder()
                .WithTitle($"You were banned from {Context.Guild.Name}")
                .AddField("Reason", reason)
                .WithFooter($"{DateTime.Now.ToShortDateString()} | Hamburger v0.0")
                .Build();

            await dmchannel.SendMessageAsync("", false, dmEmbed);

            await Context.Guild.AddBanAsync(user);
        }

        [Command("unban")]
        [Alias("pardon")]
        [Summary("Unbans the user by ID")]
        public async Task UnbanAsync(ulong userID)
        {
            await Context.Guild.RemoveBanAsync(userID);

            var embed = new EmbedBuilder()
                .WithTitle("User unbanned")
                .WithDescription($"User with ID {userID.ToString()} was unbanned by {Context.User.Mention}")
                .WithFooter($"{DateTime.Now.ToShortDateString()} | Hamburger v0.0")
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("unbanjacey")]
        public async Task UnbanJacey(ulong id = 370179002070990849)
        {
            await Context.Guild.RemoveBanAsync(id);
        }
    }
}
