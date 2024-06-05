using System.Net;

namespace BuberDinner.Application.Errors;

public interface IServiceException
{
  public HttpStatusCode StatusCode { get; }
  public string ErrorMessage { get; }
}