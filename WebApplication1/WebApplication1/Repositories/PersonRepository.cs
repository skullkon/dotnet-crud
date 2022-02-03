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
        var person = _context.Persons.Include(s => s.Skills).FirstOrDefault(p => p.Id == id);
        Console.WriteLine(person);
        return person;
    }

    public void CreatePerson(Person person)
    {
        _context.Persons.Add(person);
    }

    public void EditPerson(int id, Person person)
    {
        throw new NotImplementedException();
    }

    public void DeletePerson(int id)
    {
        // var person = _context.Persons.Include(s => s.Skills).FirstOrDefault(p => p.Id == id);
        // return _context.Persons.Include(s => s.Skills).ToList()[id];
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}