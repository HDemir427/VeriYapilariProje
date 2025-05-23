namespace OrderManagementSystem.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }        
        public string FirstName { get; set; }     
        public string LastName { get; set; }     
        public string Email { get; set; }        
        public DateTime CreatedAt { get; set; }  
        public DateTime? UpdatedAt { get; set; }  
    }
}