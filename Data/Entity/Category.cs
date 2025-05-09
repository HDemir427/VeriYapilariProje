using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    
    public class Category :  IComparable<Category>
    {
        public required string Name { get; set; }
        public int  CompareTo(Category other)
        {
            return string.Compare(Name, other?.Name, StringComparison.Ordinal);
        }
        public int CategoryId { get; set; }

        


        public string Description { get; set; }


        public string Title { get; set; }

        public  ICollection<Product> Products
        {
            get; set;

        }
       

        
    }
}
