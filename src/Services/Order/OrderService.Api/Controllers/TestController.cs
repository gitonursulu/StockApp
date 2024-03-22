using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;
using OrderService.Domain.Commands;
using OrderService.Domain.Queries;

namespace OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;
        public TestController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderAppService.GetOrderById(new GetOrderByIdQuery { Id = id });

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand OrderCommand)
        {
            await _orderAppService.CreateOrder(OrderCommand);

            return Ok("ben order api post");
        }
    }
}
