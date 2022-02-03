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

app.Run();

public class PersonDb : DbContext
{
    public PersonDb(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Skill> Skills => Set<Skill>();
    
}