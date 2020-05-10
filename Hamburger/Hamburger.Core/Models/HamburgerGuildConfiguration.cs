﻿using MongoDB.Bson.Serialization.Attributes;

namespace Hamburger.Core.Models
{
    public class HamburgerGuildConfiguration
    {
        [BsonId]
        public ulong DiscordGuildId { get; set; }
        public string CommandPrefix { get; set; } = "!";
    }
}
