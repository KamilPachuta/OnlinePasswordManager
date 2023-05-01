using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task Create(CategoryCreateDTO dto);
        Task Delete(int id);
        Task<List<CategoryDTO>> GetAll();
    }
}