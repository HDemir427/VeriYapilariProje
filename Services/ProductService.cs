using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Utilities;

namespace OrderManagementSystem.Services;
public class ProductService : IProductService
{
    private readonly CustomHashTable<int, Product> _products; 
    private readonly CustomAvlTree<Category> _categories;
    private readonly CustomQueue<Order> _orders;
    private readonly object _syncLock = new object();

    public ProductService(
        CustomHashTable<int, Product> products,
        CustomAvlTree<Category> categories,
        CustomQueue<Order> orders)
    {
        _products = products;
        _categories = categories;
        _orders = orders;
    }

    public void AddProduct(Product product)
    {
        lock (_syncLock)
        {
            if (product.Category == null)
                throw new ArgumentNullException(nameof(product.Category));

            _products.Add(product.ProductId, product);

            var existingCategory = _categories.Find(product.Category);
            if (existingCategory != null)
                existingCategory.Products.Add(product);
            else
            {
                product.Category.Products.Add(product);
                _categories.Insert(product.Category);
            }
        }
    }

    public Product GetProduct(int id) => _products.Get(id); 

    public IEnumerable<Product> Search(string keyword) =>
        _products.GetAll().Where(p => p.Name.Contains(keyword));

    public List<Product> GetByCategory(string category)
    {
        var categoryNode = _categories.Find(new Category { Name = category });
        return categoryNode?.Products ?? new List<Product>();
    }

    public void ProcessOrder(Order order) => _orders.Enqueue(order);
}
