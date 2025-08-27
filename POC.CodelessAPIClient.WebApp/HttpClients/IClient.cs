using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public interface IClient
{
    Task<ShoppingList> Get();
    Task<ShoppingList> AddItem(ShoppingListItem item);
    Task<ShoppingList> UpdateItem(ShoppingListItem item);
    Task<ShoppingList> RemoveItem(ShoppingListItem item);
}