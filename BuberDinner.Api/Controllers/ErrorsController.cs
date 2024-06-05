using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Contollers;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    return Problem(
      detail: exception?.Message,
      title: "An error occurred",
      statusCode: StatusCodes.Status500InternalServerError,
      type: "https://buberdinner.com/errors/internal-server-error"
    );
  }
}