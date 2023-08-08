using Microsoft.AspNetCore.Mvc;
using net_rpg.Services.UserService;

namespace net_rpg.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<User>>> Register(RequestUserDto newUser)
        {

            var response = await _userService.AddUser(newUser);
            if(response.Data is null)
            {
                return Conflict(response);
            }

            return Created("", response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<User>>> Login(RequestUserDto request)
        {
            var response = await _userService.VerifyUserLogin(request);
            if(response.Data is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}