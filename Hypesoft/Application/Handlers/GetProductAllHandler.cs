using Hypesoft.Domain.Entities;
using Hypesoft.Application.Queries;
using MediatR;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.Application.Handlers
{
    public class GetProductAllHandler : IRequestHandler<GetProductAllQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductAllHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetProductAllQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}