using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Hamburger.Core.Models
{
    public class HamburgerGuildConfiguration
    {
        [BsonId]
        public ulong DiscordGuildId { get; set; }
        public string DiscordGuildName { get; set; }
        public string CommandPrefix { get; set; } = "!";
    }
}
