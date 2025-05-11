using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Dto;
namespace OrderManagementSystem.Services
{
    public interface IUserService
    {
        User Register(UserRegisterDto userDto);
        User Login(UserLoginDto userDto);
        User GetUserById(int userId);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
