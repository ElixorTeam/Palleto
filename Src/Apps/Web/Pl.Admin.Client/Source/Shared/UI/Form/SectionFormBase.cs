using Fluxor.Blazor.Web.Components;
using Force.DeepCloner;
using Refit;
using Pl.Shared.Web.Extensions;

namespace Pl.Admin.Client.Source.Shared.UI.Form;

public abstract class SectionFormBase<TItem> : FluxorComponent where TItem : IEquatable<TItem>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [CascadingParameter] protected FluentDialog Dialog { get; set; } = default!;
    [Parameter, EditorRequired] public TItem FormModel { get; set; } = default!;

    protected ClaimsPrincipal UserPrincipal { get; private set; } = new();
    protected TItem DialogItemCopy { get; private set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogItemCopy = FormModel;
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
            ToastService.ShowError(ex.GetMessage(Localizer["UnknownError"]));
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
        if (FormModel.Equals(DialogItemCopy))
        {
            await Dialog.CancelAsync();
            return;
        }

        await ExecuteAction(() => UpdateItemAction(FormModel), Localizer["ToastUpdateItem"]);
    }
}