using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Hamburger.Core.Permissions;
using Hamburger.Core.Services;
using Ninject.Activation;

namespace Hamburger.Discord.CommandModules
{
    [Group("permission")]
    class PermissionCommandModule : ModuleBase<ShardedCommandContext>
    {
        private readonly UserService _userService;

        public PermissionCommandModule(UserService userService)
        {
            _userService = userService;
        }

        [Command("add")]
        public async Task AddPermissionAsync(IUser user, PermissionNode node)
        {

        }

    }
}
