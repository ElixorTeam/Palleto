namespace Pl.Admin.Models.Features.Devices.Arms.Queries;

public sealed record ArmDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public required ArmType Type { get; set; }

    [JsonPropertyName("number")]
    public required int Number { get; set; }

    [JsonPropertyName("counter")]
    public required int Counter { get; set; }

    [JsonPropertyName("systemKey")]
    public required Guid SystemKey { get; set; }

    [JsonPropertyName("printer")]
    public required ProxyDto Printer { get; set; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; set; }

    [JsonPropertyName("productionSite")]
    public required ProxyDto ProductionSite { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}