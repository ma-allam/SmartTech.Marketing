using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using SmartTech.Marketing.Core.Interfaces;
using SmartTech.Marketing.Core.AppSetting;

namespace SmartTech.Marketing.Core.Cache
{
    public class AwsCacheService : ICacheService
    {
        private readonly IDatabase redisDb;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly ILogger<AwsCacheService> _logger;
        private static readonly object CacheLockObject = new object();

        //private readonly RedisClient _redisClient;
        public AwsCacheService(IOptions<RedisAppSetting> redisOption, ILogger<AwsCacheService> logger, IConnectionMultiplexer redisConnection)
        {
            IsEnabled = redisOption.Value.Enable;
            //_redisClient = new RedisClient(redisOption.Value.ElasticCache.Server, redisOption.Value.ElasticCache.Port);
            _logger = logger;
            _redisConnection = redisConnection;
            redisDb = _redisConnection.GetDatabase();

        }

        public bool IsEnabled { get; set; }

        public bool TryGet<T>(string key, out T value)
        {
            _logger.LogInformation($"Redis Enable : {IsEnabled.ToString()}");
            if (!IsEnabled)
            {
                value = default;
                return false;
            }

            if (redisDb == null)
            {

                _logger.LogInformation($"no redis database found");
                value = default;
                return false;
            }
            var cached = redisDb.StringGet(key, CommandFlags.DemandReplica);
            if (!cached.HasValue)
            {
                _logger.LogInformation($"Redis {key} not exists");
                value = default;
                return false;
            }
            else
            {
                _logger.LogInformation($"Redis {key} exists");
                value = JsonConvert.DeserializeObject<T>(cached.ToString(), new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return true;
            }

        }

        public void Remove(string key)
        {
            _logger.LogInformation($"Redis Enable : {IsEnabled.ToString()}");
            if (!IsEnabled)
                return;

            if (redisDb == null)
            {

                _logger.LogInformation($"no redis database found");
                return;
            }
            redisDb.KeyDelete(key, CommandFlags.DemandMaster);
            _logger.LogInformation($"Redis {key} removed");
        }

        public T? Set<T>(string key, T? value, int minutes)
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
            //var redisvalue= redisDb.StringGet(key);
            //if(!redisvalue.HasValue)
            //{
            //    _logger.LogInformation($"Set Operation - Redis {key} not Exists");
            //    lock (CacheLockObject)
            //    {
            //        redisvalue = redisDb.StringGet(key);
            //        if (!redisvalue.HasValue)
            //        {
            redisDb.StringSet(key, data, exp, flags: CommandFlags.DemandMaster);
            //    }
            //}
            _logger.LogInformation($"Set Operation - Redis {key} Set Successfully");
            // }

            return value;
        }
        public T? UpdateOrSet<T>(string key, T? value, int minutes)
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
            var redisDb = _redisConnection.GetDatabase();
            if (redisDb == null)
            {

                _logger.LogInformation($"no redis database found");
                return default;

            }
            lock (CacheLockObject)
            {

                redisDb.StringSet(key, data, exp);

            }
            _logger.LogInformation($"Set Operation - Redis {key} Updated Successfully");


            return value;
        }
        public Task RemoveWithPatternAsync(string keyPattern)
        {
            if (!IsEnabled)
                return Task.CompletedTask;

            return Task.FromResult(true);
        }

        public Task RemoveWithWildCardAsync(string keyRoot)
        {
            if (!IsEnabled)
                return Task.CompletedTask;

            return Task.FromResult(true);
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
            var cached = await redisDb.StringGetAsync(key, CommandFlags.DemandReplica);
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
            //lock(CacheLockObject)
            //{ 
            // var tr=    redisDb.StringSet(key, data, exp, flags: CommandFlags.DemandMaster); 
            //}
            await redisDb.StringSetAsync(key, data, exp, flags: CommandFlags.DemandMaster);

            _logger.LogInformation($"Set Operation - Redis {key} Set Successfully");


            return value;
        }
    }
}
