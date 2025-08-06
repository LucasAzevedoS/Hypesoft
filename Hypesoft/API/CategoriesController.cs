using Hypesoft.Application.Commands;
using Hypesoft.Application.Queries;
using Hypesoft.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("FindById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetCategoryAllQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] Category category)
        {

            if (id != category.Id)
                return BadRequest("ID mismatch");

            var command = new UpdateCategoryCommand(category);
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
