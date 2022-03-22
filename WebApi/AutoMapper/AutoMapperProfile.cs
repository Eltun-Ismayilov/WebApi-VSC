using AutoMapper;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            //CreateMap<UpdateCharacterDto,Character>();
        }
    }
}