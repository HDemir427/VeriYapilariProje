using OnlineShoppingMVC.Models;

namespace OnlineShoppingMVC.Services
{
    public class DataStructuresService
    {
        // Hash table for products (simplified with Dictionary)
        private Dictionary<int, Product> _productHashTable;

        // Queue for orders
        private Queue<Order> _orderQueue;

        // Head of the linked list for user history
        private UserActivity _userHistoryHead;

        // List of categories (for the tree structure)
        private List<string> _categories;

        // Shopping cart
        private List<CartItem> _cart;

        public DataStructuresService()
        {
            InitializeDataStructures();
        }

        private void InitializeDataStructures()
        {
            // Initialize product hash table
            _productHashTable = new Dictionary<int, Product>
        {
            { 1, new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics", Stock = 25 } },
            { 2, new Product { Id = 2, Name = "Smartphone", Price = 699.99m, Category = "Electronics", Stock = 50 } },
            { 3, new Product { Id = 3, Name = "T-shirt", Price = 19.99m, Category = "Clothing", Stock = 100 } },
            { 4, new Product { Id = 4, Name = "Jeans", Price = 49.99m, Category = "Clothing", Stock = 75 } },
            { 5, new Product { Id = 5, Name = "Book", Price = 12.99m, Category = "Books", Stock = 200 } },
            { 6, new Product { Id = 6, Name = "Headphones", Price = 149.99m, Category = "Electronics", Stock = 30 } },
            { 7, new Product { Id = 7, Name = "Watch", Price = 199.99m, Category = "Accessories", Stock = 20 } }
        };

            // Initialize categories
            _categories = new List<string> { "All", "Electronics", "Clothing", "Books", "Accessories" };

            // Initialize order queue
            _orderQueue = new Queue<Order>();

            // Initialize cart
            _cart = new List<CartItem>();

            // Initialize user history linked list
            _userHistoryHead = null;
        }

        // Get all products from hash table
        public List<Product> GetAllProducts()
        {
            return _productHashTable.Values.ToList();
        }

        // Get filtered products (simulates tree traversal for category filtering)
        public List<Product> GetFilteredProducts(string category, string searchQuery)
        {
            var products = _productHashTable.Values.ToList();

            // Filter by category (tree structure)
            if (category != null && category != "All")
            {
                products = products.Where(p => p.Category == category).ToList();
            }

            // Filter by search query (hash table lookup)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return products;
        }

        // Get categories (tree nodes)
        public List<string> GetCategories()
        {
            return _categories;
        }

        // Add to cart
        public void AddToCart(int productId)
        {
            if (_productHashTable.TryGetValue(productId, out Product product))
            {
                var existingItem = _cart.FirstOrDefault(c => c.Product.Id == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItem
                    {
                        Id = _cart.Count + 1,
                        Product = product,
                        Quantity = 1
                    });
                }

                // Add to user history linked list
                AddUserActivity($"Added to cart", product.Name);
            }
        }

        // Get cart items
        public List<CartItem> GetCart()
        {
            return _cart;
        }

        // Place order (enqueue)
        public void PlaceOrder()
        {
            if (_cart.Count == 0)
                return;

            var order = new Order
            {
                Id = _orderQueue.Count + 1,
                Items = _cart.ToList(),
                Total = _cart.Sum(c => c.Product.Price * c.Quantity),
                Status = "Pending",
                Timestamp = DateTime.Now
            };

            _orderQueue.Enqueue(order);
            _cart.Clear();

            // Add to user history linked list
            AddUserActivity($"Placed order", $"Order #{order.Id}");
        }

        // Process next order (dequeue)
        public void ProcessNextOrder()
        {
            if (_orderQueue.Count == 0)
                return;

            var order = _orderQueue.Peek(); // Just peek, don't dequeue yet
            order.Status = "Processing";

            // In a real app, we'd process asynchronously, but for demo purposes:
            Task.Delay(2000).ContinueWith(_ =>
            {
                order.Status = "Completed";
            });
        }

        // Get all orders
        public List<Order> GetOrders()
        {
            return _orderQueue.ToList();
        }

        // Add user activity to linked list
        private void AddUserActivity(string action, string productName)
        {
            var newActivity = new UserActivity
            {
                Id = (_userHistoryHead?.Id ?? 0) + 1,
                Action = action,
                ProductName = productName,
                Timestamp = DateTime.Now,
                Next = null
            };

            if (_userHistoryHead == null)
            {
                _userHistoryHead = newActivity;
            }
            else
            {
                // Find the last node in the linked list
                var current = _userHistoryHead;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                // Add the new activity at the end
                current.Next = newActivity;
            }
        }

        // Get user activity history
        public List<UserActivity> GetUserHistory()
        {
            var history = new List<UserActivity>();
            var current = _userHistoryHead;

            while (current != null)
            {
                history.Add(current);
                current = current.Next;
            }

            return history;
        }
    }
}
