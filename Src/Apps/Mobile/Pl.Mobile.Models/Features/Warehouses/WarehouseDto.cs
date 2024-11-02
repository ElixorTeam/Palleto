namespace Pl.Mobile.Models.Features.Warehouses;

public sealed class WarehouseDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("productionSiteName")]
    public required string ProductionSiteName { get; set; } = string.Empty;

    [JsonPropertyName("warehouseName")]
    public required string WarehouseName { get; set; } = string.Empty;
}