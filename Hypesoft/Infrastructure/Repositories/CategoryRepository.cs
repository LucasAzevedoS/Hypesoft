using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoDbContext _context;

        public CategoryRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _context.Categories.Find(_ => true).ToListAsync();

        public async Task<Category?> GetByIdAsync(string id) =>
            await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category category) =>
            await _context.Categories.InsertOneAsync(category);

        public async Task UpdateAsync(Category category) =>
            await _context.Categories.ReplaceOneAsync(c => c.Id == category.Id, category);

        public async Task DeleteAsync(string id) =>
            await _context.Categories.DeleteOneAsync(c => c.Id == id);
    }
}
