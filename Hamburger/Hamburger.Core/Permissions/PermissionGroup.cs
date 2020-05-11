using System.Collections.Generic;

namespace Hamburger.Core.Permissions
{
    public class PermissionGroup
    {
        public IEnumerable<Permission> Permissions { get; set; }
        public ulong RoleID { get; set; }
    }
}
