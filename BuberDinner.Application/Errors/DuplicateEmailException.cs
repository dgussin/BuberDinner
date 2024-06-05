using System.Net;

namespace BuberDinner.Application.Errors
{
  public class DuplicateEmailException : Exception, IServiceException
  {
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email already exists.";
  }
}