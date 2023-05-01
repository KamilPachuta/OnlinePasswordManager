using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<int> Create(CategoryCreateDTO dto);
        Task Delete(int id);
        Task<List<CategoryDTO>> GetAll();
    }
}