using MongoDB.Bson.Serialization.Attributes;

namespace Hamburger.Core.Models
{
    public class HamburgerUser
    {
        [BsonId]
        public ulong DiscordUserId { get; set; }
    }
}
