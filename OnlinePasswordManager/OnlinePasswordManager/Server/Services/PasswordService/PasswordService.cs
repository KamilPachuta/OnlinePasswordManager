using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Exceptions;
using OnlinePasswordManager.Server.Services.UserContextService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        private readonly IMapper _mapper;
        private readonly OnlinePasswordManagerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public PasswordService(OnlinePasswordManagerDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<PasswordDTO>> GetAll()
        {
            var passwords = await _dbContext.Passwords.Where(x => x.UserId == _userContextService.GetUserId).ToListAsync();

            //if(passwords is null)
            //{
            //    throw new NotFoundExce
            //}
            var passwordsDTO = _mapper.Map<List<PasswordDTO>>(passwords);

            return passwordsDTO;


        }

        public async Task<IEnumerable<PasswordDTO>> GetAllFromCategory(int categoryId)
        {
            var passwords = await _dbContext.Passwords
                .Where(x => x.UserId == _userContextService.GetUserId && x.CategoryId == categoryId)
                .ToListAsync();

            //if(passwords is null)
            //{
            //    throw new NotFoundExce
            //}
            var passwordsDTO = _mapper.Map<List<PasswordDTO>>(passwords);

            return passwordsDTO;


        }

        public async Task<PasswordDetailsDTO> GetDetails(int id) // dodać autoryzajce 
        {
            var password = await _dbContext.Passwords.FirstAsync(x => x.Id == id);// Chyba może byc bez default bo tutaj id z listy bedzie wysyłane przez frontend

            var passwordDetailsDTO = _mapper.Map<PasswordDetailsDTO>(password);

            return passwordDetailsDTO;

        }

        public async Task CreatePassword(PasswordCreateDTO dto) // dodać autoryzacje 
        {
            var password = _mapper.Map<Password>(dto);

            password.UserId = _userContextService.GetUserId;

            _dbContext.Passwords.Add(password);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePassword(int id) // dodać autoryzacje // dodac usunięcie PasswordVersion
        {
            //_logger.LogError($"Restaurant with id: {id} DELETE action invoke");

            var password = _dbContext
                .Passwords
                .FirstOrDefault(p => p.Id == id);

            if (password is null)
                throw new NotFoundException("Password not found.");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException();
            //}

            _dbContext.Passwords.Remove(password);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddQuickNote(int id, string text)
        {
            var password = _dbContext
                .Passwords
                .FirstOrDefault(r => r.Id == id);


            if (password is null)
                throw new NotFoundException("Password not found.");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException();
            //}

            password.QuickNote = text;

            await _dbContext.SaveChangesAsync();

        }

        public async Task AddCategory(int id, int categoryId)
        {
            var password = _dbContext
                .Passwords
                .FirstOrDefault(r => r.Id == id);


            if (password is null)
                throw new NotFoundException("Password not found.");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException();
            //}




            password.CategoryId = categoryId;

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdatePassword(int id, PasswordCreateDTO dto)
        {
            var password = _dbContext
                .Passwords
                .FirstOrDefault(r => r.Id == id);


            if (password is null)
                throw new NotFoundException("Password not found.");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException();
            //}

            password.ServiceName = dto.ServiceName;
            password.Login = dto.Login;
            password.EncryptedPassword = dto.EncryptedPassword;

            if (dto.CategoryId is not null)
                password.CategoryId = dto.CategoryId;

            if (dto.QuickNote is not null)
                password.QuickNote = dto.QuickNote;

            if (dto.URL is not null)
                password.URL = dto.URL;

            //Dodać tworzenie encji PasswordVersion

            await _dbContext.SaveChangesAsync();

        }



    }
}
