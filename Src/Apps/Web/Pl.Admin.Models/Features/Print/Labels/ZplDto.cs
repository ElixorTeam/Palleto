namespace Pl.Admin.Models.Features.Print.Labels;

public class ZplDto
{
    [JsonPropertyName("width")]
    public required ushort Width { get; init; }

    [JsonPropertyName("height")]
    public required ushort Height { get; init; }

    [JsonPropertyName("rotate")]
    public required ushort Rotate { get; init; }

    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }
}