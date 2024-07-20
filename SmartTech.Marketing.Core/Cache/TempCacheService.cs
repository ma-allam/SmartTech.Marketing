using Newtonsoft.Json;
using SmartTech.Marketing.Core.Interfaces;

namespace SmartTech.Marketing.Core.Cache
{
    public class TempCacheService : ICacheService
    {
        public bool IsEnabled
        {
            get;
            set;
        }

        public TempCacheService()
        {
            IsEnabled = false;
        }




        public bool TryGet<T>(string key, out T value)
        {

            value = default;
            return false;


        }
        public async Task<T?> TryGetAsync<T>(string key)
        {
            await Task.CompletedTask;


            return default;

        }

        public async Task<T?> SetAsync<T>(string key, T? value, int minutes)
        {
            await Task.CompletedTask;


            return default;
        }


        public T? Set<T>(string key, T? value, int minutes)
        {
            return value;
        }
        public T? UpdateOrSet<T>(string key, T? value, int minutes)
        {
            return value;
        }



        public void Remove(string key)
        {

        }

        public async Task RemoveWithPatternAsync(string keyPattern)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveWithWildCardAsync(string keyRoot)
        {
            await Task.CompletedTask;
        }
    }
}
