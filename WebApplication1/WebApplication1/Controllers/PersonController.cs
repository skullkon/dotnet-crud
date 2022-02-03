using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepo _repository;
    private readonly IMapper _mapper;

    public PersonController(
        IPersonRepo repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<PersonReadDto>> GetPersons()
    {
        Console.WriteLine("--> Getting Persons....");

        var personItem = _repository.GetPersons();

        return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(personItem));
    }
    
    [HttpGet("{id}")]
    public ActionResult<PersonReadDto> GetPersonById(string id)
    {
        Console.WriteLine("--> Getting Persons....");

        var personItem = _repository.GetPersonById(Int32.Parse(id));

        return Ok(_mapper.Map<PersonReadDto>(personItem));
    }
    
    [HttpPost]
    public ActionResult<PersonCreateDto> CreatePerson(PersonCreateDto personCreateDto)
    {
        Console.WriteLine("--> Creating Persons....");
        var personModel = _mapper.Map<Person>(personCreateDto);
        _repository.CreatePerson(personModel);
        _repository.Save();
        return personCreateDto;
    }
}