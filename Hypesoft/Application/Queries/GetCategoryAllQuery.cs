using Hypesoft.Domain.Entities;
using MediatR;

namespace Hypesoft.Application.Queries
{
    public class GetCategoryAllQuery : IRequest<List<Category>>
    {

        public GetCategoryAllQuery()
        {

        }
    }
}
