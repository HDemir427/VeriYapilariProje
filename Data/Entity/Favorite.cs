using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{

    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedDate { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
