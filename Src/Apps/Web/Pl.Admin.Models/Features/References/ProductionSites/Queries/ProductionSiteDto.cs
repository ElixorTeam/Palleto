namespace Pl.Admin.Models.Features.References.ProductionSites.Queries;

public sealed record ProductionSiteDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("address")]
    public required string Address { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}