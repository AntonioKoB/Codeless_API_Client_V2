using Microsoft.AspNetCore.Components;
using POC.CodelessAPIClient.WebApp.HttpClients;

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
            _authString = await AuthClient.GetAuthorization();
            StateHasChanged();
        }
    }
}