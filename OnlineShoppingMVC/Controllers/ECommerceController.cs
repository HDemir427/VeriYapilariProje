using Microsoft.AspNetCore.Mvc;
using OnlineShoppingMVC.Models;
using OnlineShoppingMVC.Services;

namespace OnlineShoppingMVC.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly DataStructuresService _dataService;

        public ECommerceController()
        {
            _dataService = new DataStructuresService();
        }

        // Main view
        public IActionResult Index(string category = "All", string searchQuery = "", string activeTab = "products")
        {
            var viewModel = new ECommerceViewModel
            {
                Products = _dataService.GetFilteredProducts(category, searchQuery),
                Categories = _dataService.GetCategories(),
                Cart = _dataService.GetCart(),
                Orders = _dataService.GetOrders(),
                UserHistoryHead = _dataService.GetUserHistory().FirstOrDefault(),
                SelectedCategory = category,
                SearchQuery = searchQuery,
                ActiveTab = activeTab
            };

            return View(viewModel);
        }

        // Add to cart action
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            _dataService.AddToCart(productId);
            return RedirectToAction("Index", new { activeTab = "cart" });
        }

        // Place order action
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            _dataService.PlaceOrder();
            return RedirectToAction("Index", new { activeTab = "orders" });
        }

        // Process next order action
        [HttpPost]
        public IActionResult ProcessNextOrder()
        {
            _dataService.ProcessNextOrder();
            return RedirectToAction("Index", new { activeTab = "orders" });
        }

        // Change tab action
        public IActionResult ChangeTab(string tab)
        {
            return RedirectToAction("Index", new { activeTab = tab });
        }

        // Filter products action
        public IActionResult FilterProducts(string category, string searchQuery)
        {
            return RedirectToAction("Index", new { category, searchQuery, activeTab = "products" });
        }
    }
}
