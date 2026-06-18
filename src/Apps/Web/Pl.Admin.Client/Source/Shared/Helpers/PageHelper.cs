// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Specialized;
using Microsoft.JSInterop;
using Phetch.Core;
using Pl.Shared.Web.Extensions;
using Refit;
using ToastService=Pl.Components.ToastService;

namespace Pl.Admin.Client.Source.Shared.Helpers;

public class PageHelper(IJSRuntime jsRuntime, NavigationManager navigationManager, ToastService toastService)
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

    public string? GetUrlQueryParam(string key)
    {
        Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return query[key];
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
            toastService.Success(successMessage, "Готово");
            if (onSuccess != null) await onSuccess();
        }
        catch (ApiException ex)
        {
            toastService.Error(ex.GetMessage("Неизвестная ошибка сервера"), "Ошибка");
            if (onError != null) await onError(ex);
        }
        catch (Exception ex)
        {
            toastService.Error(errorMessage, "Ошибка");
            if (onError != null) await onError(ex);
        }
    }
}
