using OrderService.Domain.Interfaces;
using OrderService.Domain.Models;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SQLDbContext _sqlDbContext;
        public OrderRepository(SQLDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task CreateOrder(Domain.Models.Order Order)
        {
            await _sqlDbContext.Orders.AddAsync(Order);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order = await _sqlDbContext.Orders.FindAsync(id);
            return order;
        }
    }
}
