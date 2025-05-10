using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Utilities;

namespace OrderManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CustomAvlTree<Category> _categories;

        public CategoryService(CustomAvlTree<Category> categories)
        {
            _categories = categories;
        }

        public void AddCategory(Category category)
        {
            _categories.Insert(category);
        }

        public List<Category> GetAllCategories()
        {
            return _categories.GetAll().ToList(); 
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var category = _categories.GetAll().FirstOrDefault(c => c.CategoryId == categoryId);
            return category?.Products ?? new List<Product>();
        }
    }
}