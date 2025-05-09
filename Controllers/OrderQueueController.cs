using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderQueueController : ControllerBase
    {
        private readonly Context _context;

        public OrderQueueController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderQueue>>> GetAll()
        {
            return await _context.OrderQueues
                .Include(q => q.Order)
                .ToListAsync();
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> Enqueue(int orderId)
        {
            var queueItem = new OrderQueue
            {
                OrderId = orderId,
                QueueDate = DateTime.UtcNow
            };
            _context.OrderQueues.Add(queueItem);
            await _context.SaveChangesAsync();
            return Ok(queueItem);
        }

        [HttpDelete("dequeue")]
        public async Task<IActionResult> Dequeue()
        {
            var item = await _context.OrderQueues.OrderBy(q => q.QueueDate).FirstOrDefaultAsync();
            if (item == null) return NotFound("Kuyruk boş");
            _context.OrderQueues.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
    }


}
