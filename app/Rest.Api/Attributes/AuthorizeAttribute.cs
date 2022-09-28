using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rest.Domain.App;
using System.Security.Claims;
using Rest.Infra.CrossCutting.Utils.Enum;

namespace Rest.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;
            
            // authorization
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var user = new User();
                user.Id = int.Parse(userClaims.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value);
                user.Username = userClaims.Where(x => x.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value;
                user.Role = (Role)Enum.Parse(typeof(Role),userClaims.Where(x => x.Type == ClaimTypes.Role)?.FirstOrDefault()?.Value);
                
                if (user == null || (_roles.Any() && !_roles.Contains((Role)user.Role)))
                {
                    // not logged in or role not authorized
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
