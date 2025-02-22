using System;
namespace Core.CrossCuttingConcerns.Cache.Redis;

public interface IRedisCacheService
{

    Task SetDataAsync<T>(string key, T value);
    Task<T> GetDataAsync<T>(string key);

    Task RemoveDataAsync(string key);
}
