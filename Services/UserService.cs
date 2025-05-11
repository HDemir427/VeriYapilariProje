
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Dto;
using OrderManagementSystem.Services;
using OrderManagementSystem.Utilities;
using System;
using System.Linq;

namespace OrderManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly CustomHashTable<int, User> _users;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            CustomHashTable<int, User> users,
            IPasswordHasher passwordHasher)
        {
            _users = users;
            _passwordHasher = passwordHasher;
        }

        public User Register(UserRegisterDto userDto)
        {
            if (_users.GetAll().Any(u => u.Email == userDto.Email))
                throw new ArgumentException("Bu email zaten kayıtlı!");

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = _passwordHasher.Hash(userDto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            _users.Add(user.UserId, user);
            return user;
        }

        public User Login(UserLoginDto userDto)
        {
            var user = _users.GetAll().FirstOrDefault(u => u.Email == userDto.Email);
            if (user == null)
                throw new KeyNotFoundException("Kullanıcı bulunamadı!");

            if (!_passwordHasher.Verify(userDto.Password, user.Password))
                throw new UnauthorizedAccessException("Şifre hatalı!");

            return user;
        }

        public User GetUserById(int userId)
        {
            var user = _users.Get(userId);
            return user ?? throw new KeyNotFoundException("Kullanıcı bulunamadı!");
        }

        public void UpdateUser(User user)
        {
            if (!_users.GetAll().Any(u => u.UserId == user.UserId))
                throw new KeyNotFoundException("Kullanıcı bulunamadı!");

            user.UpdatedAt = DateTime.UtcNow;
            _users.Add(user.UserId, user);
        }

        public void DeleteUser(int userId)
        {
            if (!_users.Remove(userId))
                throw new KeyNotFoundException("Kullanıcı bulunamadı!");
        }
    }
}
