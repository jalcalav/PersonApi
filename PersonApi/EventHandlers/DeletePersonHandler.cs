using MediatR;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;

namespace PersonApi.EventHandlers;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, IdResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IdResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var resp = new IdResponse();
        var person = await _personRepository.FindByIdAsync(request.Id);
        if (person is null)
        {
            resp.AddError("Id {0} doesn't exists", request.Id);
            return resp;
        }
        await _personRepository.DeleteAsync(person, cancellationToken);
        resp.Id = person.Id;
        return resp;
    }
}
