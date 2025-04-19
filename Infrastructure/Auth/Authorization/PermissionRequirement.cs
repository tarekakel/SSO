using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string[] RequiredRoles { get; }
        public string[] RequiredPermissions { get; }

        public PermissionRequirement(string[] requiredRoles, string[] requiredPermissions)
        {
            RequiredRoles = requiredRoles;
            RequiredPermissions = requiredPermissions;
        }
    }

}
