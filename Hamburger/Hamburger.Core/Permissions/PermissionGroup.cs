using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.Core.Permissions
{
    public class PermissionGroup
    {
        public IEnumerable<Permission> Permissions { get; set; }
        public ulong RoleID { get; set; }
    }
}
