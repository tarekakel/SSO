using Application.Services;
using Application.Services.Security;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Security;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Security;
using Infrastructure.Auth;
using Persistence.Repositories;
using Persistence.Repositories.Security;

namespace SSO.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            services.AddScoped<IUserSessionService, UserSessionService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


            // Security-related Repositories
            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();


            // Services
            services.AddScoped<ISystemService, SystemService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
