using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PersonDb>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
builder.Services.AddScoped<IPersonRepo, PersonRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PersonDb>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); 
});
app.UseCors("AllowAll");
app.MapControllers();

app.Run();

namespace WebApplication1
{
    public class PersonDb : DbContext
    {
        public PersonDb(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Skill> Skills => Set<Skill>();
    
    }
}