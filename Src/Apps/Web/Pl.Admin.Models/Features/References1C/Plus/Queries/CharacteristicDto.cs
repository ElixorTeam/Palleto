namespace Pl.Admin.Models.Features.References1C.Plus.Queries;

public sealed record CharacteristicDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("count")]
    public required ushort Count { get; init; }

    [JsonPropertyName("pluWeight")]
    public required decimal PluWeight { get; init; }

    [JsonPropertyName("bundle")]
    public required CharacteristicPackageDto Bundle { get; init; }

    [JsonPropertyName("clip")]
    public required CharacteristicPackageDto Clip { get; init; }

    [JsonPropertyName("box")]
    public required CharacteristicPackageDto Box { get; init; }

    [JsonPropertyName("totalWeight")]
    public decimal TotalWeight
    {
        get => (PluWeight + Bundle.Weight + Clip.Weight) * Count + Box.Weight;
        set => _ = value;
    }
}

public sealed record CharacteristicPackageDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("weight")] decimal Weight
);