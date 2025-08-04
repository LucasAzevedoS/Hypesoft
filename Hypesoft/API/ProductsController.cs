using Hypesoft.Application.Commands;
using Hypesoft.Application.DTOs;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("FindByCategory/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var result = await _mediator.Send(new GetProductsByCategoryQuery(category));
            return Ok(result);
        }

        [HttpGet("FindById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            // chamar o MediatR para retornar o produto por ID
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("FindByLowStock")]
        public async Task<IActionResult> GetLowStock()
        {
            var products = await _mediator.Send(new GetLowStockProductsQuery());
            return Ok(products);
        }

        [HttpPatch("estoque")]
        public async Task<IActionResult> UpdateStock(UpdateStockCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
    }
}
