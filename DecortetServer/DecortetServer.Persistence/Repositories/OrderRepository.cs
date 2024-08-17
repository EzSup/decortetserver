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
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _dbContext;

        public OrderRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Order obj)
        {
            await _dbContext.Orders.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<bool> Delete(int id)
        {
            return await _dbContext.Products
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _dbContext.Orders.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Order obj)
        {
            return await _dbContext.Orders.Where(x => x.Id == obj.Id)
                .ExecuteUpdateAsync<Order>(setters => setters
                    .SetProperty(o => o.ClientName , obj.ClientName)
                    .SetProperty(p => p.Phone, obj.Phone)
                    .SetProperty(p => p.Email, obj.Email)
                    .SetProperty(p => p.Desctiption, obj.Desctiption)
                    .SetProperty(p => p.Region, obj.Region)
                    .SetProperty(p => p.Town, obj.Town)
                    .SetProperty(p => p.Address, obj.Address)
                    .SetProperty(p => p.ProductOrders, obj.ProductOrders)) == 1;
        }
    }
}
