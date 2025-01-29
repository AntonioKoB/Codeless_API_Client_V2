using Newtonsoft.Json;
using POC.CodelessAPIClient.Models;
using RestSharp;

namespace POC.CodelessAPIClient.ComparerConsole;

public class RunCommandsUsingRestsharp : RunCommandBase
    {
        private RestClient client;

        public RunCommandsUsingRestsharp(string apiBaseURL)
            : base(apiBaseURL.TrimEnd('/') + "/ShoppingList", "Restsharp")
        {
            var options = new RestClientOptions(ApiBaseURL)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            client = new RestClient(options);
        }

        public override async Task<ShoppingList> GetShoppingList()
        {
            var request = new RestRequest();
            var result = await client.ExecuteAsync<ShoppingList>(request);
            if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            return result.Data;
        }

        public override async Task PopulateDatabase()
        {
            foreach (var item in ItemGenerator.Generate())
            {
                var request = new RestRequest("/", Method.Post);
                request.AddParameter(
                    "application/json",
                    JsonConvert.SerializeObject(item),
                    ParameterType.RequestBody
                    );

                var result = await client.ExecuteAsync(request);
                if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            }
        }

        public override async Task<ShoppingList> Update(ShoppingListItem item)
        {
            var request = new RestRequest("/", Method.Put);
            request.AddParameter("application/json", JsonConvert.SerializeObject(item), ParameterType.RequestBody);
            var result = await client.ExecuteAsync<ShoppingList>(request);
            if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            return result.Data;
        }

        public override async Task<ShoppingList> Delete()
        {
            var list = await GetShoppingList();
            foreach(var item in list)
            {
                var request = new RestRequest("/" + item.Id, Method.Delete);
                var result = await client.ExecuteAsync(request);
                if (!result.IsSuccessful) throw new HttpRequestException(result.ErrorMessage, result.ErrorException);
            }

            return await GetShoppingList();
        }
    }