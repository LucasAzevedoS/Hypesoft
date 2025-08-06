using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Product Product { get; }

        public UpdateProductCommand(Product product)
        {
            Product = product;
        }
    }
}