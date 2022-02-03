using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PersonDb>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
builder.Services.AddScoped<IPersonRepo, PersonRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PersonDb>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

//
// // Загрузить всех покупателей и связанные с ними заказы
// List<Customer> customers = context.Customers
//     .Include(c => c.Orders)
//     .ToList();      // +1 запрос к базе
//
// // ... какой-то код работы с данными покупателей
//
// // Получить все их заказы
// List<Order> orders = customers.SelectMany(c => c.Orders)
//     // Запрос к базе данных не выполняется,
//     // т.к. данные уже были извлечены 
//     // ранее с помощью прямой загрузки
//     .ToList();

// app.MapGet("/api/v1/persons", (PersonDb db) => db.Persons.Include(s => s.Skills));
app.MapGet("/api/v1/person", (PersonDb db) =>
{
    Person sergey = new Person() { Name = "Sergey", DisplayName = "Test"};
    db.Persons.AddRange(sergey);
 
    Skill fight = new Skill() { Name = "Fight",Person = sergey};

    db.Skills.AddRange(fight);
    db.SaveChanges();
});
// app.MapPost("/api/v1/person", (Person person) => persons.Add(person));
// app.MapPut("/api/v1/person/{id}", (Person person) =>
// {
//     var index = persons.FindIndex(p => p.Id == person.Id);
//     if (index < 0)
//     {
//         throw new Exception("Person not found");
//     }
//
//     persons[index] = person;
// });
// app.MapDelete("/api/v1/person/{id}", (int id) =>
// {
//     var index = persons.FindIndex(p => p.Id == id);
//     if (index < 0)
//     {
//         throw new Exception("Person not found");
//     }
//
//     persons.RemoveAt(index);
// });

app.Run();

public class PersonDb : DbContext
{
    public PersonDb(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Skill> Skills => Set<Skill>();
    
}