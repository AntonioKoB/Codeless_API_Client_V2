namespace POC.CodelessAPIClient.WebApp;

public class AuthorizedHandler : DelegatingHandler
{
    private readonly string _apiBearerToken;

    public AuthorizedHandler(string apiBearerToken)
    {
        //_apiBearerToken = "asdasdasd";
        _apiBearerToken = apiBearerToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_apiBearerToken))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiBearerToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}