using Desafio.Loja.Application.Commands;
using Desafio.Loja.Application.Common.Communication.Mediator;
using Desafio.Loja.Domain.Entities.Product;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Loja.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrdersController(IMediatorHandler mediatorHandler) : base() 
        {
            _mediatorHandler = mediatorHandler;
        }        

        [HttpPost]
        public async Task<IActionResult> AddItem(Guid id, int quantity, CancellationToken cancellationToken)
        {
            

            var product = new Product("Name",12M);

            var command = new AddOrderItemCommand(CustomerId, product.Id, product.Name, quantity, product.Price);
            await _mediatorHandler.SendCommand(command);

            
            return View();
        }
    }
}
