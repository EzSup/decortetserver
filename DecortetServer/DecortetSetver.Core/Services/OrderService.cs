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

        public async Task<IEnumerable<OrderResponse>> GetAll()
        {
            var orders = await _orderRepository.GetByFilter();
            List<OrderResponse> result = orders.Select(x =>
                new OrderResponse()
                {
                    Id = x.Id,
                    ClientName = x.ClientName,
                    Phone = x.Phone,
                    Email = x.Email,
                    Region = x.Region,
                    Town = x.Town,
                    Address = x.Address,
                    Description = x.Desctiption,
                    TotalSum = x.totalSum,
                    ProductsList = x.ProductOrders != null || x.ProductOrders.Count != 0 ? x?.ProductOrders?.Select(x => x.Product?.Name) : null
                }).ToList();
            return result;
        }

        public async Task<IEnumerable<OrderResponse>> GetByFilter(string? clientName = "", string? phone = "", string? email = "",
            string? region = "", string? town = "", string? address = "",
            int minSum = int.MinValue, int maxSum = int.MaxValue, params string[] hasProducts)
        {
            var orders = await _orderRepository.GetByFilter(clientName, phone, email, region, town, address, minSum, maxSum, hasProducts);
            List<OrderResponse> result = orders.Select(x =>
               new OrderResponse()
               {
                   Id = x.Id,
                   ClientName = x.ClientName,
                   Phone = x.Phone,
                   Email = x.Email,
                   Region = x.Region,
                   Town = x.Town,
                   Address = x.Address,
                   Description = x.Desctiption,
                   TotalSum = x.totalSum,
                   ProductsList = x.ProductOrders.Select(x => x.Product.Name)
               }).ToList();
            return result;
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
            foreach (var productWithCount in obj.ProductWithCounts)
            {
                ProductOrder productOrder = new ProductOrder
                {
                    ProductId = productWithCount.Product.Id,
                    Quantity = productWithCount.Quantity,
                    Order = newOrder
                };
                newOrder.ProductOrders.Add(productOrder);
            }
            try
            {
                return await _orderRepository.Create(newOrder);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> DeleteOrder(int id) => await _orderRepository.Delete(id);

        private decimal GetPrice(OrderCreateRequest obj)
        {
            return obj.ProductWithCounts.Select(x => x.Quantity * x.Product.Price).Sum();
        }

        
    }
}
