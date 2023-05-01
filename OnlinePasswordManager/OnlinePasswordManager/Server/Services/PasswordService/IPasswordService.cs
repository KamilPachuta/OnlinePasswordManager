using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.PasswordService
{
    public interface IPasswordService
    {
        Task AddCategory(int id, int categoryId);
        Task AddQuickNote(int id, string text);
        Task CreatePassword(PasswordCreateDTO dto);
        Task DeletePassword(int id);
        Task<IEnumerable<PasswordDTO>> GetAll();
        Task<IEnumerable<PasswordDTO>> GetAllFromCategory(int categoryId);
        Task<PasswordDetailsDTO> GetDetails(int id);
        Task UpdatePassword(int id, string encryptedPassword);
        Task UpdateDetails(int id, PasswordUpdateDetailsDTO dto);

    }
}