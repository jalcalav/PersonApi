using Microsoft.EntityFrameworkCore;
using PersonApi.Domain;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;

namespace PersonApi.Infraestructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByIdentification(string identification, CancellationToken cancellationToken)
    {
        return await _context.Person.AnyAsync(x => x.Identification == identification, cancellationToken);
    }

    public async Task<Person?> FindByIdentification(string identification, CancellationToken cancellationToken)
    {
        return await _context.Person.Where(x => x.Identification == identification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<PersonDTO>> ListAllAsync(CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        return await _context.Person.AsNoTracking()
            .OrderBy(x => x.FullName)
            .Select(x => new PersonDTO
            {
                Id = x.Id,
                Identification = x.Identification,
                FullName = x.FullName,
                BirthDate = x.BirthDate,
                Age = today.Year - x.BirthDate.Year - (today.DayOfYear < x.BirthDate.DayOfYear ? 1 : 0),
            }).ToListAsync(cancellationToken);
    }
}