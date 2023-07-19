using Microsoft.EntityFrameworkCore;
using PersonApi.Domain;

namespace PersonApi.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>()
          .HasKey(person => new { person.Id });
    }
}