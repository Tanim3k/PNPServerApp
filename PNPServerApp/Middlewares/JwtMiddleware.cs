using PNPServerApp.Interfaces;

namespace PNPServerApp.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUsersService userService, IJWTService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userName = jwtService.ValidateToken(token);
            if (userName != null)
            {
                // attach user to context on successful jwt validation
                context.Items["LoggedInUser"] = userService.GetUserByUserName(userName);
            }

            await _next(context);
        }
    }
}
