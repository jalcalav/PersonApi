using MediatR;
using PersonApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.EventHandlers.Commands;

public class DeletePersonCommand : IRequest<IdResponse>
{
    [Required]
    public Guid Id { get; set; }
}