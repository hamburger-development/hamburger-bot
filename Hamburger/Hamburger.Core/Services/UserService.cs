using System;
using Hamburger.Core.Permissions;
using Hamburger.Core.PersistentStorage;
using Hamburger.Core.Providers;
using Hamburger.Logger;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hamburger.Core.Services
{
    public class UserService
    {
        private readonly IDbStorage _db;
        private readonly ILogger _logger;
        private readonly HamburgerUserProvider _userProvider;
        private readonly PermissionService _permissionService;

        public UserService(IDbStorage db, ILogger logger, HamburgerUserProvider userProvider, PermissionService permissionService)
        {
            _db = db;
            _logger = logger;
            _userProvider = userProvider;
            _permissionService = permissionService;
        }

        public async Task<bool> AddPermissionAsync(ulong userId, ulong guildId, string node)
        {
            var split = node.Split('.');
            if (!_permissionService.IsValidPermission(split[0], split[1]))
            {
                return false;
            }

            if (await HasPermissionAsync(userId, guildId, node))
            {
                throw new Exception("User already has permission");
            }
            var user = await _userProvider.GetOrCreateUserAsync(userId, guildId);

            user.Permissions.Add(new Permission
            {
                Category = split[0],
                Node = split[1]
            });

            _logger.Log($"Added permission for {user.DiscordUserId}", LogSeverity.SEVERITY_ALERT);
            await _db.UpdateOne(user, x => x.DiscordUserId == userId, guildId.ToString());
            return true;
        }

        public async Task<bool> HasPermissionAsync(ulong userId, ulong guildId, string node)
        {
            var split = node.Split('.');
            var user = await _userProvider.GetOrCreateUserAsync(userId, guildId);

            if (user.Permissions.Exists(x => x.Category == split[0] && x.Node == split[1]))
            {
                return true;
            }

            return false;
        }

        public async Task<List<Permission>> ListPermissionsAsync(ulong userId, ulong guildId)
        {
            var user = await _userProvider.GetOrCreateUserAsync(userId, guildId);
            return user.Permissions;
        }
    }
}
