using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetProductsByCategoryQuery : IRequest<List<Product>>
    {
        public string Category { get; set; }

        public GetProductsByCategoryQuery(string category)
        {
            Category = category;
        }
    }
}
