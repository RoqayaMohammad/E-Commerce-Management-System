using E_Commerce_Management_System.Modules.ProductsModule.Interfaces;
using E_Commerce_Management_System.Modules.ProductsModule.Repositories;
using E_Commerce_Management_System.Modules.ProductsModule.Serives;

namespace E_Commerce_Management_System.Modules.ProductsModule
{
    public static class ProductsModule
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
