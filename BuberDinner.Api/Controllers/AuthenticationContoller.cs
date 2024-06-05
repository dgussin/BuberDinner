using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authenication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

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
    OneOf<AuthenticationResult, DuplicateEmailError> registerResult = _authenticationService.Register(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password);

    return registerResult.Match(
      result => Ok(MapAuthResult(result)),
      error => Problem(
        statusCode: StatusCodes.Status409Conflict,
        title: "Email already exists"
      )
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

    var response = new AuthenicationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token);

    return Ok(response);
  }

}