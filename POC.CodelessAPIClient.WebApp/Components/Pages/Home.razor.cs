using Microsoft.AspNetCore.Components;
using POC.CodelessAPIClient.WebApp.HttpClients;
using Refit;

namespace POC.CodelessAPIClient.WebApp.Components.Pages;

public partial class Home
{
    private string _authString = "Not Authorized";
    [Inject] public IAuthorizedClient AuthClient { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await GetAuthorized();
            StateHasChanged();
        }
    }

    private async Task GetAuthorized()
    {
        try
        {
            _authString = await AuthClient.GetAuthorization();
        }
        catch (ApiException ex)
        {
            _authString = ex.ReasonPhrase;
        }
    }
}