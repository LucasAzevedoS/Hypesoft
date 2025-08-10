using Hypesoft.Application.Commands;
using Hypesoft.Application.DTOs;
using Hypesoft.Application.Queries;
using Hypesoft.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Hypesoft.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("GetByCategory/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var result = await _mediator.Send(new GetProductsByCategoryQuery(category));
            return Ok(result);
        }

        [HttpGet("paged")]

        public async Task<IActionResult> GetProductsPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12)
        {
            var query = new GetProductPagedQuery(page, pageSize);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetLast")]
        public async Task<IActionResult> GetLast5Products()
        {
            var query = new GetLast5ProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetLowStock")]
        public async Task<IActionResult> GetLowStock()
        {
            var products = await _mediator.Send(new GetLowStockProductsQuery());
            return Ok(products);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetProductAllQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet("GetAllDt")]
        public async Task<IActionResult> GetAllProductsDt()
        {
            var query = new GetProductAllDtQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            Log.Information("Produto {ProductName} criado por {User} em {Time}",
    command.Name, User.Identity?.Name ?? "usuário não autenticado", DateTime.UtcNow);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("EditById/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            
            if (id != product.Id)
                return BadRequest("ID mismatch");

            var command = new UpdateProductCommand(product);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound(); 

            return NoContent(); 
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return NoContent(); 
        }
    }
}
