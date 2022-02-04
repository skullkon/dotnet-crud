using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPersonRepo
{
    IEnumerable<Person> GetPersons();
    Person GetPersonById(int id);
    void CreatePerson(Person person);
    void EditPerson(int id,Person person);
    void DeletePerson(int id);
    bool Save();
}