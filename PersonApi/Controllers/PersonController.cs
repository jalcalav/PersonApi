using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonApi.EventHandlers.Commands;

namespace PersonApi.Controllers;

[ApiController]
[Route("person")]
public class PersonController : AbstractController
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _mediator.Send(new ListPersonCommand());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return AppResponse(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return AppResponse(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return AppResponse(result);
    }
}