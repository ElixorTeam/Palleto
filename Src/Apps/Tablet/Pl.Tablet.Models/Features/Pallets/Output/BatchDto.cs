namespace Pl.Tablet.Models.Features.Pallets.Output;

public sealed class BatchDto
{
    [JsonPropertyName("pluName")]
    public required string PluName { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public required DateTime Date { get; set; }

    [JsonPropertyName("weight")]
    public required decimal Weight { get; set; }

    [JsonPropertyName("number")]
    public required uint Number { get; set; }
}