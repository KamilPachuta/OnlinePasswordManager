using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.UserService
{
    public interface IUserService
    {
        Task ChangeUserPassword();
        Task LoginUser();
        Task RegisterUser(RegisterUserDto dto);
    }
}