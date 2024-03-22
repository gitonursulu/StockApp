using MediatR;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Commands;
using OrderService.Domain.Interfaces;
using OrderService.Domain.Queries;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderAppService(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task CreateOrder(CreateOrderCommand OrderCommand)
        {
            await _mediator.Send(OrderCommand);
        }

        public async Task<string> GetOrderById(GetOrderByIdQuery OrderQuery)
        {
            var order = await _mediator.Send(OrderQuery);

            return order;
        }
    }
}