using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Application.DependencyInjection;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.DependencyInjection;
using SmartTech.Marketing.Persistence.Context;

namespace SmartTech.Marketing.WebApi.DependencyInjection
{
    public static class WebApiDependencyInjection
    {
        public static void AddWebApilayerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddPreLayers(services, configuration);
            AddDataBase(services);
            AddMapper(services);
        }
        private static void AddPreLayers(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreLayer(configuration);
            services.AddApplicationDependencyInjection(configuration);
        }
        public static void AddMapper(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(WebApiDependencyInjection).Assembly)).CreateMapper();
            services.AddSingleton(m => mapper);

        }
        private static void AddDataBase(IServiceCollection services)
        {
            services.AddDbContext<DatabaseService>(opt => opt.UseNpgsql(SettingsDependancyInjection.PosSettings.ConnectionString!));
            services.AddScoped<IDataBaseService, DatabaseService>();
        }

    }
}
