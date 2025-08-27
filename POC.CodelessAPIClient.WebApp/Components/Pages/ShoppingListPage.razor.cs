using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using POC.CodelessAPIClient.Models;
using POC.CodelessAPIClient.WebApp.HttpClients;
using POC.CodelessAPIClient.WebApp.Other;

namespace POC.CodelessAPIClient.WebApp.Components.Pages;

public partial class ShoppingListPage : ComponentBase
{
    private bool _isLoading = true;
    private ShoppingList _shoppingList = new();

    private ShoppingListItem _operationItem = new();

    private Units _units = ShoppingListUnits.GetUnits();

    private bool _showAll = true;

    [Inject] public IClient Client { get; set; }

    [Inject] public IJSRuntime JS { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            _isLoading = true;
            StateHasChanged();

            _shoppingList = await Client.Get();
            _operationItem.Unit = _units.First().Abrv;

            _isLoading = false;
            StateHasChanged();
        }
    }

    private async Task AddNew()
    {
        _isLoading = true;
        StateHasChanged();

        _operationItem.UnitName = _units.Single(x => x.Abrv == _operationItem.Unit).Name;
        _shoppingList = await Client.AddItem(_operationItem);

        await RefreshList();

        _isLoading = false;
        StateHasChanged();
    }



    private async Task SaveItem(ShoppingListItem item, object value)
    {
        _isLoading = true;
        StateHasChanged();

        item.IsBought = (bool)value;
        _shoppingList = await Client.UpdateItem(item);

        await RefreshList();
        _isLoading = false;
        StateHasChanged();
    }

    private async Task DeleteItem(ShoppingListItem item)
    {
        var confirmed = await JS.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {item.Name} - {item.Quantity}{item.Unit}?");
        if (confirmed)
        {
            _isLoading = true;
            StateHasChanged();
            await Client.RemoveItem(item);

            await RefreshList();

            _isLoading = false;
            StateHasChanged();
        }
    }

    private void RefreshOperationItem()
    {
        _operationItem = new();
        _operationItem.Unit = _units.First().Abrv;
    }

    private async Task RefreshList(bool? bValue = null)
    {
        _showAll = bValue ?? _showAll;
        _shoppingList = await Client.Get();
        RefreshOperationItem();

        StateHasChanged();
    }
}