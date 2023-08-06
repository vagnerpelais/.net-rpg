using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace net_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character {Id = 1, Name = "Frodo"}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get() {
            return Ok(characters);
        }

        [HttpGet("GetSingle/{id}")]
        public ActionResult<Character> GetSingle(int id) {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost("AddCharacter")]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter) {
            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}