using MediatR;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;

namespace PersonApi.EventHandlers;

public class ListPersonHandler : IRequestHandler<ListPersonCommand, List<PersonDTO>>
{
    private readonly IPersonRepository _personRepository;

    public ListPersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<PersonDTO>> Handle(ListPersonCommand request, CancellationToken cancellationToken)
    {
        return await _personRepository.ListAllAsync(cancellationToken);
    }
}
