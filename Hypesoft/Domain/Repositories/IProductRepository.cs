using Hypesoft.Application.DTOs;
using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<List<Product>> SearchByNameAsync(string name);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
        Task<List<Product>> GetByCategoryAsync(string category);

        Task<PagedResponse<Product>> GetPagedAsync(int page, int pageSize);
        Task<List<Product>> GetAllOrderedByCreationAsync();
    }
}
