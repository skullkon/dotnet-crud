using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Profile;

public class SkillProfile : AutoMapper.Profile
{
    public SkillProfile()
    {
        CreateMap<Person, PersonReadDto>();
    }
    
}