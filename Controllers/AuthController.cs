using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_rpg.Services.UserService;
using net_rpg.utils;

namespace net_rpg.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        public async Task<ActionResult<ServiceResponse<User>>> Register(RequestUserDto newUser)
        {

            var response = await _userService.AddUser(newUser);
            if(response.Data is null)
            {
                return Conflict(response);
            }

            return Created("", response);
        }
    }
}