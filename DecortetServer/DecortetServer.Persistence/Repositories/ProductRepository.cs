using DecortetServer.Core.Interfaces.Repositories;
using DecortetServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Persistense.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Product obj)
        {
            await _dbContext.Products.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<bool> Delete(int id)
        {
            return await _dbContext.Products
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _dbContext.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Product obj)
        {
            return await _dbContext.Products.Where(x => x.Id == obj.Id)
                .ExecuteUpdateAsync<Product>(setters => setters
                    .SetProperty(p => p.Available, obj.Available)
                    .SetProperty(p => p.Name, obj.Name)
                    .SetProperty(p => p.Underheader, obj.Underheader)
                    .SetProperty(p => p.Price, obj.Price)
                    .SetProperty(p => p.Description, obj.Description)) == 1;
                
        }
    }
}
