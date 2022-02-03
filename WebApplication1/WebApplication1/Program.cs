using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*GET api/v1/persons
Возвращает массив объектов типа Person:
[Person, Person, …]

GET api/v1/person/[id]
Где id – уникальный идентификатор сотрудника.
Возвращает объект типа Person.

POST api/v1/person
Где id – уникальный идентификатор сотрудника.
В теле запроса передавать объект Person. Id должен быть null или undefined.
Создаёт нового сотрудника в системе с указанными навыками.

PUT api/v1/person/[id]
Где id – уникальный идентификатор сотрудника.
В теле запроса передавать объект Person. Id должен быть null или undefined.
Обновляет данные сотрудника согласно значениям, указанным в объекте Person в теле. Обновляет навыки сотрудника согласно указанному набору.

DELETE api/v1/person/[id]
Где id – уникальный идентификатор сотрудника.
Удаляет с указанным id сотрудника из системы.

*/

var persons = new List<Person>();
persons.Add(new Person() {Name = "Sergey", DisplayName = "Sergey", Id = 1});
persons[0].Skills.Add(new Skill(){Id = 10,Level = 10,Name = "test"});

app.MapGet("/api/v1/persons", () => persons);
app.MapGet("/api/v1/person/{id}", (int id) => persons.FirstOrDefault(p => p.Id == id));
app.MapPost("/api/v1/person", (Person person) => persons.Add(person));
app.MapPut("/api/v1/person/{id}", (Person person) =>
{
    var index = persons.FindIndex(p => p.Id == person.Id);
    if (index < 0)
    {
        throw new Exception("Person not found");
    }

    persons[index] = person;
});
app.MapDelete("/api/v1/person/{id}", (int id) =>
{
    var index = persons.FindIndex(p => p.Id == id);
    if (index < 0)
    {
        throw new Exception("Person not found");
    }

    persons.RemoveAt(index);
});

app.Run();

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();
}

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
}