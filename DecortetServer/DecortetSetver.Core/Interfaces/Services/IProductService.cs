using DecortetServer.Core.DTOs;
using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<int> AddProduct(ProductCreateRequest request);
        Task<IEnumerable<Product>> GetAll();
        Task<bool> UpdateProduct(Product request);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetByFilter(bool? available = null, string? name = "", string? underheader = "", decimal minPrice = 0, decimal maxPrice = decimal.MaxValue);
    }
}
