
namespace Clothing.API.Infrastructure.Middlewares
{
    public class CorsAccessControlMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private readonly IConfiguration _configuration;
        public CorsAccessControlMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public Task InvokeAsync(HttpContext context)
        {
            
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(AccessControlAllowOrigin))
                {
                    var clothingURL = _configuration.GetSection("ClothingURL").Value;
                    context.Response.Headers.Add(AccessControlAllowOrigin, clothingURL);
                }
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}
