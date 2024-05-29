namespace BuberDinner.Contracts.Authenication;

public record LoginRequest(
  string Email,
  string Password
);
