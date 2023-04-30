using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.UserService
{
    public interface IUserService
    {
        Task ChangeUserPassword();
        Task<string> GenerateJWT(LoginUserDto dto);
        Task RegisterUser(RegisterUserDto dto);
    }
}