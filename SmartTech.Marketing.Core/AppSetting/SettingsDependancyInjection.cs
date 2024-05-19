using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.AppSetting
{
    public static class SettingsDependancyInjection
    {
        public static SqlAppSetting SqlSettings { get; } = new();
        public static PosAppSetting PosSettings { get; } = new();
        public static void Init(IConfiguration configuration)
        {
            var sqlSection = configuration.GetSection(SqlAppSetting.SectionName);
            sqlSection.Bind(SqlSettings);
            var posSection = configuration.GetSection(PosAppSetting.SectionName);
            sqlSection.Bind(posSection);
        }
        public static void AddSettingsDependancyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlAppSetting>(configuration.GetSection(SqlAppSetting.SectionName));
            services.Configure<PosAppSetting>(configuration.GetSection(PosAppSetting.SectionName));
            Init(configuration);
        }

    }
}
