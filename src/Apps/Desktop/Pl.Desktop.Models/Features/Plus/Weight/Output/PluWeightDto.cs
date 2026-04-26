namespace Pl.Desktop.Models.Features.Plus.Weight.Output;

public record PluWeightDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [JsonPropertyName("bundleCount")]
    public required byte BundleCount { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("fullName")]
    public required string FullName { get; init; }

    [JsonPropertyName("box")]
    public required string Box { get; init; }

    [JsonPropertyName("bundle")]
    public required string Bundle { get; init; }

    [JsonPropertyName("tareWeight")]
    public required decimal TareWeight { get; init; }
};