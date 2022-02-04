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
    
    /// <summary>
    /// Gets the list of persons
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/v1/person
    /// </remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    public ActionResult<IEnumerable<PersonReadDto>> GetPersons()
    {
        Console.WriteLine("--> Getting Persons....");

        var personItem = _repository.GetPersons();

        return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(personItem));
    }
    
    [HttpGet("{id}", Name = "GetPlatformById")]
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
    /// <summary>
    /// Creates a TodoItem.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/person
    ///      { 
    ///         "name": "Masha",
    ///         "displayName": "MashaEditted",
    ///         "skills": [
    ///         {
    ///             "name": "test1",
    ///             "level": 10
    ///         }
    ///     ]
    /// }
    ///
    /// </remarks>
    /// <param name="item"></param>
    /// <returns>A newly created TodoItem</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>            
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <summary>
    /// Deletes a specific Person.
    /// </summary>
    /// <param name="id"></param>   
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