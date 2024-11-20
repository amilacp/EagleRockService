using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;
using Newtonsoft.Json;
using EagleRockService.Models;

namespace EagleRockService.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;

        public CacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redis.GetDatabase();
            var json = JsonConvert.SerializeObject(value);
            await db.StringSetAsync(key, json, expiration);
        }

        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var cachedData = await db.StringGetAsync(key);
            if (cachedData.HasValue)
            {
                string innerJson = JsonConvert.DeserializeObject<string>(cachedData);
                return JsonConvert.DeserializeObject<T>(innerJson);
            }
            else
                return default;
        }
    }

}
