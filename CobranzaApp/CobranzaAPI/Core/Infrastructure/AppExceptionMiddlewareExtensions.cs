using Microsoft.AspNetCore.Builder;

namespace CobranzaAPI.Core.Infrastructure
{
    public static class AppExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppExceptionMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<AppExceptionMiddleware>();
    }
}