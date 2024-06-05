using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authenication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace BuberDinner.Api.Contollers;

[ApiController]
[Route("auth")]
// instead of adding filter to all controller individually [ErrorHandlingFilter]
// program.cs can be modified to add filter globally
public class AuthenticationContoller : ControllerBase
{

  private readonly IAuthenticationService _authenticationService;

  public AuthenticationContoller(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest registerRequest)
  {
    Result<AuthenticationResult> registerResult = _authenticationService.Register(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password);

    if (registerResult.IsSuccess)
    {
      return Ok(MapAuthResult(registerResult.Value));
    }

    var firstError = registerResult.Errors[0];

    if (firstError is DuplicateEmailError)
    {
      return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists");
    }

    return Problem();

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

    var response = new AuthenicationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token);

    return Ok(response);
  }

}