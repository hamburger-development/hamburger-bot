using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburger.Core.Models;
using Hamburger.Core.PersistentStorage;

namespace Hamburger.Core.Services
{
    public class UserService
    {
        private readonly IDbStorage _db;

        public UserService(IDbStorage db)
        {
            _db = db;
        }

        public async Task<HamburgerUser> GetUserAsync(HamburgerGuildConfiguration config, ulong id)
        {
            if (_db.Exists<HamburgerUser>(x => x.DiscordUserId == id, config.DiscordGuildId.ToString()))
            {
                var user = await _db.FindOne<HamburgerUser>(x => x.DiscordUserId == id,
                    config.DiscordGuildId.ToString());

                return user;
            }

            return null;
        }

        public async Task CreateUserAsync(HamburgerUser user, HamburgerGuildConfiguration config)
        {
            await _db.StoreOne(user, config.DiscordGuildId.ToString());
        }
    }
}
