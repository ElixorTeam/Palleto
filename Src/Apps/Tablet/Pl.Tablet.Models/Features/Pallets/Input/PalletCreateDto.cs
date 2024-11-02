namespace Pl.Tablet.Models.Features.Pallets.Input;

public sealed class PalletCreateDto
{
    [JsonPropertyName("documentBarcode")]
    public string DocumentBarcode { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    [JsonPropertyName("batches")]
    public List<BatchCreateDto> Batches { get; set; } = [];
}
