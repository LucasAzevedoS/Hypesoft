using Hypesoft.Application.DTOs;
using Hypesoft.Application.Queries;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using MediatR;

namespace Hypesoft.Application.Handlers
{
    public class GetProductPaginationHandler : IRequestHandler<GetProductPagedQuery, PagedResponse<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductPaginationHandler(IProductRepository productRepository) { 
            _productRepository = productRepository;
        }
        public async Task<PagedResponse<Product>> Handle(GetProductPagedQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetPagedAsync(request.Page, request.PageSize);
        }
    }

}
