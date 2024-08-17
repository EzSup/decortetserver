using DecortetServer.Core.DTOs;
using DecortetServer.Core.Interfaces.Repositories;
using DecortetServer.Core.Interfaces.Services;
using DecortetServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecortetServer.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<int> CreateOrder(OrderCreateRequest obj)
        {
            var allIds = (await _productRepository.GetAll()).Where(x => x.Available).Select(x => x.Id);
            bool allProductsAreAvailable = obj.ProductWithCounts
                                                .Select(x => x.Product.Id)
                                                .All(item => allIds.Contains(item));
            if (!allProductsAreAvailable)
            {
                return -1;
            }
            
            Order newOrder = new(0, obj.Name, obj.Phone, obj.Email, obj.Region, obj.City, obj.Street + obj.StreetNum, obj.Description, GetPrice(obj), new());
            try
            {
                return await _orderRepository.Create(newOrder);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private decimal GetPrice(OrderCreateRequest obj)
        {
            return obj.ProductWithCounts.Select(x => x.Quantity * x.Product.Price).Sum();
        }
    }
}
