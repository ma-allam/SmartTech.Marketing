﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Interfaces;
using StackExchange.Redis;

namespace SmartTech.Marketing.Core.Cache
{
    public class CacheServiceV2 : ICacheService
    {
        private readonly IDatabase redisDb;
        private static readonly object CacheLockObject = new object();
        public bool IsEnabled
        {
            get;
            set;
        }

        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<CacheService> _logger;
        public CacheServiceV2(IOptions<RedisAppSetting> redisSetting, IConnectionMultiplexer connectionMultiplexer, ILogger<CacheService> logger)
        {
            IsEnabled = redisSetting.Value.Enable;

            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
            redisDb = connectionMultiplexer.GetDatabase();

        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (!IsEnabled)
                    return Task.FromResult(HealthCheckResult.Unhealthy("Redis Not Enabled"));
                var database = _connectionMultiplexer.GetDatabase();
                database.StringGet("health");
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception exception)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(exception.Message));
            }
        }
        public bool TryGet<T>(string key, out T value)
        {
            if (!IsEnabled)
            {
                value = default;
                return false;
            }

            var cached = redisDb.StringGet(key);
            if (string.IsNullOrEmpty(cached))
            {
                value = default;
                return false;
            }
            else
            {
                value = JsonConvert.DeserializeObject<T>(cached, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return true;
            }

        }


        public T? Set<T>(string key, T? value, int minutes)
        {
            if (!IsEnabled)
                return default;
            _logger.LogInformation($"Start Caching {key}");
            var keyValue = redisDb.StringGet(key);

            if (!keyValue.HasValue)
            {
                lock (CacheLockObject)
                {
                    keyValue = redisDb.StringGet(key);
                    if (!keyValue.HasValue)
                    {
                        redisDb.StringSet(key, JsonConvert.SerializeObject(value, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }), expiry: TimeSpan.FromMinutes(minutes), flags: CommandFlags.PreferMaster);
                    }
                }
            }
            return value;
        }
        public T? UpdateOrSet<T>(string key, T? value, int minutes)
        {
            if (!IsEnabled)
                return default;
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes),
                SlidingExpiration = TimeSpan.FromMinutes(minutes)
            };
            _logger.LogInformation($"Start Caching {key}");

            lock (CacheLockObject)
            {

                redisDb.StringSet(key, JsonConvert.SerializeObject(value, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }), expiry: TimeSpan.FromMinutes(minutes), flags: CommandFlags.PreferMaster);

            }

            return value;
        }

        public void Remove(string key)
        {
            if (!IsEnabled)
                return;
            redisDb.KeyDelete(key);
        }
        public async Task RemoveWithPatternAsync(string keyPattern)
        {
            if (!IsEnabled)
                return;
            if (string.IsNullOrWhiteSpace(keyPattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(keyPattern));

            // get all the keys* and remove each one
            foreach (var key in GetKeys(keyPattern))
            {
                await redisDb.KeyDeleteAsync(key);
            }
        }
        public async Task RemoveWithWildCardAsync(string keyRoot)
        {
            if (!IsEnabled)
                return;
            if (string.IsNullOrWhiteSpace(keyRoot))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(keyRoot));

            // get all the keys* and remove each one
            foreach (var key in GetKeys(keyRoot + "*"))
            {
                await redisDb.KeyDeleteAsync(key);
            }
        }

        public IEnumerable<string> GetKeys(string pattern)
        {

            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            foreach (var endpoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                foreach (var key in server.Keys(pattern: pattern))
                {
                    yield return key.ToString();
                }
            }
        }

        public IEnumerable<RedisFeatures> GetRedisFeatures()
        {
            foreach (var endpoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                yield return server.Features;
            }
        }
        public async Task<T?> TryGetAsync<T>(string key)
        {
            _logger.LogInformation($"Redis Enable : {IsEnabled.ToString()}");
            if (!IsEnabled)
            {

                return default;
            }

            if (redisDb == null)
            {

                _logger.LogInformation($"no redis database found");

                return default;
            }
            var cached = await redisDb.StringGetAsync(key, CommandFlags.PreferReplica);
            if (!cached.HasValue)
            {
                _logger.LogInformation($"Redis {key} not exists");
                return default;
            }
            else
            {
                _logger.LogInformation($"Redis {key} exists");
                return JsonConvert.DeserializeObject<T>(cached.ToString(), new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            }

        }

        public async Task<T?> SetAsync<T>(string key, T? value, int minutes)
        {
            _logger.LogInformation($"Set Operation - Redis Enable : {IsEnabled.ToString()}");
            if (!IsEnabled)
                return default;

            var exp = TimeSpan.FromMinutes(minutes);
            var data = JsonConvert.SerializeObject(value, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });

            if (redisDb == null)
            {

                _logger.LogInformation($"no redis database found");
                return default;

            }

            await redisDb.StringSetAsync(key, data, exp, flags: CommandFlags.PreferMaster);

            _logger.LogInformation($"Set Operation - Redis {key} Set Successfully");


            return value;
        }
    }
}
