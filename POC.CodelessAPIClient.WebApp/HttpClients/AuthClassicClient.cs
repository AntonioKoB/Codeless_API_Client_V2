namespace POC.CodelessAPIClient.WebApp.HttpClients;

public class AuthClassicClient : IAuthorizedClient
{
    private readonly HttpClient _http;
    public AuthClassicClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetAuthorization()
    {
        var result = await _http.GetAsync("SecureStatus");
        if (!result.IsSuccessStatusCode)
            return "Not Authorized";
        return await result.Content.ReadAsStringAsync();
    }
}