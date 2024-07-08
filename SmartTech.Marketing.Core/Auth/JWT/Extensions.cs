using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartTech.Marketing.Core.Auth.JWT
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtHandler, JwtHandler>();
        }
    }
}
