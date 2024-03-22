using MediatR;

namespace OrderService.Domain.Events
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            //TODO : Masstransit bağlantısı kurulacak.

            return Task.CompletedTask;
        }
    }
}
