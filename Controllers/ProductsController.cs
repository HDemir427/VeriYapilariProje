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


    }
}


