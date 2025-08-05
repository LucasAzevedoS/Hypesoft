using Hypesoft.Domain.Entities;
using Hypesoft.Application.Queries;
using MediatR;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.Application.Handlers
{
    public class GetCategoryAllHandler : IRequestHandler<GetCategoryAllQuery, List<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryAllHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> Handle(GetCategoryAllQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}