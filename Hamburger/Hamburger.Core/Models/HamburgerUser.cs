using System.Collections.Generic;
using Hamburger.Core.Permissions;
using MongoDB.Bson.Serialization.Attributes;

namespace Hamburger.Core.Models
{
    public class HamburgerUser
    {
        [BsonId]
        public ulong DiscordUserId { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
