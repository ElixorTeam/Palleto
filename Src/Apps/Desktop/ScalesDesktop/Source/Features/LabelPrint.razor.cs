using ScalesDesktop.Source.Shared.Models;
using ScalesDesktop.Source.Shared.Services.Devices;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelPrint : FluxorComponent
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;
    [Inject] private ArmEndpoints ArmEndpoints { get; set; } = default!;
    [Inject] private IState<PrinterState> PrinterState { get; set; } = default!;
    [Inject] private IState<WeightState> WeightState { get; set; } = default!;
    [Inject] private IState<ScalesState> ScalesState { get; set; } = default!;
    [Inject] private IState<PluState> PluState { get; set; } = default!;

    #endregion

    [Parameter, EditorRequired] public ArmValue Arm { get; set; } = default!;
    [Parameter, EditorRequired] public WeightKneadingModel KneadingModel { get; set; } = default!;

    private bool IsButtonClicked { get; set; }
    private const int ButtonCooldownDelay = 500;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await JsRuntime.InvokeVoidAsync("subscribeMiddleMouseClickEvent", DotNetObjectReference.Create(this), nameof(HandleMiddleMouseClick));
    }

    [JSInvokable]
    public async Task HandleMiddleMouseClick()
    {
        if (GetPrintLabelDisabledStatus()) return;
        await PrintLabel();
    }

    private async Task PrintLabel()
    {
        if (IsButtonClicked) return;
        IsButtonClicked = true;

        await PrintLabelAsync();

        await Task.Delay(ButtonCooldownDelay);
        IsButtonClicked = false;
    }

    private async Task PrintLabelAsync()
    {
        if (!ValidateScalesStatus() || !await ValidatePrinterStatus()) return;

        CreateWeightLabelDto createDto = new()
        {
            Kneading = KneadingModel.KneadingCount,
            ProductDt = GetProductDt(KneadingModel.ProductDate),
            WeightNet = KneadingModel.NetWeight,
            WeightTare = PluState.Value.Plu?.TareWeight ?? 0
        };

        try
        {
            WeightLabel label = await DesktopApi.CreatePluWeightLabel(PluState.Value.Plu!.Id, createDto);
            ArmEndpoints.UpdateArmCounter(label.ArmCounter);
            await PrinterService.PrintZplAsync(label.Zpl);
        }
        catch (PrinterCommandBodyException)
        {
            ToastService.ShowError(Localizer["ZplCodeError"]);
        }
        catch (PrinterStatusException)
        {
            PrintPrinterStatusMessage();
        }
        catch (PrinterConnectionException)
        {
            ToastService.ShowError(Localizer["PrinterStatusCommonError"]);
        }
        catch (ApiException ex)
        {
            if (!StrUtils.TryDeserializeFromJson(ex.Content, out ApiExceptionClient? exception))
                ToastService.ShowError(Localizer["UnknownError"]);
            else
                ToastService.ShowError($"{Localizer[exception.LocalizeMessage]}");
        }
        catch
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }

        await InvokeAsync(StateHasChanged);
    }

    private async Task<bool> ValidatePrinterStatus()
    {
        await PrinterService.RequestStatusAsync();
        if (PrinterState.Value.Status is PrinterStatus.Ready or PrinterStatus.Busy) return true;
        PrintPrinterStatusMessage();
        return false;
    }

    private bool ValidateScalesStatus()
    {
        if (!WeightState.Value.IsStable)
        {
            ToastService.ShowWarning(Localizer["ScalesStatusUnstable"]);
            return false;
        }

        if (KneadingModel.NetWeight >= 0) return true;
        ToastService.ShowWarning(Localizer["ScalesStatusTooLight"]);
        return false;
    }

    private static DateTime GetProductDt(DateTime time) =>
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

    private void PrintPrinterStatusMessage() =>
        ToastService.ShowWarning(PrinterState.Value.Status switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusDisconnected"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private bool GetPrintLabelDisabledStatus() =>
        PluState.Value.Plu == null || ScalesState.Value.Status != MassaKStatus.Ready;

    public new async ValueTask DisposeAsync()
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("unsubscribeMiddleMouseClickEvent");
        }
        catch
        {
            // pass
        }
    }
}