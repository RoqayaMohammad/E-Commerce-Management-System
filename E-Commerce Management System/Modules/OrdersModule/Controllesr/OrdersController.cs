using E_Commerce_Management_System.Modules.OrdersModule.Dtos;
using E_Commerce_Management_System.Modules.OrdersModule.Interfaces;
using E_Commerce_Management_System.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_Management_System.Modules.OrdersModule.Controllesr
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = Roles.Customer)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>[Customer] Place a new order.</summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _orderService.CreateOrderAsync(customerId, dto);

            return result.Success
                ? CreatedAtAction(nameof(GetOrderById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        /// <summary>[Customer] Get all my orders.</summary>
        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var orders = await _orderService.GetMyOrdersAsync(customerId);
            return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.Ok(orders));
        }

        /// <summary>[Customer] Get a specific order by ID (must belong to caller).</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var order = await _orderService.GetOrderByIdAsync(id, customerId);

            if (order is null)
                return NotFound(ApiResponse<OrderResponseDto>.Fail($"Order {id} not found."));

            return Ok(ApiResponse<OrderResponseDto>.Ok(order));
        }
    }
}
