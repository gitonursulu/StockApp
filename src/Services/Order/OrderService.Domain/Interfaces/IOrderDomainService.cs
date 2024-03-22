using OrderService.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderDomainService
    {
        Task<OrderService.Domain.Models.Order> CreateOrder(CreateOrderCommand Order);
        Task<OrderService.Domain.Models.Order> GetOrderById(int id);
    }
}
