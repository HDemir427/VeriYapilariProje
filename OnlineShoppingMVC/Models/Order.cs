namespace OnlineShoppingMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } // "Pending", "Processing", "Completed"
        public DateTime Timestamp { get; set; }
    }
}
