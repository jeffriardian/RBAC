using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RBAC.Core.JWT
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeRolePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        private readonly string[] _permissions;

        public AuthorizeRolePermissionAttribute(string[] roles = null, string[] permissions = null)
        {
            _roles = roles ?? Array.Empty<string>();
            _permissions = permissions ?? Array.Empty<string>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Validasi role
            if (_roles.Any() && !_roles.Any(role => user.IsInRole(role)))
            {
                context.Result = new ForbidResult();
                return;
            }

            // Validasi permission
            if (_permissions.Any())
            {
                var userPermissions = user.Claims
                    .Where(c => c.Type == "Permission")
                    .Select(c => c.Value)
                    .ToList();

                if (!_permissions.Any(permission => userPermissions.Contains(permission)))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }

}
