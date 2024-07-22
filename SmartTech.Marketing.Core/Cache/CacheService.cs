using Microsoft.Extensions.Caching.Distributed;
using SmartTech.Marketing.Core.AppSetting;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.Cache
{
    public class CacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;

        public CacheService(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
        }

        private string GenerateCacheKey(string[] entityNames, string key)
        {
            var entities = string.Join("_", entityNames);
            return $"Cache_{entities}_{key}";
        }



        public async Task InvalidateCacheAsync(string entityName)
        {
            var endpoints = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoints.First());
            var keys = new List<RedisKey>();

            var pattern = $"*_{entityName}_*";
            await foreach (var redisKey in server.KeysAsync(pattern: pattern))
            {
                keys.Add(redisKey);
            }

            foreach (var redisKey in keys)
            {
                await _cache.RemoveAsync(redisKey.ToString().Replace(SettingsDependancyInjection.RedisSettings.OnPrem.InstanceName, ""));
            }
        }

        public async Task<string> GetCacheAsync(string key)
        {
            var endpoints = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoints.First());

            var pattern = $"*{key}*";
            var keys = server.Keys(pattern: pattern).ToList();

            foreach (var redisKey in keys)
            {
                
                var cachedResponse = await _cache.GetStringAsync(redisKey.ToString().Replace(SettingsDependancyInjection.RedisSettings.OnPrem.InstanceName,""));
                if (!string.IsNullOrEmpty(cachedResponse))
                {
                    return cachedResponse;
                }
            }

            return null;
        }

        public async Task SetCacheAsync(string[] entityNames, string key, string data)
        {
            var cacheKey = GenerateCacheKey(entityNames, key);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)  // Set cache duration
            };
            await _cache.SetStringAsync(cacheKey, data, options);
        }
    }
}
