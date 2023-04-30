using Microsoft.AspNetCore.Identity;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly OnlinePasswordManagerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(OnlinePasswordManagerDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                CreatedAt = dto.CreatedAt,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;

            await _dbContext.users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LoginUser()
        {

        }

        public async Task ChangeUserPassword()
        {

        }

    }
}
