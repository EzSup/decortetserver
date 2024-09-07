using DecortetServer.Models;
using Microsoft.AspNetCore.Mvc;
using DecortetServer.Core.Models;
using DecortetServer.Core.DTOs;
using DecortetServer.Core.Interfaces.Services;
using DecortetServer.Core.Interfaces.Repositories;


namespace DecortetServer.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public AdminController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            return Ok(await _orderService.GetAll());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            return Ok(await _productService.GetByFilter());
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            var result = await _productService.AddProduct(request) > 0;
            if (!result)
            {
                return BadRequest("Помилка в виконанні!");
            }
            return Ok("Успішно додано!");
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody]Product request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            var result = await _productService.UpdateProduct(request);
            if (!result)
            {
                return BadRequest("Помилка в виконанні!");
            }
            return Ok("Успішно оновлено!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            var result = await _productService.DeleteProduct(id);
            if (!result)
            {
                return BadRequest("Помилка в виконанні!");
            }
            return Ok("Успішно видалено!");
        }
    }
}
