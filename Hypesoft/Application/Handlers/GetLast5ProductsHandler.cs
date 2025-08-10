using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetLast5ProductsHandler : IRequestHandler<GetLast5ProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetLast5ProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetLast5ProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetLast5Async();
        }
    }
}
