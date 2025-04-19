using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = context.User;

            if (!user.Identity?.IsAuthenticated ?? true)
                return Task.CompletedTask;

            // Check custom claims
            var isActive = user.FindFirst("IsActive")?.Value == "true";
            var isDeleted = user.FindFirst("IsDeleted")?.Value == "false";

            if (!isActive || isDeleted)
                return Task.CompletedTask;

            // Check roles
            if (requirement.RequiredRoles.Any())
            {
                var hasRole = requirement.RequiredRoles.Any(role =>
                    user.IsInRole(role));

                if (!hasRole)
                    return Task.CompletedTask;
            }

            // Check permissions (custom claims)
            if (requirement.RequiredPermissions.Any())
            {
                var userPermissions = user.FindAll("Permission").Select(p => p.Value);
                if (!requirement.RequiredPermissions.All(p => userPermissions.Contains(p)))
                    return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
