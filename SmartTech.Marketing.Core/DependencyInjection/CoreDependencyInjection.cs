using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Auth.Contract;
using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Auth.User;
using SmartTech.Marketing.Core.Crypto;
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
                    ValidIssuer = SettingsDependancyInjection.AuthenticationSettings.Jwt.issuer,
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

    }
}
