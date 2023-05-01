using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlinePasswordManager.Server.Authorization;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Exceptions;
using OnlinePasswordManager.Server.Services.UserContextService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly OnlinePasswordManagerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CategoryService(OnlinePasswordManagerDbContext dbContext, IMapper mapper,
            IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var categories = await _dbContext.Categories
                .Where(c => c.UserId == _userContextService.GetUserId && c.UserId == null)
                .ToListAsync();

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);

            return categoriesDTO;
        }

        public async Task Create(CategoryCreateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);

            category.UserId = _userContextService.GetUserId;

            await _dbContext.Categories.AddAsync(category);

            await _dbContext.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                throw new NotFoundException("Category not found.");
            
            await Access(category);
            
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
        }

        private async Task Access(Category category)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, category, new ResourcePasswordRequirement());

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("No access.");
            }

        }
    }
}