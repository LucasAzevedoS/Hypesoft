using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Commands
{
    public class UpdateCategoryCommand : IRequest <bool>
    {

        public Category Category { get; }
        public UpdateCategoryCommand (Category category)
        {
            Category = category;
        }
    }
}
