using Microsoft.Extensions.Caching.Distributed;

namespace EagleRockService.Services
{
    public interface ICacheService
    {
        public Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration);
        public Task<T> GetCacheValueAsync<T>(string key);
    }
}
