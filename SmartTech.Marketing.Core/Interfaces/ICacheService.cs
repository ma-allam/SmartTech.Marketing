namespace SmartTech.Marketing.Core.Interfaces
{
    public interface ICacheService
    {
        bool IsEnabled
        {
            get;
            set;
        }

        bool TryGet<T>(string key, out T value);

        T? Set<T>(string key, T? value, int minutes);
        Task<T?> TryGetAsync<T>(string key);

        Task<T?> SetAsync<T>(string key, T? value, int minutes);
        T? UpdateOrSet<T>(string key, T? value, int minutes);
        void Remove(string key);
        Task RemoveWithPatternAsync(string keyPattern);
        Task RemoveWithWildCardAsync(string keyRoot);
    }
}
