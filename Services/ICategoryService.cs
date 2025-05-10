using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Services
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        List<Category> GetAllCategories();
        List<Product> GetProductsByCategory(int categoryId);
    }
}