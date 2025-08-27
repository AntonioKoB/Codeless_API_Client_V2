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

        var configs = new Configs();
        builder.Configuration.GetSection("Configurations").Bind(configs);

        // Register typed HttpClient for ClassicClient using base URL from configs
        builder.Services.AddHttpClient<IClient, ClassicClient>((sp, http) =>
        {
            http.BaseAddress = new Uri(configs.ApiUrl.TrimEnd('/') + "/");
        });

        builder.Services.AddSingleton<IAuthorizedClient, AuthFakeClient>();

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