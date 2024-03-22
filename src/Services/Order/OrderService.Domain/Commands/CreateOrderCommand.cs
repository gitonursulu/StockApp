using MediatR;

namespace OrderService.Domain.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public string OrderName { get; set; }
    }
}
