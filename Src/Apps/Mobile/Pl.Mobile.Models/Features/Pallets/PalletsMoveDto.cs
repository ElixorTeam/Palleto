namespace Pl.Mobile.Models.Features.Pallets;

public sealed class PalletsMoveDto
{
    [JsonPropertyName("documentBarcode")]
    public string DocumentBarcode { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    [JsonPropertyName("warehouseId")]
    public Guid WarehouseId { get; set; }

    [JsonPropertyName("palletsBarcodes")]
    public List<string> PalletsBarcodes{ get; set; } = [];
}