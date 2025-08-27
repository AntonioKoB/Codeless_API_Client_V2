using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public interface IAuthorizedClient
{
    Task<string> GetAuthorization();
}