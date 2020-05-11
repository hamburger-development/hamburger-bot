using Hamburger.Core.Permissions;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Hamburger.Core.Models
{
    public class HamburgerGuildConfiguration
    {
        [BsonId]
        public ulong DiscordGuildId { get; set; }
        public string CommandPrefix { get; set; } = "!";
        public IEnumerable<PermissionGroup> PermissionGroups { get; set; }
    }
}
