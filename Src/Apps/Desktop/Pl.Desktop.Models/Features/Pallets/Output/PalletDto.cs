using Pl.Desktop.Models.Shared;

namespace Pl.Desktop.Models.Features.Pallets.Output;

public sealed record PalletDto
{
    [JsonPropertyName("Id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("palletMan")]
    public required Fio PalletMan { get; init; }

    [JsonPropertyName("arm")]
    public required string Arm { get; init; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; init; }

    [JsonPropertyName("plus")]
    public required HashSet<PluPalletDto> Plus { get; init; }

    [JsonPropertyName("barcode")]
    public required string Barcode { get; init; }

    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }

    [JsonPropertyName("deletedAt")]
    public required DateTime? DeletedAt { get; init; }

    [JsonPropertyName("isShipped")]
    public required bool IsShipped { get; init; }

    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    #region JsonIgnore

    [JsonIgnore]
    public bool IsDelete => DeletedAt != null;

    [JsonIgnore]
    public HashSet<ushort> Kneadings => Plus.Select(i => i.Kneading).ToHashSet();

    [JsonIgnore]
    public decimal WeightNet => Plus.Sum(i => i.WeightNet);

    [JsonIgnore]
    public decimal WeightBrutto => Plus.Sum(i => i.WeightBrutto);

    [JsonIgnore]
    public ushort BundleCount => (ushort)Plus.Sum(i => i.BundleCount);

    [JsonIgnore]
    public ushort BoxCount => (ushort)Plus.Sum(i => i.BoxCount);

    #endregion
}