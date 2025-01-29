using POC.CodelessAPIClient.Models;
using Refit;

namespace POC.CodelessAPIClient.ComparerConsole;

public class RunCommandsUsingRefit : RunCommandBase
{
    private IRefitClient _refitClient;

    public RunCommandsUsingRefit(string apiBaseURL)
        : base(apiBaseURL.TrimEnd('/') + "/ShoppingList", "Refit")
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri(ApiBaseURL)
        };

        _refitClient = RestService.For<IRefitClient>(client);
    }

    public override async Task<ShoppingList> GetShoppingList()
    {
        return await _refitClient.GetShoppingList();
    }

    public override async Task PopulateDatabase()
    {
        foreach(var item in ItemGenerator.Generate())
        {
            await _refitClient.PostItem(item);
        }
    }

    public override async Task<ShoppingList> Update(ShoppingListItem item)
    {
        return await _refitClient.Update(item);
    }

    public override async Task<ShoppingList> Delete()
    {
        var list = await GetShoppingList();

        foreach(var item in list)
        {
            await _refitClient.Delete(item.Id.Value);
        }

        return await GetShoppingList();
    }
}

public interface IRefitClient
{
    [Get("/")]
    public Task<ShoppingList> GetShoppingList();
    [Post("/")]
    public Task PostItem(ShoppingListItem item);
    [Put("/")]
    public Task<ShoppingList> Update(ShoppingListItem item);
    [Delete("/{id}")]
    public Task<ShoppingList> Delete([AliasAs("id")] Guid itemId);

}