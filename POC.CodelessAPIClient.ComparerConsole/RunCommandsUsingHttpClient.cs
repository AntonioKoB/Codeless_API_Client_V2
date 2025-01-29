using System.Text;
using Newtonsoft.Json;
using POC.CodelessAPIClient.Models;

namespace POC.CodelessAPIClient.ComparerConsole;

public class RunCommandsUsingHttpClient : RunCommandBase
{
    private HttpClient _client;
    public RunCommandsUsingHttpClient(string apiBaseURL)
        : base(apiBaseURL.TrimEnd('/') + "/ShoppingList", "HttpClient")
    {
        _client = new HttpClient(new HttpClientHandlerInsecure());
    }

    public override async Task<ShoppingList> GetShoppingList()
    {
        var response = await _client.GetAsync(ApiBaseURL);
        response.EnsureSuccessStatusCode();
        return await Deserialize(response);
    }

    public override async Task PopulateDatabase()
    {
        foreach (var item in ItemGenerator.Generate())
        {
            var json = JsonConvert.SerializeObject(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ApiBaseURL, data);
            response.EnsureSuccessStatusCode();
        }
    }

    public override async Task<ShoppingList> Update(ShoppingListItem item)
    {
        var json = JsonConvert.SerializeObject(item);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(ApiBaseURL, data);
        response.EnsureSuccessStatusCode();
        try
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await Deserialize(response);
        }
        catch // Could be ArgumentNullException or UnsupportedMediaTypeException
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
            return null;
        }

    }

    public override async Task<ShoppingList> Delete()
    {
        var list = await GetShoppingList();
        foreach (var item in list)
        {
            var response = await _client.DeleteAsync(ApiBaseURL + "/" + item.Id);
            response.EnsureSuccessStatusCode();
        }

        return await GetShoppingList();
    }

    private async Task<ShoppingList> Deserialize(HttpResponseMessage response)
    {
        try
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);

            var serializer = new JsonSerializer();
            return serializer.Deserialize<ShoppingList>(jsonReader);
        }
        catch // Could be ArgumentNullException or UnsupportedMediaTypeException
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
            return null;
        }
    }
}

public class HttpClientHandlerInsecure : HttpClientHandler
{
    public HttpClientHandlerInsecure()
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }
}