using Hypesoft.Application.DTOs;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext _context;

        public ProductRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<List<Product>> SearchByNameAsync(string name) =>
            await _context.Products.Find(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();

        public async Task CreateAsync(Product product) =>
            await _context.Products.InsertOneAsync(product);

        public async Task UpdateAsync(Product product) =>
            await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

        public async Task DeleteAsync(string id) =>
            await _context.Products.DeleteOneAsync(p => p.Id == id);

        public async Task<List<Product>> GetByCategoryAsync(string category)
        {
            return await _context.Products.Find(p => p.CategoryId == category).ToListAsync();
        }
        public async Task<PagedResponse<Product>> GetPagedAsync(int page, int pageSize)
        {
          
            var totalItems = await _context.Products.CountDocumentsAsync(_ => true);


            var products = await _context.Products
                .Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PagedResponse<Product>(products, page, pageSize, totalItems);
        }

        public async Task<List<Product>> GetAllOrderedByCreationAsync()
        {
            return await _context.Products
                .Find(_ => true)
                .SortByDescending(p => p.DtCriacao) 
                .ToListAsync();
        }

        public async Task<List<Product>> GetLast5Async()
        {
            return await _context.Products
                .Find(_ => true)
                .SortByDescending(p => p.DtCriacao)
                .Limit(5)
                .ToListAsync();
        }


    }
}
