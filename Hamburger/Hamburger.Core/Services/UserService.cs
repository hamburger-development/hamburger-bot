using System.Collections.Generic;
using System.Linq;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;
using System.Threading.Tasks;
using Hamburger.Core.Permissions;
using Hamburger.Core.Providers;
using Hamburger.Logger;

namespace Hamburger.Core.Services
{
    public class UserService
    {
        private readonly IDbStorage _db;
        private readonly ILogger _logger;
        private readonly HamburgerUserProvider _userProvider;

        public UserService(IDbStorage db, ILogger logger ,HamburgerUserProvider userProvider)
        {
            _db = db;
            _logger = logger;
            _userProvider = userProvider;
        }

        public async Task AddPermissionAsync(ulong userId, ulong guildId, PermissionNode node)
        {
            var user = await _userProvider.GetOrCreateUserAsync(userId, guildId);
            user.Permissions.Add(new Permission
            {
                Category = PermissionCategory.GENERAL,
                Node = node
            });

            _logger.Log($"Added permission for {user.DiscordUserId}", LogSeverity.SEVERITY_ALERT);
            await _db.UpdateOne(user,x => x.DiscordUserId == userId , guildId.ToString());
        }

        public async Task<List<Permission>> ListPermissionsAsync(ulong userId, ulong guildId)
        {
            var user = await _userProvider.GetOrCreateUserAsync(userId, guildId);
            return user.Permissions;
        }
    }
}
