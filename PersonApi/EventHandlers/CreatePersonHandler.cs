using MediatR;
using PersonApi.Domain;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;

namespace PersonApi.EventHandlers;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, IdResponse>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    private async Task<bool> IsValid(CreatePersonCommand request, AppResponse resp, CancellationToken cancellationToken)
    {
        if (request.BirthDate > DateTime.Now)
        {
            resp.AddError("Invalid BirthDate");
        }
        if (await _personRepository.ExistsByIdentification(request.Identification, cancellationToken))
        {
            resp.AddError("Identification {0} already exists", request.Identification);
        }
        return resp.Ok;
    }

    public async Task<IdResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var resp = new IdResponse();
        if (!await IsValid(request, resp, cancellationToken))
        {
            return resp;
        }
        Person person = new()
        {
            Identification = request.Identification,
            FullName = request.FullName,
            BirthDate = request.BirthDate,
        };
        await _personRepository.AddAsync(person, cancellationToken);
        resp.Id = person.Id;
        return resp;
    }
}
