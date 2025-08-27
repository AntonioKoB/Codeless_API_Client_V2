using POC.CodelessAPIClient.Models;
using Refit;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public interface IClient
{
    [Get("/ShoppingList")]
    Task<ShoppingList> Get();

    [Post("/ShoppingList")]
    Task<ShoppingList> AddItem(ShoppingListItem item);

    [Put("/ShoppingList")]
    Task<ShoppingList> UpdateItem(ShoppingListItem item);

    [Delete("/ShoppingList/{id}")]
    Task<ShoppingList> RemoveItem([AliasAs("id")]Guid id);
}