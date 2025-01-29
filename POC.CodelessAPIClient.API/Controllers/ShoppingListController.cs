using Microsoft.AspNetCore.Mvc;
using POC.CodelessAPIClient.API.Database;
using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ShoppingListController : ControllerBase
{
    private IFakeDatabase _fakeDB;

    public ShoppingListController(IFakeDatabase database)
    {
        _fakeDB = database;
    }

    [HttpGet]
    public async Task<ShoppingList> Get()
    {
        return await _fakeDB.Get();
    }

    [HttpPost]
    public async Task<ShoppingList> Post([FromBody] ShoppingListItem item)
    {
        return await _fakeDB.AddItem(item);
    }

    [HttpPut]
    public async Task<ShoppingList> Put([FromBody] ShoppingListItem item)
    {
        return await _fakeDB.Update(item);
    }

    // DELETE /<ShopingListController>/5
    [HttpDelete("{id}")]
    public async Task<ShoppingList> Delete(Guid id)
    {
        return await _fakeDB.Deletetem(new ShoppingListItem() { Id = id });
    }
}