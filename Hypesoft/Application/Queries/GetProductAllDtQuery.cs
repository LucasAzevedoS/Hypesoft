using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetProductAllDtQuery : IRequest<List<Product>>
    {
    }
}
