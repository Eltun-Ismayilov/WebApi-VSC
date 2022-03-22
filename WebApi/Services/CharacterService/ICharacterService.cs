using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<GetCharacterDto>>> Get();
         Task<ServiceResponse<GetCharacterDto>> GetId(int id);
         Task<ServiceResponse<List<GetCharacterDto>>> Create(AddCharacterDto newCharacter);
         Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter);
         Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id);
    }
}