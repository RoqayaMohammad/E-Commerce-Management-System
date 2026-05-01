using E_Commerce_Management_System.Shared.Entities;

namespace E_Commerce_Management_System.Modules.OrdersModule.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order entity);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
    }
}
