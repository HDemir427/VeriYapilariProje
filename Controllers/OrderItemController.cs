using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly Context _context;

        public OrderItemController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
    }

}
