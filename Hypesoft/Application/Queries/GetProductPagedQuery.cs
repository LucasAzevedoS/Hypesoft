using Hypesoft.Application.DTOs;
using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetProductPagedQuery : IRequest<PagedResponse<Product>>
    {
        public int Page { get; }
        public int PageSize { get; }

        public GetProductPagedQuery(int page = 1, int pageSize = 12)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 12 : (pageSize > 100 ? 100 : pageSize);
        }
    }
}