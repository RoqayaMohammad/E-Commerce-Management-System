using E_Commerce_Management_System.Modules.OrdersModule.Interfaces;
using E_Commerce_Management_System.Shared.Data;
using E_Commerce_Management_System.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Management_System.Modules.OrdersModule.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId) =>
            await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId) =>
            await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
