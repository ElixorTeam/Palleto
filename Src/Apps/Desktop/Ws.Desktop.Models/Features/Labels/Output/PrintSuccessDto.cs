namespace Ws.Desktop.Models.Features.Labels.Output;

public sealed record PrintSuccessDto
{
    [JsonPropertyName("armCounter")]
    public required uint ArmCounter { get; init; }

    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }
}