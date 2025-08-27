using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.WebApp.HttpClients;

public class ClassicClient : IClient
{
    private readonly HttpClient _http;

    public ClassicClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<ShoppingList> Get()
    {
        var result = await _http.GetFromJsonAsync<ShoppingList>("ShoppingList");
        if (result is null) throw new InvalidOperationException("Empty response from GET /ShoppingList");
        return result;
    }

    public async Task<ShoppingList> AddItem(ShoppingListItem item)
    {
        var resp = await _http.PostAsJsonAsync("ShoppingList", item);
        resp.EnsureSuccessStatusCode();
        var list = await resp.Content.ReadFromJsonAsync<ShoppingList>();
        if (list is null) throw new InvalidOperationException("Empty response from POST /ShoppingList");
        return list;
    }

    public async Task<ShoppingList> UpdateItem(ShoppingListItem item)
    {
        var resp = await _http.PutAsJsonAsync("ShoppingList", item);
        resp.EnsureSuccessStatusCode();
        var list = await resp.Content.ReadFromJsonAsync<ShoppingList>();
        if (list is null) throw new InvalidOperationException("Empty response from PUT /ShoppingList");
        return list;
    }

    public async Task<ShoppingList> RemoveItem(ShoppingListItem item)
    {
        if (item.Id is null) throw new ArgumentException("Item Id must be set for delete.", nameof(item));
        var resp = await _http.DeleteAsync($"ShoppingList/{item.Id}");
        resp.EnsureSuccessStatusCode();
        var list = await resp.Content.ReadFromJsonAsync<ShoppingList>();
        if (list is null) throw new InvalidOperationException("Empty response from DELETE /ShoppingList/{id}");
        return list;
    }
}