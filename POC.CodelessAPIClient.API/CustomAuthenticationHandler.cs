using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace POC.CodelessAPIClient.API;

public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string AUTHORIZATION_HEADER = "Authorization";
    private const string BEARER_PREFIX = "Bearer ";

    public CustomAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(AUTHORIZATION_HEADER))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        var token = Request.Headers[AUTHORIZATION_HEADER].ToString();
        if (!token.StartsWith(BEARER_PREFIX))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }

        token = token.Substring(BEARER_PREFIX.Length);
        if (token != "your-secret-token") // Replace with your token validation logic
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "user") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}