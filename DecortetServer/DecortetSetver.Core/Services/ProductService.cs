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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IPhotoService _photoService;

        public ProductService(IProductRepository repository, IPhotoService photoService)
        {
            _repository = repository;   
            _photoService = photoService;
        }

        public async Task<int> AddProduct(ProductCreateRequest request)
        {
            var links = await _photoService.AddPhotosAsync(request.Photos);
            var newProduct = new Product(0, request.Name, request.Price, request.Underheader, request.Description, links.ToArray(), request.Available);
            return await _repository.Create(newProduct);
        }

        public async Task<bool> UpdateProduct(Product request)
        {
            return await _repository.Update(request);
        }

        public async Task<IEnumerable<Product>> GetAll() => await _repository.GetAll();
    }
}
