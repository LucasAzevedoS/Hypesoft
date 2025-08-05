using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetProductAllQuery : IRequest<List<Product>>
    {
        public GetProductAllQuery()
        {
            
        }
    }
}
