using BuberDinner.Application.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Contollers;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    var (statuCode, message) = exception switch
    {
      IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
      _ => (500, "An error occured while processing your request.")
    };

    return Problem(statusCode: statuCode, title: message);
  }
}