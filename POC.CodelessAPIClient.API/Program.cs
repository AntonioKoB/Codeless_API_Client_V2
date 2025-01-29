using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using POC.CodelessAPIClient.API.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace POC.CodelessAPIClient.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services);

        Configure(builder.Build());
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication("CustomScheme")
            .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomScheme", options => { });

        services.AddAuthorization();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheAPIServer", Version = "v1" });
        });
        services.AddSingleton<IFakeDatabase, FakeDatabase>();
    }

    private static void Configure(WebApplication app)
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheAPIServer v1"));

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}