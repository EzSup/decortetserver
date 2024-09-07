using DecortetServer.Core.DTOs;
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

        public async Task<IEnumerable<Order>> GetByFilter(string? clientName = "", 
            string? phone = "", string? email = "", string? region = "", string? town = "", 
            string? address = "", int minSum = int.MinValue, int maxSum = int.MaxValue,params string[] hasProducts)
        {
            var query = _dbContext.Orders
                .Include(x => x.ProductOrders)
                .ThenInclude(x => x.Product)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(clientName))
            {
                query = query.Where(order => order.ClientName.ToLower().Contains(clientName.ToLower()));
            }
            if (!String.IsNullOrEmpty(phone))
            {
                query = query.Where(order => order.Phone.Contains(phone.Trim()));
            }
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(order => order.Email.Contains(email));
            }
            if (!String.IsNullOrEmpty(region))
            {
                query = query.Where(order => order.Region.ToLower().Contains(region.ToLower()));
            }
            if (!String.IsNullOrEmpty(town))
            {
                query = query.Where(order => order.Town.ToLower().Contains(town.ToLower()));
            }
            if (!String.IsNullOrEmpty(address))
            {
                query = query.Where(order => order.Address.ToLower().Contains(address.ToLower()));
            }
            if (maxSum >= minSum)
            {
                query = query.Where(order => order.totalSum >= minSum && order.totalSum <= maxSum);
            }

            if (hasProducts.Any())
            {
                query = query.Where(order => 
                    hasProducts.All(productName => 
                        order.ProductOrders.Any(po => po.Product.Name == productName)));
            }

            return await query.ToListAsync();
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
