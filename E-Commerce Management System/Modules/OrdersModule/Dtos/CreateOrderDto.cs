using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Management_System.Modules.OrdersModule.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        [MaxLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
