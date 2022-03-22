using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Character;
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
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await characterService.Get());
        }


        [HttpGet("id")] 
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetId(int id)
        {
            return Ok(await characterService.GetId(id));
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Create(AddCharacterDto newCharacter)
        {
            return Ok(await characterService.Create(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Update(UpdateCharacterDto updateCharacterDto)
        {
            var response=await characterService.Update(updateCharacterDto);
            if (response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


          [HttpDelete("id")]
           public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
           {
            return Ok(await characterService.Delete(id));
           }

        
    }
}