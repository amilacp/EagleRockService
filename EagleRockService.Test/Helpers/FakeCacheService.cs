using EagleRockService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRockService.Test.Helpers
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    public class FakeCacheService : ICacheService
    {
        // In-memory storage for simulating cache
        private readonly ConcurrentDictionary<string, (string Value, DateTime Expiration)> _cache = new();

        public Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            // Serialize the value to JSON and set the expiration time
            var json = JsonConvert.SerializeObject(value);
            var expirationTime = DateTime.UtcNow.Add(expiration);

            _cache[key] = (json, expirationTime);
            return Task.CompletedTask;
        }

        public Task<T> GetCacheValueAsync<T>(string key)
        {
            if (_cache.TryGetValue(key, out var cacheEntry))
            {
                // Check if the cache entry is expired
                if (cacheEntry.Expiration > DateTime.UtcNow)
                {
                    // Deserialize and return the value
                    var json = JsonConvert.DeserializeObject<string>(cacheEntry.Value);
                    var result = JsonConvert.DeserializeObject<T>(json);
                    return Task.FromResult(result);
                }
                else
                {
                    // Remove expired entry
                    _cache.TryRemove(key, out _);
                }
            }

            // Return default if key not found or expired
            return Task.FromResult(default(T));
        }
    }

}
