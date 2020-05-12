using Discord;
using Discord.Commands;
using Hamburger.Core.Permissions;
using Hamburger.Core.Services;
using System;
using System.Threading.Tasks;

namespace Hamburger.Discord.CommandModules
{
    [Group("permission")]
    public class PermissionCommandModule : ModuleBase<ShardedCommandContext>
    {
        private readonly UserService _userService;

        public PermissionCommandModule(UserService userService)
        {
            _userService = userService;
        }

        [Command("add")]
        public async Task CmdAddPermissionAsync(IUser user, string node)
        {
            try
            {
                if (await _userService.AddPermissionAsync(user.Id, Context.Guild.Id, node))
                {
                    var embed = new EmbedBuilder()
                        .WithTitle("Permission added!")
                        .WithDescription($"Added permission `{node}` to {user.Mention}")
                        .Build();

                    await Context.Channel.SendMessageAsync("", false, embed);
                    return;
                }
            }
            catch (Exception ex)
            {
                var embed = new EmbedBuilder()
                    .WithTitle("User already has permission")
                    .WithDescription($"Please try adding another permission")
                    .Build();

                await Context.Channel.SendMessageAsync("", false, embed);
                return;
            }

            var embedError = new EmbedBuilder()
                .WithTitle("Permission doesn't exist")
                .WithDescription("Please try adding another permission.")
                .Build();

            await Context.Channel.SendMessageAsync("", false, embedError);
        }

        [Command("list")]
        public async Task CmdListPermissionsAsync(IUser user = null)
        {
            if (user is null)
            {
                var permissions = await _userService.ListPermissionsAsync(Context.User.Id, Context.Guild.Id);
                var embed = new EmbedBuilder()
                    .WithTitle($"Permissions for {Context.User.Username}");

                foreach (var permission in permissions)
                {
                    embed.AddField(permission.Category + "." + permission.Node, "true");
                }

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}
