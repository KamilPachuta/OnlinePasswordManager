using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlinePasswordManager.Server.Authorization;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Exceptions;
using OnlinePasswordManager.Server.Services.UserContextService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly OnlinePasswordManagerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public NoteService(OnlinePasswordManagerDbContext dbContext, IMapper mapper,
            IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<NoteDTO>> GetAll()
        {
            var notes = await _dbContext.Notes
                .Where(n => n.UserId == _userContextService.GetUserId)
                .ToListAsync();

            var notesDTO = _mapper.Map<List<NoteDTO>>(notes);

            return notesDTO;
        }

        public async Task<NoteDTO> Get(int id)
        {
            var note = await _dbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note is null)
                throw new NotFoundException("Note not found.");

            await Access(note);

            var noteDTO = _mapper.Map<NoteDTO>(note);

            return noteDTO;
        }

        public async Task<int> Create(NoteCreateDTO dto)
        {
            var note = _mapper.Map<Note>(dto);

            note.UserId = _userContextService.GetUserId;
            note.CreatedAt = DateTime.Now;

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.Id;
        }

        public async Task Update(int id, NoteCreateDTO dto)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note is null)
                throw new NotFoundException("Note not found.");

            await Access(note);

            note.Title = dto.Title;
            note.Content = dto.Content;
            note.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note is null)
                throw new NotFoundException("Note not found.");

            await Access(note);

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        private async Task Access(Note note)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, note, new ResourceNoteRequirement());

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("No access.");
            }

        }
    }
}