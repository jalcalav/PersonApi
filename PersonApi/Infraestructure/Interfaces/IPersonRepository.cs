using PersonApi.Domain;
using PersonApi.Models;

namespace PersonApi.Infraestructure.Interfaces;

public interface IPersonRepository : IAsyncRepository<Person>
{
    Task<bool> ExistsByIdentification(string identification, CancellationToken cancellationToken);
    Task<Person?> FindByIdentification(string identification, CancellationToken cancellationToken);
    Task<List<PersonDTO>> ListAllAsync(CancellationToken cancellationToken);
}
