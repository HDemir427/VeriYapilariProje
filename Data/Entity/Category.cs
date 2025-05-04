using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    public class Category
    {

        public int CategoryId { get; set; }

        public string Name { get; set; }


        public string Description { get; set; }


        public string Title { get; set; }

        public  ICollection<Product> Products
        {
            get; set;

        }
    }
}
