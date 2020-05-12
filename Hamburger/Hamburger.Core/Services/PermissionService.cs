using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamburger.Core.Configuration;

namespace Hamburger.Core.Services
{
    public class PermissionService
    {
        private readonly IConfiguration _config;

        public PermissionService(IConfiguration config)
        {
            _config = config;
        }

        public bool IsValidPermission(string category, string node)
        {
            if (_config.ValidPermissions.Exists(x=> x.Category == category && x.Node == node))
            {
                return true;
            }

            return false;
        }
    }
}
