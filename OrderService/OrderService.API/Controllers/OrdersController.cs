using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            // Gelen isteği doğrudan MediatR'a yolluyoruz. 
            // Hangi handler'ın çalışacağını MediatR kendisi bulacak.
            var orderId = await _mediator.Send(command);

            return Ok(new { OrderId = orderId, Message = "Sipariş başarıyla oluşturuldu." });
        }
    }
}
