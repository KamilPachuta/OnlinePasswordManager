using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePasswordManager.Server.Services.PasswordService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordDTO>>> GetAll()
        {
            var passwords = await _passwordService.GetAll();

            return Ok(passwords);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordDTO>>> GetAllFromCategory([FromQuery]int id)
        {
            var passwords = await _passwordService.GetAll();

            return Ok(passwords);
        }

        [HttpGet("{id}")] // tautaj musi byc autoryzajca 
        public async Task<ActionResult<PasswordDetailsDTO>> GetDetails([FromRoute]int id)
        {
            var password = await _passwordService.GetDetails(id);

            return Ok(password);
        }

        [HttpPut]
        public async Task<ActionResult> Create([FromBody]PasswordCreateDTO dto)
        {
            await _passwordService.CreatePassword(dto);

             return Ok();
        }

        [HttpPut("/update/details/{id}")]
        public async Task<ActionResult> UpdateDetails([FromRoute]int id, [FromBody]PasswordUpdateDetailsDTO dto)
        {
            await _passwordService.UpdateDetails(id, dto);

            return Ok();
        }

        [HttpPut("/update/password/{id}")]
        public async Task<ActionResult> UpdatePassword([FromRoute] int id, [FromBody]string encryptedPassword)
        {
            await _passwordService.UpdatePassword(id, encryptedPassword);

            return Ok();
        }


        [HttpPut("/add/category/{id}")]
        public async Task<ActionResult> AddCategory([FromRoute] int id, [FromQuery]int categoryId )
        {
            await _passwordService.AddCategory(id, categoryId);

            return Ok();
        }

        [HttpPut("/add/note/{id}")]
        public async Task<ActionResult> AddNote([FromRoute] int id, [FromBody]string text)
        {
            await _passwordService.AddQuickNote(id, text);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _passwordService.DeletePassword(id);

            return Ok();
        }

    }
}
