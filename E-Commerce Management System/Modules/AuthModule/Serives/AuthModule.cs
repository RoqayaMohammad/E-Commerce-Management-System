using E_Commerce_Management_System.Modules.AuthModule.Interfaces;

namespace E_Commerce_Management_System.Modules.AuthModule.Serives
{
    public static class AuthModule
    {
        public static IServiceCollection AddAuthModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
