using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePasswordManager.Server.Services.UserService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterUser(dto);

            return Ok();
        }
        // register

        // login

        // logout

        // change password

        // change email

        // delete user
    }
}
