using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<int> Create(Order obj);
        Task<bool> Delete(int id);
        Task<bool> Update(Order obj);
    }
}
