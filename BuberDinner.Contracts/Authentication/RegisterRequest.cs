namespace BuberDinner.Contracts.Authenication;

public record RegisterRequest(
  string FirstName,
  string LastName,
  string Email,
  string Password
);
