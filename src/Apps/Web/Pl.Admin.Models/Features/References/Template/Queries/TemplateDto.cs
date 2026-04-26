namespace Pl.Admin.Models.Features.References.Template.Queries;

public sealed record TemplateDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("isWeight")]
    public required bool IsWeight { get; set; }

    [JsonPropertyName("width")]
    public required ushort Width { get; set; }

    [JsonPropertyName("height")]
    public required ushort Height { get; set; }

    [JsonPropertyName("rotate")]
    public required ushort Rotate { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}