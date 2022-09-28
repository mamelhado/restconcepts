using Rest.Domain.App.Interfaces.Service;
using Rest.Domain.App.Interfaces.Service.Authentication;
using Rest.Infra.CrossCutting.Utils;

namespace Rest.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PrimitiveToken _primitiveToken;

        public JwtMiddleware(RequestDelegate next, PrimitiveToken primitiveToken)
        {
            _next = next;
            _primitiveToken = primitiveToken;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtService.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetByIdAsync(userId.Value);
            }

            await _next(context);
        }
    }
}
