using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserHistoryController : ControllerBase
    {
        private readonly Context _context;

        public UserHistoryController(Context context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserHistory>>> GetByUser(int userId)
        {
            return await _context.UserHistories
                .Where(h => h.UserId == userId)
                .Include(h => h.Product)
                .OrderByDescending(h => h.ActionDate)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserHistory history)
        {
            history.ActionDate = DateTime.UtcNow;
            _context.UserHistories.Add(history);
            await _context.SaveChangesAsync();
            return Ok(history);
        }
    }

}
