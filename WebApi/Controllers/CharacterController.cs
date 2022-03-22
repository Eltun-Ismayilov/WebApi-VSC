using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<List<Character>> Get()
        {
            return Ok(characterService.GetAllCharacters());
        }


        [HttpGet("id")] 
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characterService.GetCharacterById(id));
        }


        [HttpPost]
        public ActionResult<List<Character>> GetAddCharacter(Character newCharacter)
        {
            return Ok(characterService.AddCharacter(newCharacter));
        }
    }
}