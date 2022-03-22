using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;

        }

        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id=1, Name="Tural"}
        };
        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character=mapper.Map<Character>(newCharacter);
            character.Id=characters.Max(c=>c.Id)+1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Get()
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data =characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> GetId(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                 Character character=characters.FirstOrDefault(c=>c.Id==updateCharacter.Id);
                 character.Name=updateCharacter.Name;
                 character.Defense=updateCharacter.Defense;
                 character.HitPoints=updateCharacter.HitPoints;
                 character.Strength=updateCharacter.Strength;
                 character.Intelligence=updateCharacter.Intelligence;
                 character.Class=updateCharacter.Class;

                 serviceResponse.Data =mapper.Map<GetCharacterDto>(character);
                 return serviceResponse;

            }
            catch (System.Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message=ex.Message;
            }
            return null;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var character=characters.FirstOrDefault(x=>x.Id==id);
            characters.Remove(character);

            serviceResponse.Data =characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
    }
}