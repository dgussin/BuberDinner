using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Errors;
using BuberDinner.Domain.Entities;
using OneOf;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public OneOf<AuthenticationResult, DuplicateEmailError> Register(string firstName, string lastName, string email, string password)
  {

    // 1. Validate the user doesn't exist
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      return new DuplicateEmailError();
    }

    // 2. create user (generate unique ID) & Persist to DB
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password
    };

    _userRepository.Add(user);

    // 3. create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }

  public AuthenticationResult Login(string email, string password)
  {
    // 1. validate the user exists
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      throw new Exception("User with given email does not exit");
    }

    // 2. validate the password if correct
    if (user.Password != password)
    {
      throw new Exception("Invalid password");
    }

    // 3. create jwt token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(
      user,
      token);
  }
}