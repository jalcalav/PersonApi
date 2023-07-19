using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

namespace PersonApi.Controllers;

public abstract class AbstractController : ControllerBase
{
    [NonAction]
    public virtual ObjectResult AppResponse<T>(T resp) where T : AppResponse
    {
        if (resp.Ok)
        {
            return Ok(resp);
        }
        return BadRequest(resp);
    }
}
