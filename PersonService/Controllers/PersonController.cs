using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonService.Dto;
using PersonService.Helpers;
using PersonService.Models;
using PersonService.Repositories;

namespace PersonService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonController> _logger;

    public PersonController(
        IPersonRepository repository,
        IMapper mapper,
        ILogger<PersonController> logger)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Gets the list of persons
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// GET /api/v1/person
    /// </remarks>
    /// <returns>Returns Persons list</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PersonReadDto>> GetPersons()
    {
        Console.WriteLine("--> Getting Persons....");

        var personItem = _repository.GetPersons();

        return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(personItem));
    }
    
    
    /// <summary>
    /// Gets the person by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    /// GET /api/v1/person/1
    /// </remarks>
    /// <param name="id">Person id (int)</param>
    /// <returns>Returns Person</returns>
    /// <response code="200">Success</response>
    /// <response code="404">if the person was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PersonReadDto> GetPersonById(string id)
    {
        
        Console.WriteLine("--> Getting Person by id....");
        if (!ParseHelper.IsNumber(id))
        {
            _logger.LogError("Id is incorrect");
            return StatusCode(400);
        }
        var intId = Int32.Parse(id);
        var personItem = _repository.GetPersonById(intId);

        if (personItem != null)
        {
            return Ok(_mapper.Map<PersonReadDto>(personItem));
        }
        _logger.LogError("Persons not found");
        return NotFound("Persons not found");
    }
    
    /// <summary>
    /// Creates the person with skills
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/v1/person
    /// {
    ///     "name": "Sasha",
    ///     "displayName": "SashaEdited",
    ///     "skills": [
    ///         {
    ///                "name": "test1",
    ///                "level": 10
    ///         }
    ///     ]
    /// }
    /// </remarks>
    /// <param name="personCreateDto">CreatePersonDto object</param>
    /// <returns>Returns id (int)</returns>
    /// <response code="201">Success</response>
    /// <response code="400">If json format is not suitable</response>
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

    /// <summary>
    /// Updates the person
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /api/v1/person/1
    /// {
    ///     "name": "Sasha",
    ///     "displayName": "SashaUpdated",
    ///     "skills": [
    ///         {
    ///                "name": "testUpdated",
    ///                "level": 10
    ///         }
    ///     ]
    /// }
    /// </remarks>
    /// <param name="id">id of user</param>
    /// <param name="personCreateDto">PersonCreateDto object</param>
    /// <returns>Returns Edited Person</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the person does not exist</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PersonCreateDto> EditPerson(string id,PersonCreateDto personCreateDto)
    {
        if (!ParseHelper.IsNumber(id))
        {
            _logger.LogError("Id is incorrect");
            return StatusCode(400);
        }

        var intId = Int32.Parse(id);
        Console.WriteLine("--> Edit Person....");
        var personModel = _mapper.Map<Person>(personCreateDto);
        try
        {
            _repository.EditPerson(intId, personModel);
            return Ok(personCreateDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Person not found");
            return NotFound();
        }
    }
    
    /// <summary>
    /// Deletes the person by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /api/v1/person/1
    /// </remarks>
    /// <param name="id">Id of the person (int)</param>
    /// <returns>Returns Ok(Deleted successfully)</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the person does not exist</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PersonCreateDto> DeletePerson(string id)
    {
        if (!ParseHelper.IsNumber(id))
        {
            _logger.LogError("Id is incorrect");
            return StatusCode(400);
        }

        var intId = Int32.Parse(id);
        Console.WriteLine("--> Creating Persons....");
        try
        {
            _repository.DeletePerson(intId);
            return Ok("Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Persons not found");
            return NotFound();
        }
    }
}