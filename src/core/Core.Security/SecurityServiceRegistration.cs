using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Security;

public static class SecurityServiceRegistration
{

    public static IServiceCollection AddSecurityDependencies(this IServiceCollection services)
    {

        services.AddScoped<ITokenHelper, JwtHelper>();
        return services;
    }
}
