using PersonService.Dto;
using PersonService.Models;

namespace PersonService.Profile;

public class SkillProfile : AutoMapper.Profile
{
    public SkillProfile()
    {
        CreateMap<Person, PersonReadDto>();
        CreateMap<PersonCreateDto, Person>();
    }
    
}