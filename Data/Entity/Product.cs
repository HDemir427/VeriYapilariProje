using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public string Description { get; set; }

        
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int MyProperty { get; set; }

        public Category Category { get; set; }

    }
}
