using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service) => _service = service;

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _service.AddProduct(product);
                return Created($"/products/{product.Id}", product);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            try
            {
                return Ok(_service.GetProduct(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet("search/{keyword}")]
        public IActionResult Search(string keyword) => Ok(_service.Search(keyword));

        [HttpGet("category/{name}")]
        public IActionResult GetByCategory(string name) => Ok(_service.GetByCategory(name));

        [HttpPost("orders")]
        public IActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            _service.ProcessOrder(order);
            return Accepted();
        }

    }
}


