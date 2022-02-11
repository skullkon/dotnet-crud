using Microsoft.EntityFrameworkCore;
using PersonService.Models;

namespace PersonService.Helpers;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Skill> Skills => Set<Skill>();
}