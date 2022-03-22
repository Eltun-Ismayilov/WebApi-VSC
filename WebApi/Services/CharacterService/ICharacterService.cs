using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<List<Character>> GetAllCharacters();
         Task<Character> GetCharacterById(int id);
         Task<List<Character>> AddCharacter(Character newCharacter);
    }
}