using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Client.Source.Widgets.TransferForm;

public record TransferFormModel
{
    public string DocumentNumber { get; set; } = string.Empty;
    public List<string> Pallets { get; set; } = [];
    public WarehouseDto Warehouse { get; set; } = new()
    {
        Id = Guid.Empty, WarehouseName = string.Empty, ProductionSiteName = string.Empty
    };
}