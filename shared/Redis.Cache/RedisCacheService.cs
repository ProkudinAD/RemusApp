using Microsoft.Extensions.Caching.Distributed;
using Redis.Cache.Cache.Interface;

namespace Redis.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }
    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _cache.GetStringAsync(key);
        return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
    }
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
        };
        var data = System.Text.Json.JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, data, options);
    }
    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}
