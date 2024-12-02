namespace Pl.Desktop.Models.Features.Pallets.Output;

public sealed record LabelDto
{
    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }

    [JsonPropertyName("barcode")]
    public required string Barcode { get; init; }
}