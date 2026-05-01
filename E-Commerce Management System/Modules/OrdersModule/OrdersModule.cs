using E_Commerce_Management_System.Modules.OrdersModule.Interfaces;
using E_Commerce_Management_System.Modules.OrdersModule.Repositories;
using E_Commerce_Management_System.Modules.OrdersModule.Serives;

namespace E_Commerce_Management_System.Modules.OrdersModule
{
    public static class OrdersModule
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
