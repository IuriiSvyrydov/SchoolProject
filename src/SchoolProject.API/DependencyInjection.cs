using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using System.Globalization;

namespace SchoolProject.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLocalizationExtension(this IServiceCollection services)
        {
            services.AddLocalization(opt =>
        {
            opt.ResourcesPath = "";
        });
            services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCulture = new List<CultureInfo>
            {
                    new("es-US"),
                    new("uk"),
                    new("fr-FR")
            };
            options.DefaultRequestCulture = new RequestCulture("uk");
            options.SupportedCultures = supportedCulture;
            options.SupportedUICultures = supportedCulture;
        });

            return services;
        }
        public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;

                }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}