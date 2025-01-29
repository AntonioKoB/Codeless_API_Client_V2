using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.API.Database;

public class FakeDatabase : IFakeDatabase
{
    private ShoppingList _shoppingList;

    public FakeDatabase()
    {
        _shoppingList = new ShoppingList()
        {
            Name = "Shopping List for Tesco",
            Date = DateTime.UtcNow.AddDays(1)
        };
    }

    public Task<ShoppingList> Get()
    {
        return Task.FromResult(_shoppingList);
    }

    public Task<ShoppingList> Update(ShoppingListItem item)
    {
        var existingItem = _shoppingList.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem == null)
            throw new Exception("Item doesn't exist");

        _shoppingList[_shoppingList.IndexOf(existingItem)] = item;

        return Task.FromResult(_shoppingList);
    }

    public Task<ShoppingList> AddItem(ShoppingListItem item)
    {
        item.Id ??= Guid.NewGuid();

        _shoppingList.Add(item);

        return Task.FromResult(_shoppingList);
    }

    public Task<ShoppingList> Deletetem(ShoppingListItem item)
    {
        var existingItem = _shoppingList.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem == null)
            throw new Exception("Item doesn't exist");

        _shoppingList.Remove(existingItem);

        return Task.FromResult(_shoppingList);
    }
}