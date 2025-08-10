using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetLast5ProductsQuery : IRequest<List<Product>>
    {
    }
}
