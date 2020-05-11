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
        public async Task CmdAddPermissionAsync(IUser user, PermissionNode node)
        {
            await _userService.AddPermissionAsync(user.Id, Context.Guild.Id, node);
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
                    embed.AddField(Enum.GetName(typeof(PermissionNode), permission.Node), "true");
                }

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}
