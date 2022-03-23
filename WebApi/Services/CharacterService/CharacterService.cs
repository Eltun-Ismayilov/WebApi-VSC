using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext dataContext;
        public CharacterService(IMapper mapper,DataContext dataContext)
        {
            this.mapper = mapper;
            this.dataContext = dataContext;

        }

       
        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character=mapper.Map<Character>(newCharacter);
            var data=dataContext.characters.Add(character);
            await  dataContext.SaveChangesAsync();
            serviceResponse.Data = await dataContext.characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Get()
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var data=await dataContext.characters.ToListAsync();
            serviceResponse.Data =data.Select(c=>mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> GetId(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var data=await dataContext.characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDto>(data);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                 var data=await dataContext.characters.FirstOrDefaultAsync(c=>c.Id==updateCharacter.Id);
                 data.Name=updateCharacter.Name;
                 data.Defense=updateCharacter.Defense;
                 data.HitPoints=updateCharacter.HitPoints;
                 data.Strength=updateCharacter.Strength;
                 data.Intelligence=updateCharacter.Intelligence;
                 data.Class=updateCharacter.Class;

                 serviceResponse.Data =mapper.Map<GetCharacterDto>(data);
                 await dataContext.SaveChangesAsync();
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

            var data=await dataContext.characters.FirstOrDefaultAsync(c => c.Id == id);
            dataContext.characters.Remove(data);
            await dataContext.SaveChangesAsync();

            serviceResponse.Data = await dataContext.characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }
    }
}