namespace Ws.Desktop.Models.Features.Plus.Piece.Output;

public sealed record NestingDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("bundleCount")]
    public required byte BundleCount { get; init; }

    [JsonPropertyName("box")]
    public required string Box { get; init; }
}

public sealed record PluPieceDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("fullName")]
    public required string FullName { get; init; }

    [JsonPropertyName("bundle")]
    public required string Bundle { get; init; }

    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [JsonPropertyName("nestings")]
    public required List<NestingDto> Nestings { get; init; }
};