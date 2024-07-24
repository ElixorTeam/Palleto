using System.Security.Claims;
using Fluxor.Blazor.Web.Components;
using Force.DeepCloner;
using Refit;
using Ws.Shared.Api.ApiException;

namespace DeviceControl.Source.Widgets.Section;

public abstract class SectionFormBase<TItem> : FluxorComponent
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [CascadingParameter] protected FluentDialog Dialog { get; set; } = default!;
    [Parameter, EditorRequired] public TItem FormModel { get; set; } = default!;

    protected ClaimsPrincipal UserPrincipal { get; private set; } = new();
    private TItem DialogItemCopy { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogItemCopy = FormModel.DeepClone();
    }

    protected override async Task OnInitializedAsync() =>
        UserPrincipal = (await AuthState).User;

    protected virtual Task DeleteItemAction() =>
        throw new NotImplementedException();

    protected virtual Task UpdateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task CreateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected async Task OnCancelAction() => await Dialog.CancelAsync();

    protected void ResetAction()
    {
        FormModel = DialogItemCopy.DeepClone();
        ToastService.ShowInfo(Localizer["ToastResetItem"]);
    }

    private async Task ExecuteAction(Func<Task> action, string successMessage)
    {
        try
        {
            await action();
            ToastService.ShowSuccess(successMessage);
            await Dialog.CloseAsync();
        }
        catch (ApiException ex)
        {
            if (!ex.HasContent || string.IsNullOrEmpty(ex.Content) || !SerializationUtils.TryDeserialize(ex.Content, out ApiExceptionClient? exception) || exception == null)
                ToastService.ShowError(Localizer["UnknownError"]);
            else
                ToastService.ShowError(exception.LocalizeMessage);
        }
        catch
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }

    protected async Task CreateItem() =>
        await ExecuteAction(() => CreateItemAction(FormModel), Localizer["ToastCreateItem"]);

    protected async Task DeleteItem() =>
        await ExecuteAction(DeleteItemAction, Localizer["ToastDeleteItem"]);

    protected async Task UpdateItem()
    {
        if (FormModel != null && FormModel.Equals(DialogItemCopy))
        {
            await Dialog.CancelAsync();
            return;
        }

        await ExecuteAction(() => UpdateItemAction(FormModel), Localizer["ToastUpdateItem"]);
    }
}