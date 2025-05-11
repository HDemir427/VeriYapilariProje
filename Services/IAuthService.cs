using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Dto;

namespace OrderManagementSystem.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        User Authenticate(UserLoginDto userDto);
    }
}
