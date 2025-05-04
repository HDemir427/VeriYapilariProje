using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Data.Entity
{
    public class User
    {
        
        public int UserId { get; set; }
        
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<UserHistory>  UserHistories { get; set; }
        
        public enum GenderType
        {
            Unknown,
            Male,
            Female,
            Other
        }



    }
}
