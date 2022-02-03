using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonRepo _repository;
    private readonly IMapper _mapper;

    public PersonsController(
        IPersonRepo repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<PersonReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Persons....");

        var personItem = _repository.GetPersons();

        return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(personItem));
    }
}