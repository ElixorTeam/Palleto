using Pl.Mobile.Models.Features.Pallets;

namespace Pl.Mobile.Client.Source.Widgets.TransferForm;

public static class TransferMapper
{
    public static PalletsMoveDto MapToPalletsMoveDto(TransferFormModel model, Guid userId)
    {
        return new()
        {
            DocumentBarcode = model.DocumentNumber,
            PalletsBarcodes = model.Pallets,
            WarehouseId = model.Warehouse.Id,
            UserId = userId
        };
    }
}