using POC.CodelessAPIClient.Models;
using Refit;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public interface IAuthorizedClient
{
    [Get("/SecureStatus")]
    Task<string> GetAuthorization();
}