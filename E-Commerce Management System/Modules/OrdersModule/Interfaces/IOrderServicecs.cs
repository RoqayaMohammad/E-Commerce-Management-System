using E_Commerce_Management_System.Modules.OrdersModule.Dtos;
using E_Commerce_Management_System.Shared.Helpers;

namespace E_Commerce_Management_System.Modules.OrdersModule.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(string customerId, CreateOrderDto dto);
        Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(string customerId);
        Task<OrderResponseDto?> GetOrderByIdAsync(int orderId, string customerId);
    }
}
