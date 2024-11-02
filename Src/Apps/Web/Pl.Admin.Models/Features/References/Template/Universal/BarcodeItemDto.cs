namespace Pl.Admin.Models.Features.References.Template.Universal;

public record BarcodeItemDto
{
    [JsonPropertyName("property")]
    public string Property { get; set; } = string.Empty;

    [JsonPropertyName("formatStr")]
    public string FormatStr { get; set; } = string.Empty;
}