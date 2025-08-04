using Hypesoft.Application.Queries;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using MediatR;

namespace Hypesoft.Application.Handlers
{
    public class GetLowStockProductsHandler : IRequestHandler<GetLowStockProductsQuery, List<Product>>
    {
        private readonly IProductRepository _repository;

        public GetLowStockProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return products.Where(p => p.StockQuantity < 10).ToList();
        }
    }
}
