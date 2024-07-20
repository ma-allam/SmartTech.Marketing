using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Application.DependencyInjection;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.DependencyInjection;
using SmartTech.Marketing.Persistence.Context;
using Prometheus;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.WebApi.DependencyInjection
{
    public static class WebApiDependencyInjection
    {
        public static void AddWebApilayerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddInitWebApi(services, configuration);
            AddPreLayers(services, configuration);
            AddHealthCheck(services);
            AddDataBase(services);
            AddMapper(services);
            AddCache(services);
        }
        public static void AddHealthCheck(IServiceCollection services)
        {

            var hcBuilder = services.AddHealthChecks();
            //if (SettingsDependancyInjection.AwsSecreteManagerSetting.Enable)
            //{
            //    if (SettingsDependancyInjection.AwsSettings.IsRoleBased)
            //    {
            //        hcBuilder.AddSecretsManager(option =>
            //        {

            //        });
            //    }
            //    else
            //    {
            //        hcBuilder.AddSecretsManager(option =>
            //        {

            //            option.Credentials = new BasicAWSCredentials(
            //                SettingsDependancyInjection.AwsSettings.UserAccessKeyId,
            //                SettingsDependancyInjection.AwsSettings.UserAccessSecretKey);
            //            option.RegionEndpoint =
            //                RegionEndpoint.GetBySystemName(SettingsDependancyInjection.AwsSettings.Region);
            //        });
            //    }
            //}

           hcBuilder.AddNpgSql(SettingsDependancyInjection.PosSettings.ConnectionString!,name: "PostgreSQL");//.AddApplicationInsightsPublisher();


            if (SettingsDependancyInjection.RedisSettings.Enable)
            {
                switch (SettingsDependancyInjection.RedisSettings.RedisClientType)
                {
                    case "ElasticCache":
                        hcBuilder.AddRedis(
                            $"{SettingsDependancyInjection.RedisSettings.ElasticCache.Server}:{SettingsDependancyInjection.RedisSettings.ElasticCache.Port}");
                        break;
                    case "OnPrem":
                        hcBuilder.AddRedis(
                            $"{SettingsDependancyInjection.RedisSettings.OnPrem.Server},password={SettingsDependancyInjection.RedisSettings.OnPrem.Password}");
                        break;
                }
            }
            hcBuilder.AddApplicationInsightsPublisher().ForwardToPrometheus();
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
        public static void AddInitWebApi(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddControllers(Options =>
            //{
            //    Options.Filters.Add(typeof(AuditActionFilter));
            //});
            //services.AddEndpointsApiExplorer();



            services.AddCors(options => options.AddPolicy("CorsPolicy", corsPolicyBuilder =>
            {
                corsPolicyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
            }));

        }
        private static void AddCache(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();
        }

    }
}
