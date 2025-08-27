using POC.CodelessAPIClient.WebApp.Components;
using POC.CodelessAPIClient.WebApp.HttpClients;
using Refit;

namespace POC.CodelessAPIClient.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var configs = builder.Configuration.GetSection("Configurations").Get<Configs>();

        builder.Services.AddRefitClient<IClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs!.ApiUrl));

        builder.Services.AddRefitClient<IAuthorizedClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs!.ApiUrl))
            .AddHttpMessageHandler(() => new AuthorizedHandler(configs.ApiBearerToken));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}