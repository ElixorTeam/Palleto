using System.Security.Claims;
using Fluxor.Blazor.Web.Components;
using Force.DeepCloner;
using Ws.Domain.Services.Exceptions;

namespace DeviceControl.Source.Widgets.Section;

public abstract class SectionFormBase<TItem> : FluxorComponent
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;
    [CascadingParameter] protected FluentDialog Dialog { get; set; } = default!;
    [Parameter, EditorRequired] public TItem FormModel { get; set; } = default!;

    protected ClaimsPrincipal UserPrincipal { get; private set; } = new();
    protected bool IsForceSubmit { get; set; }
    protected TItem DialogItemCopy { get; private set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogItemCopy = FormModel.DeepClone();
    }

    protected override async Task OnInitializedAsync()
        => UserPrincipal = (await AuthState).User;

    protected virtual Task DeleteItemAction(TItem item) =>
        throw new NotImplementedException();

    protected virtual TItem UpdateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected virtual TItem CreateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected async Task OnCancelAction() => await Dialog.CancelAsync();

    protected void ResetAction()
    {
        FormModel = DialogItemCopy.DeepClone();
        ToastService.ShowInfo(Localizer["ToastResetItem"]);
    }

    protected async Task CreateItem()
    {
        try
        {
            TItem createdItem = CreateItemAction(FormModel.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastCreateItem"]);
            await Dialog.CloseAsync(new SectionDialogContent<TItem>
            { Item = createdItem, DataAction = SectionDialogResultEnum.Create });
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                ToastService.ShowWarning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }

    protected async Task UpdateItem()
    {
        if (FormModel != null && FormModel.Equals(DialogItemCopy))
        {
            await Dialog.CancelAsync();
            return;
        }

        try
        {
            UpdateItemAction(FormModel.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastUpdateItem"]);
            await Dialog.CloseAsync();
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                ToastService.ShowWarning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }

    protected async Task DeleteItem()
    {
        try
        {
            await DeleteItemAction(FormModel.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastDeleteItem"]);
            await Dialog.CloseAsync();
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }
}