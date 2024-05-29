namespace BuberDinner.Contracts.Authenication;

public record AuthenicationResponse(
  Guid Id,
  string FirstName,
  string LastName,
  string Email,
  string Token
);
