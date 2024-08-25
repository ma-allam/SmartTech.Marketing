using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Auth.Contract;
using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Auth.User;
using SmartTech.Marketing.Core.Cache;
using SmartTech.Marketing.Core.Crypto;
using SmartTech.Marketing.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Core.DependencyInjection
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettingsDependancyInjection(configuration);
            services.AddScoped<IAesCryptoHelper, AesCryptoHelper>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSwagger();
            //AddRedis(services);
            services.AddJwt(configuration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
<<<<<<< HEAD
                    ValidIssuer =SettingsDependancyInjection.AuthenticationSettings.Jwt.issuer,
=======
                    ValidIssuer = SettingsDependancyInjection.AuthenticationSettings.Jwt.issuer,
>>>>>>> master
                    ValidAudience = SettingsDependancyInjection.AuthenticationSettings.Jwt.issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SettingsDependancyInjection.AuthenticationSettings.Jwt.secretKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;



            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            return services;
        }
        //public static void AddRedis(this IServiceCollection services)
        //{

        //    if (!SettingsDependancyInjection.RedisSettings.Enable)
        //    {
        //        services.AddSingleton<ICacheService, TempCacheService>();
        //        return;
        //    }
        //    if (SettingsDependancyInjection.RedisSettings.RedisClientType == RedisClientType.ElasticCache)
        //    {
        //        services.AddSingleton<IConnectionMultiplexer>(provider =>
        //        {

        //            var redisConnectionString = $"{SettingsDependancyInjection.RedisSettings.ElasticCache.Server}:{SettingsDependancyInjection.RedisSettings.ElasticCache.Port}";


        //            ConfigurationOptions configuration = new ConfigurationOptions
        //            {
        //                EndPoints = { SettingsDependancyInjection.RedisSettings.ElasticCache.PrimaryEndPoint },
        //                AllowAdmin = true,
        //                ConnectTimeout = 5000, // Adjust as needed
        //                SyncTimeout = 5000 // Adjust as needed
        //            };

        //            // Add read replicas to the configuration
        //            configuration.EndPoints.Add(SettingsDependancyInjection.RedisSettings.ElasticCache.ReplicaEndPoint);

        //            return ConnectionMultiplexer.Connect(configuration);
        //        });
        //        services.AddSingleton<ICacheService, AwsCacheService>();
        //    }
        //    else
        //    {
        //        if (SettingsDependancyInjection.PosSettings.ConnectionString == RedisClientType.OnPrem)
        //        {
        //            services.AddSingleton<ICacheService, CacheService>();
        //            string redisConnectionString = "";
        //            if (!string.IsNullOrEmpty(SettingsDependancyInjection.RedisSettings.OnPrem.Password))
        //            {
        //                redisConnectionString = $"{SettingsDependancyInjection.RedisSettings.OnPrem.Server}:{SettingsDependancyInjection.RedisSettings.OnPrem.Port},password={SettingsDependancyInjection.RedisSettings.OnPrem.Password}";
        //            }
        //            else
        //            {
        //                redisConnectionString = $"{SettingsDependancyInjection.RedisSettings.OnPrem.Server}:{SettingsDependancyInjection.RedisSettings.OnPrem.Port}";
        //            }


        //            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
        //            services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);
        //        }
        //    }
        //}

       
    }
}
