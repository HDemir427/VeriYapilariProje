namespace OnlineShoppingMVC.Models
{
    public class ECommerceViewModel
    {
        public List<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public List<CartItem> Cart { get; set; }
        public List<Order> Orders { get; set; }
        public UserActivity UserHistoryHead { get; set; } // Head of the linked list
        public string SelectedCategory { get; set; }
        public string SearchQuery { get; set; }
        public string ActiveTab { get; set; }
    }
}
