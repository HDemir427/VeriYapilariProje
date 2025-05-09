using System.ComponentModel.DataAnnotations;
namespace OrderManagementSystem.Dto
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }

}