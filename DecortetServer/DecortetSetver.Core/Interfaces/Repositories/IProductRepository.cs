using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<int> Create(Product obj);
        Task<bool> Delete(int id);
        Task<bool> Update(Product obj);
        Task<IEnumerable<Product>> GetByFilter(bool? available = null, string? name = "", string? underheader = "", decimal minPrice = 0, decimal maxPrice = decimal.MaxValue);
    }
}
