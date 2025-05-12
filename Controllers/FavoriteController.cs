using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<FavoriteController> _logger;

        public FavoriteController(Context context, ILogger<FavoriteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserFavorites(int userId)
        {
            try
            {
                var favorites = _context.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => new
                    {
                        f.Id,
                        f.ProductId,
                        f.AddedDate,
                        Product = new
                        {
                            f.Product.Name,
                            f.Product.Price,
                            f.Product.Image,
                            f.Product.Description
                        }
                    })
                    .ToList();

                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Favoriler getirilirken hata oluştu. UserId: {userId}");
                return StatusCode(500, "Favoriler getirilirken bir hata oluştu.");
            }
        }

        [HttpPost]
        public IActionResult AddFavorite([FromBody] FavoriteRequest model)
        {
            try
            {
                _logger.LogInformation($"Favori ekleme isteği alındı. UserId: {model?.UserId}, ProductId: {model?.ProductId}");

                if (model == null)
                {
                    _logger.LogWarning("Geçersiz istek formatı: model null");
                    return BadRequest("Geçersiz istek formatı");
                }

                // Kullanıcı kontrolü
                var user = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);
                if (user == null)
                {
                    _logger.LogWarning($"Kullanıcı bulunamadı. UserId: {model.UserId}");
                    return BadRequest($"Kullanıcı bulunamadı (ID: {model.UserId})");
                }

                // Ürün kontrolü
                var product = _context.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (product == null)
                {
                    _logger.LogWarning($"Ürün bulunamadı. ProductId: {model.ProductId}");
                    return BadRequest($"Ürün bulunamadı (ID: {model.ProductId})");
                }

                // Zaten favoriye eklenmiş mi?
                bool alreadyFavorited = _context.Favorites.Any(f => f.UserId == model.UserId && f.ProductId == model.ProductId);
                if (alreadyFavorited)
                {
                    _logger.LogInformation($"Ürün zaten favorilerde. UserId: {model.UserId}, ProductId: {model.ProductId}");
                    return Conflict("Ürün zaten favorilere eklenmiş");
                }

                // Favorilere ekle
                var favorite = new Favorite
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    AddedDate = DateTime.Now
                };

                _context.Favorites.Add(favorite);
                _context.SaveChanges();

                _logger.LogInformation($"Favori başarıyla eklendi. UserId: {model.UserId}, ProductId: {model.ProductId}");
                return Ok(new { success = true, message = "Favorilere eklendi" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Favori eklenirken hata oluştu. UserId: {model?.UserId}, ProductId: {model?.ProductId}");
                return StatusCode(500, "Favori eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult RemoveFavorite(int userId, int productId)
        {
            try
            {
                var favorite = _context.Favorites
                    .FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

                if (favorite == null)
                {
                    return NotFound("Favori bulunamadı");
                }

                _context.Favorites.Remove(favorite);
                _context.SaveChanges();

                _logger.LogInformation($"Favori silindi. UserId: {userId}, ProductId: {productId}");
                return Ok(new { success = true, message = "Favorilerden kaldırıldı" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Favori silinirken hata oluştu. UserId: {userId}, ProductId: {productId}");
                return StatusCode(500, "Favori silinirken bir hata oluştu.");
            }
        }

        [HttpGet("check/{userId}/{productId}")]
        public IActionResult CheckFavorite(int userId, int productId)
        {
            try
            {
                bool isFavorite = _context.Favorites
                    .Any(f => f.UserId == userId && f.ProductId == productId);

                return Ok(new { isFavorite });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Favori kontrolü yapılırken hata oluştu. UserId: {userId}, ProductId: {productId}");
                return StatusCode(500, "Favori kontrolü yapılırken bir hata oluştu.");
            }
        }
    }

    public class FavoriteRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
