using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Data.Entity
{
    public class Order
    {
      
        public int OrderId { get; set; }

       
        public int  UserId { get; set; }

      
        public string UserName { get; set; }
        
     
        public decimal TotalAmount { get; set; }

       
        public string Status { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered, Cancelled

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        public User User { get; internal set; }

        public OrderQueue OrderQueue { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }

}
