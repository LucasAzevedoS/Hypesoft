using Hypesoft.Application.Queries;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using MediatR;

namespace Hypesoft.Application.Handlers
{
    public class GetProductAllDtHandler : IRequestHandler<GetProductAllDtQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductAllDtHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetProductAllDtQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllOrderedByCreationAsync();
        }
    }
}

