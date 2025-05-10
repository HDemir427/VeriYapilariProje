/*
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
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHistoryController : ControllerBase
    {
        private readonly IUserHistoryService _userHistoryService;

        public UserHistoryController(IUserHistoryService userHistoryService)
        {
            _userHistoryService = userHistoryService;
        }

        [HttpPost]
        public IActionResult AddHistory([FromBody] UserHistory history)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _userHistoryService.AddHistory(history);
                return Ok(new { Message = "History added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserHistory(int userId)
        {
            try
            {
                var history = _userHistoryService.GetUserHistory(userId);
                return Ok(history);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
