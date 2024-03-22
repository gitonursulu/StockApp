using MediatR;
using OrderService.Domain.Interfaces;

namespace OrderService.Domain.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderDomainService _orderDomainService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderDomainService orderDomainService, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderDomainService = orderDomainService;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //userservice
            var order = await _orderDomainService.CreateOrder(request);
            await _orderRepository.CreateOrder(order);
            await _unitOfWork.Commit();

            return true;
        }
    }
}
