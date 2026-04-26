namespace Pl.Admin.Models.Features.References.Template.Universal;

public record BarcodeItemWrapper
{
    [JsonPropertyName("top")]
    public required List<BarcodeItemDto> Top { get; set; }

    [JsonPropertyName("bottom")]
    public required List<BarcodeItemDto> Bottom { get; set; }

    [JsonPropertyName("right")]
    public required List<BarcodeItemDto> Right { get; set; }
}