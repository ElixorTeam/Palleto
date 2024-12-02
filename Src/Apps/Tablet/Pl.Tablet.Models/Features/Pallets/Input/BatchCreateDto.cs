namespace Pl.Tablet.Models.Features.Pallets.Input;

public sealed class BatchCreateDto
{
    [JsonPropertyName("pluId")]
    public Guid PluId { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("weight")]
    public decimal Weight { get; set; }
}