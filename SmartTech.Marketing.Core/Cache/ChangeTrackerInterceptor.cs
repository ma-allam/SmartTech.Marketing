using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.Cache
{
    public class ChangeTrackerInterceptor : SaveChangesInterceptor
    {
        private readonly CacheService _cacheService;

        public ChangeTrackerInterceptor(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            InvalidateCache(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await InvalidateCacheAsync(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void InvalidateCache(DbContext context)
        {
            var entityNames = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => e.Entity.GetType().Name)
                .Distinct();

            foreach (var entityName in entityNames)
            {
                _cacheService.InvalidateCacheAsync(entityName).Wait();
            }
        }

        private async Task InvalidateCacheAsync(DbContext context)
        {
            var entityNames = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .Select(e => e.Entity.GetType().Name)
                .Distinct();

            foreach (var entityName in entityNames)
            {
                await _cacheService.InvalidateCacheAsync(entityName);
            }
        }
    }
}
