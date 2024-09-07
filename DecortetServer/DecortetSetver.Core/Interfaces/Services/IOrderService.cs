using DecortetServer.Core.DTOs;
using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrder(OrderCreateRequest obj);
        Task<IEnumerable<OrderResponse>> GetAll();
        Task<bool> DeleteOrder(int id);
        Task<IEnumerable<OrderResponse>> GetByFilter(string? clientName = "",
            string? phone = "", string? email = "", string? region = "", string? town = "",
            string? address = "", int minSum = int.MinValue, int maxSum = int.MaxValue, params string[] hasProducts);
    }
}
