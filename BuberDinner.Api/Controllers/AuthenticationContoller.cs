using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authenication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace BuberDinner.Api.Contollers;


[Route("auth")]
// instead of adding filter to all controller individually [ErrorHandlingFilter]
// program.cs can be modified to add filter globally
public class AuthenticationContoller : ApiController
{

  private readonly IAuthenticationService _authenticationService;

  public AuthenticationContoller(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest registerRequest)
  {
    ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password);

    return authResult.Match(
      authResult => Ok(MapAuthResult(authResult)),
      errors => Problem(errors)
    );
  }

  private static AuthenicationResponse MapAuthResult(AuthenticationResult authResult)
  {
    return new AuthenicationResponse(
        authResult.User.Id,
        authResult.User.FirstName,
        authResult.User.LastName,
        authResult.User.Email,
        authResult.Token);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest loginRequest)
  {
    var authResult = _authenticationService.Login(
      loginRequest.Email,
      loginRequest.Password);

    // example of overriding the default status code
    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(
        statusCode: StatusCodes.Status401Unauthorized,
        title: authResult.FirstError.Description
      );
    }

    return authResult.Match(
       authResult => Ok(MapAuthResult(authResult)),
       errors => Problem(errors)
     );
  }

}