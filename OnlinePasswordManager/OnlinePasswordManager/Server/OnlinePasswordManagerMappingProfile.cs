using AutoMapper;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server
{
    public class OnlinePasswordManagerMappingProfile : Profile
    {
        public OnlinePasswordManagerMappingProfile()
        {
            CreateMap<Password, PasswordDTO>();

            CreateMap<Password, PasswordDetailsDTO>();

            CreateMap<PasswordCreateDTO, Password>();


            CreateMap<Note, NoteDTO>();

            CreateMap<NoteCreateDTO, Note>();


            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryCreateDTO, Category>();

        }
    }
}
