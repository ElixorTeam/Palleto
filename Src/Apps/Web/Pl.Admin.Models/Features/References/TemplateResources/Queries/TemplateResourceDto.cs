namespace Pl.Admin.Models.Features.References.TemplateResources.Queries;

public sealed record TemplateResourceDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ZplResourceType>))]
    public required ZplResourceType Type { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}