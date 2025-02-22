using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Cache.Redis;

public sealed class RedisCacheService : IRedisCacheService
{

    private readonly IDistributedCache _distributedCache;
    private readonly CacheSettings _cacheSettings;

    public RedisCacheService(IDistributedCache cache, IConfiguration configuration)
    {
        _distributedCache = cache;

        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
    }

    public async Task<T> GetDataAsync<T>(string key)
    {
        byte[] data = await _distributedCache.GetAsync(key);

        if(data == null)
        {
            return default;
        }

        var jsonData = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task RemoveDataAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    public async Task SetDataAsync<T>(string key, T value)
    {
        var jsonData = JsonSerializer.Serialize(value);
        var dataByte = Encoding.UTF8.GetBytes(jsonData);

        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromDays(_cacheSettings.SlidingExpiration)
        };
        await _distributedCache.SetAsync(key,dataByte,options);
    }
}
