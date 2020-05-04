using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Hamburger.Core.Models
{
    public class HamburgerUser
    {
        [BsonId]
        public ulong DiscordUserId { get; set; }
        public string DiscordUsername { get; set; }
        public string DiscordDiscriminator { get; set; }
    }
}
