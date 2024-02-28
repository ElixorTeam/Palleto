using Blazorise;
using DeviceControl.Resources;
using FluentValidation.Results;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Database.Core.Utils;
using Ws.Domain.Abstractions.Entities.Common;

namespace DeviceControl.Features.Sections.Shared.Form;

public class SectionFormBase<TItem> : ComponentBase where TItem : EntityBase, new()
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSubmitAction { get; set; }

    private TItem SectionEntityCopy { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        SectionEntityCopy = SectionEntity.DeepClone();
        return Task.CompletedTask;
    }

    protected string GetMockIfEmptyInput(string value) =>
        string.IsNullOrEmpty(value) ? Localizer["SectionFormInputEmpty"] : value;

    protected async Task ResetItem()
    {
        if (SectionEntity.Equals(SectionEntityCopy)) return;
        SectionEntity = SectionEntityCopy.DeepClone();
        await NotificationService.Info(Localizer["ToastResetItem"]);
    }

    protected async Task AddItem(TItem item, Func<TItem, TItem> saveAction)
    {
        if (item.IsExists) return;
        if (!await IsValidateItem(item, false)) return;
        saveAction(item);
        await NotificationService.Success(Localizer["ToastAddItem"]);
        await OnSubmitAction.InvokeAsync();
    }

    protected async Task UpdateItem(TItem item, Func<TItem, TItem> updateAction)
    {
        if (item.IsNew) return;
        if (!await IsValidateItem(item, true)) return;

        if (!SectionEntity.Equals(SectionEntityCopy))
        {
            updateAction(item);
            await NotificationService.Success(Localizer["ToastUpdateItem"]);
        }
        await OnSubmitAction.InvokeAsync();
    }

    private async Task<bool> IsValidateItem(TItem item, bool isUpdateForm)
    {
        ValidationResult result = SqlValidationUtils.GetValidationResult(item, isUpdateForm);
        if (result.Errors.Count == 0) return true;
        foreach (ValidationFailure error in result.Errors)
            await NotificationService.Error(error.ErrorMessage);
        return false;
    }

    protected async Task DeleteItem(Action<TItem> deleteItem)
    {
        deleteItem(SectionEntity);
        await NotificationService.Success(Localizer["ToastDeleteItem"]);
        await OnSubmitAction.InvokeAsync();
    }
}