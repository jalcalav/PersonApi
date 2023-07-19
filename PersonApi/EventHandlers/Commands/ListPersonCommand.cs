using MediatR;
using PersonApi.Models;

namespace PersonApi.EventHandlers.Commands;

public class ListPersonCommand : IRequest<List<PersonDTO>>
{
}