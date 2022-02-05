using PersonService.Models;

namespace PersonService.Repositories;

public interface IPersonRepository
{
    IEnumerable<Person> GetPersons();
    Person GetPersonById(int id);
    void CreatePerson(Person person);
    void EditPerson(int id,Person person);
    void DeletePerson(int id);
    bool Save();
}