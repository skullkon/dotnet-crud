using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class PersonRepo : IPersonRepo
{
    private readonly PersonDb _context;

    public PersonRepo(PersonDb context)
    {
        _context = context;
    }


    public IEnumerable<Person> GetPersons()
    {
        return _context.Persons.Include(s => s.Skills);
    }

    public Person GetPersonById(int id)
    {
        throw new NotImplementedException();
    }

    public void CreatePerson(Person person)
    {
        throw new NotImplementedException();
    }

    public void EditPerson(int id, Person person)
    {
        throw new NotImplementedException();
    }

    public void DeletePerson(int id)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}