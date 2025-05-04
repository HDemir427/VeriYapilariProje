using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    public class UserHistory
    {
      
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }
      
        public string ActionType { get; set; } // Viewed, Purchased, Searched

        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        public string Details { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
