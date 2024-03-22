using OrderService.Domain.Commands;
using OrderService.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task CreateOrder(CreateOrderCommand OrderCommand);
        Task<string> GetOrderById(GetOrderByIdQuery OrderQuery);
    }
}
