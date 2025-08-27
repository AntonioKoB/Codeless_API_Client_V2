using POC.CodelessAPIClient.WebApp.Components;
using POC.CodelessAPIClient.WebApp.HttpClients;

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

        // Register typed HttpClient for ClassicClient using base URL from configs
        builder.Services.AddHttpClient<IClient, ClassicClient>((sp, http) =>
        {
            http.BaseAddress = new Uri(configs!.ApiUrl.TrimEnd('/') + "/");
        });

        // Register typed HttpClient for AuthClassicClient using base URL from configs
        builder.Services.AddHttpClient<IAuthorizedClient, AuthClassicClient>((sp, http) =>
        {
            http.BaseAddress = new Uri(configs!.ApiUrl.TrimEnd('/') + "/");
            // Add Authorisation header
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {configs.ApiBearerToken}");
        });

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