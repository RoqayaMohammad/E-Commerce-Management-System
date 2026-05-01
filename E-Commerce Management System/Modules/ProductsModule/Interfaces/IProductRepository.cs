using E_Commerce_Management_System.Shared.Entities;

namespace E_Commerce_Management_System.Modules.ProductsModule.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product entity);
        Task<Product> UpdateAsync(Product entity);
        Task DeleteAsync(Product entity);
    }
}
