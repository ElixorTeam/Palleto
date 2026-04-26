using FluentValidation;
using Pl.Desktop.Models.Features.PalletMen;
using Pl.Shared.Web.Extensions;
using IDispatcher = Fluxor.IDispatcher;

namespace Pl.Desktop.Client.Source.Pages.Pallet;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    # region Injects

    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;

    # endregion

    private PalletManFormModel Model { get; set; } = new();

    private bool IsLoading { get; set; }

    private async Task OnValidSubmit()
    {
        string toastId = Guid.NewGuid().ToString();
        ToastService.ClearAll();
        IsLoading = true;
        StateHasChanged();

        try
        {
            ToastService.ShowProgressToast(new()
            {
                Id = toastId,
                Intent = ToastIntent.Progress,
                Title = "Процесс авторизации",
                Timeout = null,
                Content = new() { Details = "Подождите пока мы не проверим пользователя по данному коду" }
            });

            PalletManDto dto = await DesktopApi.GetPalletManByCode(Model.Password);

            Dispatcher.Dispatch(new ChangePalletManAction(dto));
        }
        catch (ApiException ex)
        {
            ToastService.ShowError(ex.GetMessage("Неизвестная ошибка"));
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }

        IsLoading = false;
        ToastService.CloseToast(toastId);
        StateHasChanged();
    }
}

public record PalletManFormModel
{
    public string Password { get; set; } = string.Empty;
}

public class PalletManFormValidator : AbstractValidator<PalletManFormModel>
{
    public PalletManFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Password).NotNull().Length(4).WithName(wsDataLocalizer["ColPassword"]);
    }
}