using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.UserService
{
    public interface IUserService
    {
        Task ChangeUserPassword();
        Task<string> GenerateJWT(UserLoginDto dto);
        Task RegisterUser(UserRegisterDto dto);
    }
}