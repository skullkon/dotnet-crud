using PersonService.Models;

namespace PersonService.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPersonsAsync();
    Task<Person> GetPersonByIdAsync(int id);
    Task CreatePerson(Person person);
    Task EditPerson(int id,Person person);
    Task DeletePerson(int id);
    bool Save();
}