using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Utilities;

namespace OrderManagementSystem.Services;

public class ProductService : IProductService
{
    private readonly CustomHashTable<string, Product> _products;
    private readonly CustomAvlTree<Category> _categories;
    private readonly CustomQueue<Order> _orders;

    public ProductService(
        CustomHashTable<string, Product> products,
        CustomAvlTree<Category> categories,
        CustomQueue<Order> orders)
    {
        _products = products;
        _categories = categories;
        _orders = orders;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product.ProductId, product);
        var category = new Category { Name = product.Category };
        var existingCategory = _categories.Find(category);
        if (existingCategory != null)
            existingCategory.Products.Add(product);
        else
        {
            category.Products.Add(product);
            _categories.Insert(category);
        }
    }

    public Product GetProduct(string id) => _products.Get(id);

    public IEnumerable<Product> Search(string keyword) =>
        _products.GetAll().Where(p => p.Name.Contains(keyword));

    public List<Product> GetByCategory(string category) =>
        _categories.SearchCategory(category);

    public void ProcessOrder(Order order) => _orders.Enqueue(order);
}