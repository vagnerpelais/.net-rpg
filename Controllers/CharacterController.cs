using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_rpg.Services.CharacterService;

namespace net_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("GetSingle/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id) {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost("AddCharacter")]
        public async  Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter) {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}