using DecortetServer.Models;
using Microsoft.AspNetCore.Mvc;
using DecortetServer.Core.Models;
using DecortetServer.Core.DTOs;
using DecortetServer.Core.Interfaces.Services;
using DecortetServer.Core.Interfaces.Repositories;

namespace DecortetServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public UserController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                var products = await _productService.GetByFilter(true);
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Buy([FromBody] OrderCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Помилка в переданих даних!");
            }
            var result =  await _orderService.CreateOrder(request) > 0;
            if(!result) 
            {
                return BadRequest("Помилка в виконанні!");
            }
            return Ok("Успішно додано!");
        }
    }
}
