
using System.ComponentModel.DataAnnotations;


namespace OrderManagementSystem.Dto
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}