using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetLowStockProductsQuery : IRequest<List<Product>> { }
}
