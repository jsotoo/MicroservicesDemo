using System;
using AutoMapper.Extensions.ExpressionMapping;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Infrastructure.DbContexts;
using Net.Microservices.CleanArchitecture.Infrastructure.Extensions;
using Net.Microservices.CleanArchitecture.Infrastructure.Identity.Factories;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;
using Net.Microservices.CleanArchitecture.Infrastructure.Resources;
using Net.Microservices.CleanArchitecture.Infrastructure.Shared.Extensions;
using Net.Microservices.CleanArchitecture.Presentation.Framework.Services;
using Net.Microservices.CleanArchitecture.Presentation.Web.Mappings;
using Net.Microservices.CleanArchitecture.Presentation.Web.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddInfrastructureLayer(configuration);
            services.AddSharedServices();
            services.AddApplicationCookie();

            services.AddScoped<LocalizedRolesResolver>();
            services.AddScoped<LoaderService>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AppProfile>();
                config.AddExpressionMapping();
            });

            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddStorageOptions(configuration);
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        private static void AddApplicationCookie(this IServiceCollection services) {
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Shared/AccessDenied";
                options.Cookie.Name = "Net.Microservices.CleanArchitecture.AUTH";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/auth/login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        private static void AddStorageOptions(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<FileStorageOptions>(x => configuration.GetSection("Storage").Bind(x));
        }
    }
}