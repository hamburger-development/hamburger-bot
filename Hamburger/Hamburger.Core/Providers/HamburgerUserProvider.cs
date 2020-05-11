using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburger.Core.Models;
using Hamburger.Core.Permissions;
using Hamburger.Core.PersistentStorage;

namespace Hamburger.Core.Providers
{
    public class HamburgerUserProvider
    {
        private readonly IDbStorage _db;

        public HamburgerUserProvider(IDbStorage db)
        {
            _db = db;
        }

        public async Task<HamburgerUser> GetOrCreateUserAsync(ulong userId, ulong guildId)
        {
            var user = await _db.FindOne<HamburgerUser>(x => x.DiscordUserId == userId, guildId.ToString());
            return EnsureUserExists(user, userId, guildId);
        }

        private HamburgerUser EnsureUserExists(
            HamburgerUser user,
            ulong userId,
            ulong guildId)
        {
            if (user is null)
            {
                user = new HamburgerUser()
                {
                    DiscordUserId = userId,
                    Permissions = new List<Permission>()
                };
                _db.StoreOne(user, guildId.ToString());
            }

            return user;
        }
    }
}
