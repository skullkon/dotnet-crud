using Microsoft.EntityFrameworkCore;
using PersonService.Helpers;
using PersonService.Models;

namespace PersonService.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DataContext _context;

    public PersonRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Person>> GetPersonsAsync()
    {
        return await Task.FromResult(_context.Persons.Include(s => s.Skills));
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        var person = await _context.Persons.Include(s => s.Skills).FirstOrDefaultAsync(p => p.Id == id);
        return person;
    }

    public async Task CreatePerson(Person person)
    {
        await _context.Persons.AddAsync(person);
    }

    public async Task EditPerson(int id, Person person)
    {
        var personForEdit =  await GetPersonByIdAsync(id);
        if (person == null)
        {
            throw new Exception("Person with this id was not found");
        }
        personForEdit.Name = person.Name;
        personForEdit.DisplayName = person.DisplayName;
        personForEdit.Skills = person.Skills;
        await _context.SaveChangesAsync();
    }

    public async Task DeletePerson(int id)
    {
        var person = await _context.Persons.Include(s => s.Skills).FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
        {
            throw new Exception("Person with this id was not found");
        }
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}