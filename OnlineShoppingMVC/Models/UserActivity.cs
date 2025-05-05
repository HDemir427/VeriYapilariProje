namespace OnlineShoppingMVC.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string ProductName { get; set; }
        public DateTime Timestamp { get; set; }
        public UserActivity Next { get; set; } // Link to the next activity (for linked list)
    }
}
