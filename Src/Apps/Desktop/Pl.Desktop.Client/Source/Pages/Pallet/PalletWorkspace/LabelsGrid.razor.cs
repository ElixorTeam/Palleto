using Append.Blazor.Printing;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Pl.Desktop.Client.Source.Shared.Api.Desktop.Endpoints;
using Pl.Desktop.Client.Source.Shared.Services.Devices;
using Pl.Desktop.Models.Features.Pallets.Output;
using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Desktop.Client.Source.Pages.Pallet.PalletWorkspace;

public sealed partial class LabelsGrid : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IPrinterService PrinterService { get; set; } = default!;
    [Inject] private PalletEndpoints PalletEndpoints { get; set; } = default!;
    [Inject] private IPrintingService PrintingService { get; set; } = default!;
    [Inject] private HtmlRenderer HtmlRenderer { get; set; } = default!;
    [Inject] private IState<PrinterState> PrinterState { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletDto Pallet { get; set; } = default!;

    private IEnumerable<DataItem> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private bool IsPrinting { get; set; } = false;
    private const ushort PrinterRequestDelay = 500;
    private const ushort MaxPrinterErrors = 3;

    private PalletDto PreviousPallet { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PreviousPallet = Pallet;
    }

    protected override void OnParametersSet()
    {
        if (PreviousPallet == Pallet) return;
        PreviousPallet = Pallet;
        SelectedItems = [];
    }

    private async Task SaveToClipboard(string value)
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", value);
            ToastService.ShowInfo(Localizer["ToastValueCopiedToClipboard"]);
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastValueCopiedToClipboardUnsuccessful"]);
        }
    }

    private void ToggleAllLabels(DataItem[] labels) =>
        SelectedItems = SelectedItems.Count().Equals(labels.Length) ? [] : labels.ToList();

    private void SelectAllItems(List<DataItem> labels) => SelectedItems = labels;

    private IQueryable<DataItem> GetOrderedLabels(LabelDto[] labels)
    {
        IEnumerable<DataItem> indexedLabels = labels.Select((label, index) => new DataItem { Id = index + 1, Label = label });
        if (string.IsNullOrWhiteSpace(SearchingNumber)) return indexedLabels.AsQueryable();
        indexedLabels = indexedLabels.Where(x => x.Id.ToString().Contains(SearchingNumber));
        return indexedLabels.AsQueryable();
    }

    private async Task PrintLabelsAsync()
    {
        if (IsPrinting) return;

        IsPrinting = true;
        await PrinterService.RequestStatusAsync();

        if (PrinterState.Value.Status is not (PrinterStatus.Ready or PrinterStatus.Busy))
        {
            PrintPrinterStatusMessage(PrinterState.Value.Status);
            IsPrinting = false;
            return;
        }

        string toastUid = Guid.NewGuid().ToString();
        double percentagesPerLabel = 100.0 / SelectedItems.Count();
        ToastParameters<ProgressToastContent> toastData = new()
        {
            Id = toastUid,
            Intent = ToastIntent.Upload,
            Title = Localizer["LabelsPrintingToastTitle"],
            Timeout = 0,
            TopAction = "Cancel",
            Content = new()
            {
                Details = Localizer["LabelsPrintingToastDescription"],
                Progress = 0,
            }
        };
        ToastService.ShowProgressToast(toastData);

        IOrderedEnumerable<DataItem> orderedLabels = SelectedItems.OrderBy(x => x.Id);
        bool isPrintedSuccessfully = true;
        ushort printedLabelsCount = 0;
        List<DataItem> itemsToDelete = [];

        foreach (DataItem item in orderedLabels)
        {
            for (int attempt = 0 ; attempt < MaxPrinterErrors ; attempt++)
            {
                try
                {
                    await PrinterService.PrintZplAsync(item.Label.Zpl);
                    itemsToDelete.Add(item);
                    isPrintedSuccessfully = true;
                    printedLabelsCount++;
                    toastData.Content.Progress = (int)(printedLabelsCount * percentagesPerLabel);
                    ToastService.UpdateToast(toastUid, toastData);
                    break;
                }
                catch
                {
                    isPrintedSuccessfully = false;
                    await Task.Delay(PrinterRequestDelay);
                }
            }

            if (!isPrintedSuccessfully)
            {
                ToastService.ShowError(string.Format(Localizer["IndexedLabelNotPrinted"], item.Id));
                break;
            }

            await Task.Delay(PrinterRequestDelay);
        }

        SelectedItems = SelectedItems.Except(itemsToDelete).ToArray();

        ToastService.CloseToast(toastUid);
        if (isPrintedSuccessfully)
            ToastService.ShowSuccess(Localizer["LabelsPrintingSuccess"]);
        else
            ToastService.ShowError(Localizer["PrintingStoppedDueToErrors"]);

        IsPrinting = false;

        await InvokeAsync(StateHasChanged);
    }

    private void PrintPrinterStatusMessage(PrinterStatus printerStatus) =>
        ToastService.ShowWarning(printerStatus switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusDisconnected"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private Task PrintPalletCard()
    {
        return HtmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            Dictionary<string, object?> parameters = new()
            {
                { nameof(PalletDocument.Pallet), Pallet }
            };

            HtmlRootComponent output = await HtmlRenderer.RenderComponentAsync<PalletDocument>(ParameterView.FromDictionary(parameters));
            await PrintingService.Print(output.ToHtmlString(), PrintType.RawHtml);
        });
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelDto Label { get; init; } = null!;
}