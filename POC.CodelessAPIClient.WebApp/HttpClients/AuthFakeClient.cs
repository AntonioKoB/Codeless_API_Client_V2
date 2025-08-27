namespace POC.CodelessAPIClient.WebApp.HttpClients;

public class AuthFakeClient : IAuthorizedClient
{
    public Task<string> GetAuthorization() => Task.FromResult("Authorized");
}