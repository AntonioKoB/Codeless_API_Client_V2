using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.API.Database;

public interface IFakeDatabase
{
    Task<ShoppingList> AddItem(ShoppingListItem item);
    Task<ShoppingList> Get();
    Task<ShoppingList> Update(ShoppingListItem item);
    Task<ShoppingList> Deletetem(ShoppingListItem item);
}