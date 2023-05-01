using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePasswordManager.Server.Data.Entities;
using OnlinePasswordManager.Server.Services.NoteService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetAll()
        {
            var notes = await _noteService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get([FromRoute]int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult<int>> Create([FromBody]NoteCreateDTO dto)
        {
            throw new NotImplementedException();
            int id;
            return Created($"/api/note/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            throw new NotImplementedException();
        }
    }
}
