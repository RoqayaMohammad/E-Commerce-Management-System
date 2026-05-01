using E_Commerce_Management_System.Shared.Entities;

namespace E_Commerce_Management_System.Modules.OrdersModule.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }
}
