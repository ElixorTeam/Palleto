namespace Pl.Desktop.Models.Features.Pallets.Input;

public sealed record PalletPieceCreateDto
{
    [JsonPropertyName("pluId")]
    public required Guid PluId { get; init; }

    [JsonPropertyName("characteristicId")]
    public required Guid CharacteristicId { get; init; }

    [JsonPropertyName("palletManId")]
    public required Guid PalletManId { get; init; }

    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    [JsonPropertyName("labelCount")]
    public required byte LabelCount { get; init; }

    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; init; }

    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }
}