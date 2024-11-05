// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.JSInterop;
using Phetch.Core;
using Pl.Shared.Web.Extensions;
using Refit;

namespace Pl.Admin.Client.Source.Shared.Helpers;

public class PageHelper(IJSRuntime jsRuntime, NavigationManager navigationManager, IToastService toastService)
{
    private DialogParameters? _dialogParameters;

    public QueryOptions DefaultEndpointOptions { get; } =
        new() { RefetchInterval = TimeSpan.FromMinutes(1), StaleTime = TimeSpan.FromMinutes(1) };

    public DialogParameters DialogParameters => _dialogParameters ??= new()
    {
        OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
            await jsRuntime.InvokeVoidAsync("animateDialogClosing", instance.Id) ),
        OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
            await jsRuntime.InvokeVoidAsync("animateDialogOpening", instance.Id) )
    };

    public async Task OpenLinkInNewTab(string url) =>
        await jsRuntime.InvokeVoidAsync("open", url, "_blank");

    public Task OpenLink(string url)
    {
        navigationManager.NavigateTo(url);
        return Task.CompletedTask;
    }

    public async Task ApiActionWrapper(
        Func<Task> func,
        Func<Exception, Task>? onError = null,
        Func<Task>? onSuccess = null,
        string successMessage = "Операция выполнена успешно",
        string errorMessage = "Произошла ошибка при обработке операции"
    )
    {
        try
        {
            await func();
            toastService.ShowSuccess(successMessage);
            if (onSuccess != null) await onSuccess();
        }
        catch (ApiException ex)
        {
            toastService.ShowError(ex.GetMessage("Неизвестная ошибка сервера"));
            if (onError != null) await onError(ex);
        }
        catch (Exception ex)
        {
            toastService.ShowError(errorMessage);
            if (onError != null) await onError(ex);
        }
    }
}
