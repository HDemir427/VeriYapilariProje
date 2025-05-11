using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;
using System;
using System.Linq;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddFavorite([FromBody] FavoriteRequest model)
        {
            using var context = new Context();

            // Kullanıcı ve ürün kontrolü
            var user = context.Users.FirstOrDefault(u => u.UserId == model.UserId);
            var product = context.Products.FirstOrDefault(p => p.ProductId == model.ProductId);

            if (user == null || product == null)
            {
                return BadRequest("Kullanıcı veya ürün bulunamadı");
            }

            // Zaten favoriye eklenmiş mi?
            bool alreadyFavorited = context.Favorites.Any(f => f.UserId == model.UserId && f.ProductId == model.ProductId);
            if (alreadyFavorited)
            {
                return Conflict("Ürün zaten favorilere eklenmiş");
            }

            // Favorilere ekle
            var favorite = new Favorite
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                AddedDate = DateTime.Now
            };

            _ = context.Favorites.Add(favorite);
            context.SaveChanges();

            return Ok(new { success = true, message = "Favorilere eklendi" });
        }
    }

    public class FavoriteRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
