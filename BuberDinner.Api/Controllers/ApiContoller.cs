using BuberDinner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Contollers;

[ApiController]
public class ApiController : ControllerBase
{
  protected IActionResult Problem(List<Error> errors)
  {
    HttpContext.Items[HttpContextItemKeys.Errors] = errors;
    var firstError = errors[0];

    var statusCode = firstError.Type switch
    {

      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      // ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      // ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Failure => StatusCodes.Status500InternalServerError,
      ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError
    };
    return Problem(
      statusCode: statusCode,
      title: firstError.Description
    );
  }
}