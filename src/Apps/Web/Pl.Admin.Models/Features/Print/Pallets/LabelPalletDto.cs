namespace Pl.Admin.Models.Features.Print.Pallets;

public sealed record LabelPalletDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("barcode")]
    public required string Barcode { get; init; }
}