using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.CharacterService;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController:ControllerBase
    {
        
        private readonly ICharacterService characterService;
        public CharacterController(ICharacterService characterService)
        {
            this.characterService=characterService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Character>>> Get()
        {
            return Ok(await characterService.GetAllCharacters());
        }


        [HttpGet("id")] 
        public async Task<ActionResult<Character>> GetSingle(int id)
        {
            return Ok(await characterService.GetCharacterById(id));
        }


        [HttpPost]
        public async Task<ActionResult<List<Character>>> GetAddCharacter(Character newCharacter)
        {
            return Ok(await characterService.AddCharacter(newCharacter));
        }
    }
}