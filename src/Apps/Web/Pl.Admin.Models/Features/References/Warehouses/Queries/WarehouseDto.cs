namespace Pl.Admin.Models.Features.References.Warehouses.Queries;

public sealed record WarehouseDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("id1C")]
    public required Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("productionSite")]
    public required ProxyDto ProductionSite { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}