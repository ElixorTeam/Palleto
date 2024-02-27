using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class LabelsGrid : ComponentBase
{
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IPalletService PalletService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    private IEnumerable<LabelEntity> GridData { get; set; } = [];
    private DataGrid<LabelEntity> DataGridRef { get; set; } = null!;
    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;

    private IEnumerable<LabelEntity> GetGridData() => PalletService.GetAllLabels(PalletContext.CurrentPallet.Uid);

    protected override async Task OnInitializedAsync()
    {
        PalletContext.OnStateChanged += async () => await ReloadGrid();
        await Task.Run(InitializeData);
    }

    private async Task OnSearchingNumberChanged() => await DataGridRef.Reload();

    private void InitializeData() => GridData = GetGridData();

    private async Task ReloadGrid()
    {
        await Task.Run(InitializeData);
        SelectedItems = [];
        await DataGridRef.Reload();
        StateHasChanged();
    }

    private static void RowStyling(LabelEntity item, DataGridRowStyling styling) =>
        styling.Class = "transition-colors !border-y !border-black/[.1] hover:bg-neutral-100";

    private static void SelectedRowStyling(LabelEntity item, DataGridRowStyling styling)
    {
        styling.Color = new("e5e5e5");
        styling.Class = "!bg-neutral-100 !text-black";
    }

    private bool OnCustomFilter(LabelEntity entity) =>
        string.IsNullOrEmpty(SearchingNumber) ||
        entity.Plu.Number.ToString().Contains(SearchingNumber, StringComparison.OrdinalIgnoreCase);
}