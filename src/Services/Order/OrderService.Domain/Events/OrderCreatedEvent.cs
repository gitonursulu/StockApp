using MediatR;

namespace OrderService.Domain.Events
{
    public class OrderCreatedEvent : INotification
    {
        public Guid OrderId { get; }
        public string UserId { get; }
        public DateTime OrderDate { get; }

        public OrderCreatedEvent(Guid orderId, string userId, DateTime orderDate)
        {
            OrderId = orderId;
            UserId = userId;
            OrderDate = orderDate;
        }
    }
}
