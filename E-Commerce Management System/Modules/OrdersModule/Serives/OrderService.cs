using AutoMapper;
using E_Commerce_Management_System.Modules.OrdersModule.Dtos;
using E_Commerce_Management_System.Modules.OrdersModule.Interfaces;
using E_Commerce_Management_System.Modules.ProductsModule.Interfaces;
using E_Commerce_Management_System.Shared.Entities;
using E_Commerce_Management_System.Shared.Helpers;

namespace E_Commerce_Management_System.Modules.OrdersModule.Serives
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository; 
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(string customerId, CreateOrderDto dto)
        {
            var orderItems = new List<OrderItem>();
            decimal total = 0;

            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);

                if (product is null)
                    return ApiResponse<OrderResponseDto>.Fail($"Product with ID {itemDto.ProductId} not found.");

                if (!product.IsActive)
                    return ApiResponse<OrderResponseDto>.Fail($"Product '{product.Name}' is no longer available.");

                if (product.StockQuantity < itemDto.Quantity)
                    return ApiResponse<OrderResponseDto>.Fail(
                        $"Insufficient stock for '{product.Name}'. Available: {product.StockQuantity}.");

                product.StockQuantity -= itemDto.Quantity;
                product.UpdatedAt = DateTime.UtcNow;
                await _productRepository.UpdateAsync(product);

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                };

                orderItems.Add(orderItem);
                total += orderItem.UnitPrice * orderItem.Quantity;
            }

            var order = new Order
            {
                CustomerId = customerId,
                ShippingAddress = dto.ShippingAddress,
                TotalAmount = total,
                Status = OrderStatus.Pending,
                OrderItems = orderItems
            };

            var created = await _orderRepository.CreateAsync(order);
            var detailed = await _orderRepository.GetOrderWithDetailsAsync(created.Id);

            return ApiResponse<OrderResponseDto>.Ok(
                _mapper.Map<OrderResponseDto>(detailed), "Order placed successfully.");
        }

        public async Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(string customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId, string customerId)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(orderId);
            if (order is null || order.CustomerId != customerId) return null;
            return _mapper.Map<OrderResponseDto>(order);
        }
    }
}
