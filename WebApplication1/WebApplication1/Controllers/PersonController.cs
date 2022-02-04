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
        
        Console.WriteLine("--> Getting Person by id....");

        var personItem = _repository.GetPersonById(Int32.Parse(id));

        if (personItem != null)
        {
            return Ok(_mapper.Map<PersonReadDto>(personItem));
        }

        return NotFound("Persons not found");
    }
    
    [HttpPost]
    public ActionResult<PersonCreateDto> CreatePerson(PersonCreateDto personCreateDto)
    {
        Console.WriteLine("--> Creating Person....");
        var personModel = _mapper.Map<Person>(personCreateDto);
        _repository.CreatePerson(personModel);
        _repository.Save();
        return Ok(personCreateDto);
    }
    
    [HttpPut("{id}")]
    public ActionResult<PersonCreateDto> EditPerson(int id,PersonCreateDto personCreateDto)
    {
        Console.WriteLine("--> Edit Person....");
        var personModel = _mapper.Map<Person>(personCreateDto);

        try
        {
            _repository.EditPerson(id, personModel);
            return Ok(personCreateDto);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult<PersonCreateDto> DeletePerson(int id)
    {
        Console.WriteLine("--> Creating Persons....");
        try
        {
            _repository.DeletePerson(id);
            return Ok("Deleted successfully");
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
}