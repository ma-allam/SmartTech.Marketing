using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(typeof(IMediatorImplementor).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IMediatorImplementor).Assembly));

            services.AddHandlers<IBusinessHandler>();
            //services.AddScoped<SignInManager<ApplicationUser>, CustomSignInManager>();
        }
    }
}
