using Hypesoft.Application.Commands;
using Hypesoft.Domain.Repositories;
using MediatR;

namespace Hypesoft.Application.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.Category.Id);
            if (existingCategory == null) // Se NÃO existe
            {
                return false; // Categoria não encontrada
            }

            await _categoryRepository.UpdateAsync(request.Category);
            return true; // Atualização bem-sucedida
        }
    }
}