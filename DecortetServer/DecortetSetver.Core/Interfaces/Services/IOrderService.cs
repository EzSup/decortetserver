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
    }
}
