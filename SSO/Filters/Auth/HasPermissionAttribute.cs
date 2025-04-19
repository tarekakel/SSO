using Microsoft.AspNetCore.Authorization;

namespace SSO.Filters.Auth
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string[] roles, string[] permissions)
        {
            Policy = PermissionPolicyBuilder.BuildPolicyName(roles, permissions);
        }
    }

    public static class PermissionPolicyBuilder
    {
        public static string BuildPolicyName(string[] roles, string[] permissions)
        {
            return $"PermissionPolicy:{string.Join(",", roles)}:{string.Join(",", permissions)}";
        }
    }

}
