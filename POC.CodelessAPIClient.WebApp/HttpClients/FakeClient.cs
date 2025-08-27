using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public class FakeClient : IClient
{
    private ShoppingList _shoppingList;
    public FakeClient()
    {
        _shoppingList = new Models.ShoppingList()
        {
            Name = "Shopping List for Tesco",
            Date = DateTime.UtcNow
        };
    }

    public Task<ShoppingList> Get() => Task.FromResult(_shoppingList);

    public Task<ShoppingList> AddItem(ShoppingListItem item)
    {
        item.Id ??= Guid.NewGuid();
        _shoppingList.Add(item);
        return Task.FromResult(_shoppingList);
    }

    public Task<ShoppingList> UpdateItem(ShoppingListItem item)
    {
        var existingItem = _shoppingList.Single(x => x.Id == item.Id);
        _shoppingList[_shoppingList.IndexOf(existingItem)] = item;
        return Task.FromResult(_shoppingList);
    }

    public Task<ShoppingList> RemoveItem(ShoppingListItem item)
    {
        _shoppingList.RemoveAll(x => x.Id == item.Id);
        return Task.FromResult(_shoppingList);
    }
}