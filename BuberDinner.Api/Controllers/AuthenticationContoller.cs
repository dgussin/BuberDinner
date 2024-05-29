using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authenication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Contollers;

[ApiController]
[Route("auth")]
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
    var authResult = _authenticationService.Register(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password);

    var response = new AuthenicationResponse(
      authResult.Id,
      authResult.FirstName,
      authResult.LastName,
      authResult.Email,
      authResult.Token);

    return Ok(response);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest loginRequest)
  {
    var authResult = _authenticationService.Login(
      loginRequest.Email,
      loginRequest.Password);

    var response = new AuthenicationResponse(
      authResult.Id,
      authResult.FirstName,
      authResult.LastName,
      authResult.Email,
      authResult.Token);

    return Ok(response);
  }

}