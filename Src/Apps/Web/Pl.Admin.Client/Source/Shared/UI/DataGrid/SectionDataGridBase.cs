using Fluxor.Blazor.Web.Components;
using Microsoft.JSInterop;
using Phetch.Core;
using Refit;
using Pl.Shared.Web.Extensions;

namespace Pl.Admin.Client.Source.Shared.UI.DataGrid;

public abstract class SectionDataGridBase<TItem> : FluxorComponent where TItem : notnull
{
    # region Injects

    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    # endregion

    protected QueryOptions DefaultEndpointOptions { get; } =
        new() { RefetchInterval = TimeSpan.FromMinutes(1), StaleTime = TimeSpan.FromMinutes(1) };

    protected DialogParameters DialogParameters { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogParameters = new()
        {
            OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogClosing", instance.Id)
            ),
            OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogOpening", instance.Id)
            )
        };
    }

    protected async Task DeleteItem(TItem item) =>
        await DialogService.ShowDialogAsync<DeleteItemDialog>(EventCallback.Factory.Create(this, () => DeleteItemCallback(item)), DialogParameters);

    private async Task DeleteItemCallback(TItem item)
    {
        try
        {
            await DeleteItemAction(item);
            ToastService.ShowSuccess(Localizer["ToastDeleteItem"]);
            StateHasChanged();
        }
        catch (ApiException ex)
        {
            ToastService.ShowError(ex.GetMessage(Localizer["ToastDeleteItemError"]));
        }
    }

    protected async Task OpenModalWithItem<T>(TItem sectionEntity) where T : IDialogContentComponent<TItem> =>
        await DialogService.ShowDialogAsync<T>(sectionEntity, DialogParameters);

    protected async Task OpenModal<T>() where T : IDialogContentComponent =>
        await DialogService.ShowDialogAsync<T>(DialogParameters);

    protected async Task ContextFuncWrapper(TItem? item, EventCallback onComplete, Func<TItem, Task> action)
    {
        await onComplete.InvokeAsync();
        if (item == null) return;
        await action(item);
    }

    protected async Task OpenLinkInNewTab(string url) =>
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");

    protected Task OpenLink(string url)
    {
        NavigationManager.NavigateTo(url);
        return Task.CompletedTask;
    }

    protected virtual Task OpenUpdateFormModal(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task OpenCreateFormModal() =>
        throw new NotImplementedException();

    protected virtual Task<TItem> SearchByUidAction(Guid uid) =>
        throw new NotImplementedException();

    protected virtual Task OpenItemInNewTab(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task DeleteItemAction(TItem item) =>
        throw new NotImplementedException();
}