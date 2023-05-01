using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.NoteService
{
    public interface INoteService
    {
        Task<int> Create(NoteCreateDTO dto);
        Task Delete(int id);
        Task<NoteDTO> Get(int id);
        Task<IEnumerable<NoteDTO>> GetAll();
        Task Update(int id, NoteCreateDTO dto);
    }
}