using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Core.AppSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.DependencyInjection
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettingsDependancyInjection(configuration);
            return services;
        }

    }
}
