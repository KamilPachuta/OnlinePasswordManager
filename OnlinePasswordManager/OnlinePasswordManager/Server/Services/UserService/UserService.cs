using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Exceptions;
using OnlinePasswordManager.Shared.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlinePasswordManager.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly OnlinePasswordManagerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(OnlinePasswordManagerDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task RegisterUser(UserRegisterDto dto)
        {
            var newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                CreatedAt = dto.CreatedAt,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GenerateJWT(UserLoginDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(issuer: _authenticationSettings.JwtIssuer, 
                audience: _authenticationSettings.JwtIssuer, 
                claims: claims, 
                expires: expires, 
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);


        }

        public async Task ChangeUserPassword()
        {

        }

    }
}
