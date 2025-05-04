using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    public class OrderQueue
    {
    
        public int QueueId { get; set; }

      
        public int OrderId { get; set; }

        
        public DateTime QueueDate { get; set; } = DateTime.UtcNow;

    
        public string ProcessingStatus { get; set; } = "Queued"; // Queued, Processing, Completed

        public Order Order { get; set; }

    }
}
