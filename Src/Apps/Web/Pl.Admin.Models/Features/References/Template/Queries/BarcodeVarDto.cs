namespace Pl.Admin.Models.Features.References.Template.Queries;

public sealed record BarcodeVarDto
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(TypeJsonConverter))]
    public required Type Type { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("length")]
    public required short Length { get; set; }

    [JsonPropertyName("isRepeatable")]
    public required bool IsRepeatable { get; set; }
}