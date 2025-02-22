using Core.CrossCuttingConcerns.Cache.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns;

public static class Extensions
{
    public static IServiceCollection AddRedisDistributedCacheDependency(this IServiceCollection services)
    {

        services.AddScoped<IRedisCacheService,RedisCacheService>();
        return services;
    }
}
