using OrderManagementSystem.Data.Entity;
using System.Collections.Generic;
namespace OrderManagementSystem.Services;

public interface IProductService
{
    void AddProduct(Product product);
    Product GetProduct(string id);
    IEnumerable<Product> Search(string keyword);
    List<Product> GetByCategory(string category);
    void ProcessOrder(Order order);
}
