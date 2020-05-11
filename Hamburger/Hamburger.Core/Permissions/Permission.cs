namespace Hamburger.Core.Permissions
{
    public class Permission
    {
        public PermissionCategory Category { get; set; }
        public PermissionNode Node { get; set; }
    }

    public enum PermissionCategory
    {
        MODERATION,
        GENERAL
    }

    public enum PermissionNode
    {
        GENERAL_BOTINFO,
        GENERAL_PREFIX,
        MODERATION_BAN,
        //MODERATION_MUTE,
        MODERATION_KICK
    }
}
