using MediatR;
using PersonApi.EventHandlers.Commands;
using PersonApi.Infraestructure.Interfaces;
using PersonApi.Models;

namespace PersonApi.EventHandlers;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, IdResponse>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    private async Task<bool> IsValid(UpdatePersonCommand request, AppResponse resp, CancellationToken cancellationToken)
    {
        if (request.BirthDate > DateTime.Now)
        {
            resp.AddError("Invalid BirthDate");
        }
        var person = await _personRepository.FindByIdentification(request.Identification, cancellationToken);
        if (person != null && person.Id != request.Id)
        {
            resp.AddError("Identification {0} already exists", request.Identification);
        }
        return resp.Ok;
    }

    public async Task<IdResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var resp = new IdResponse();
        if (!await IsValid(request, resp, cancellationToken))
        {
            return resp;
        }
        var person = await _personRepository.FindByIdAsync(request.Id);
        if (person is null)
        {
            resp.AddError("Id {0} doesn't exists", request.Id);
            return resp;
        }
        person.Identification = request.Identification;
        person.FullName = request.FullName;
        person.BirthDate = request.BirthDate;
        await _personRepository.UpdateAsync(person, cancellationToken);
        resp.Id = person.Id;
        return resp;
    }
}
