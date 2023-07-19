using MediatR;
using PersonApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.EventHandlers.Commands;

public class UpdatePersonCommand : IRequest<IdResponse>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Identification { get; set; } = "";
    [Required]
    public string FullName { get; set; } = "";
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}